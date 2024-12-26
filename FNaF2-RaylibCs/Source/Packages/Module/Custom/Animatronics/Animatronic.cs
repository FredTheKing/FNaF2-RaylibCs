using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public enum AnimatronicType
{
  AutoBlackouter,
  TriggerWaiter
}

public class Animatronic : ScriptTemplate
{
  public Animatronic(Scene gameScenePointer, string name, float targetTime, AnimatronicType type, Location afkRoom, List<MovementOpportunity> movements)
  {
    _gameScenePointer = gameScenePointer;
    _startLocation = movements[0].From;
    _autoerBrakeLocation = afkRoom;
    Timer = new SimpleTimer(targetTime, true);
    NextQueue = null;

    if (Type is AnimatronicType.TriggerWaiter) _autoer = false;
    
    Name = name;
    Movements = movements;
    Type = type;
    CurrentLocation = _startLocation;
  }
  
  private Scene _gameScenePointer;
  private Location _autoerBrakeLocation;
  private Location _startLocation;
  private bool _locationChanged;
  private bool _autoer = true;
  private float _lightHatering = 3000f;
  private float _droppedChance = -1f;
  private List<float> _chances = [];
  private bool _grant;

  public string Name;
  public SimpleTimer Timer;
  public List<MovementOpportunity> Movements;
  public AnimatronicType Type;
  public int Difficulty = 15;
  public (Animatronic, Location)? NextQueue;
  public Location? PlanningLocation;
  public Location CurrentLocation;

  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode(Name))
    {
      ImGui.Text($" > Last Dropped: {_droppedChance}");
      ImGui.Text($" > Last Chances:");
      foreach (float chance in _chances)
      {
        ImGui.SameLine();
        ImGui.Text($">{chance}");
      }
      ImGui.Separator();
      ImGui.Text($" > Queue: {NextQueue?.Item1.Name} - {NextQueue?.Item2}");
      ImGui.Text($" > Walking: {_autoer}");
      ImGui.Text($" > Difficulty: {Difficulty}");
      ImGui.Text($" > Type: {Type}");
      ImGui.Text($" > Time to Try: {Timer.GetTimeLeft()}");
      ImGui.Text($" > Start Location: {_startLocation}");
      ImGui.Text($" > Planning Location: {PlanningLocation}");
      OnlyGameScene(() => { ImGui.Text($" > Current Location: {CurrentLocation}"); }, registry);
      ImGui.Text($" > Movements: {Movements.Count}");
      OnlyGameScene(() => { ImGui.Text($" > Current Movements: {Movements.Count(x => x.From == CurrentLocation)}"); }, registry);
      ImGui.TreePop();
    }
  }

  public override void Deactivation(Registry registry, string nextSceneName) => 
    OnlyGameScene(() => { Timer.StopAndResetTimer(); }, registry);
  
  public override void Activation(Registry registry) => 
    OnlyGameScene(() =>
    {
      Timer.Activation(registry);
      CurrentLocation = _startLocation;
      _lightHatering = 3000f;
      NextQueue = null;
    }, registry);
  
  public override void Update(Registry registry) => 
    OnlyGameScene(() =>
    {
      Timer.Update(registry);
      _autoer = CurrentLocation != _autoerBrakeLocation;

      if (_locationChanged)
      {
        if (NextQueue != null)
        {
          NextQueue!.Value.Item1.Timer.StartTimer();
          NextQueue!.Value.Item1.PlanningLocation = NextQueue!.Value.Item2;
          NextQueue = null;
        }
        _locationChanged = false;
      }
      
      switch (Type)
      {
        case AnimatronicType.AutoBlackouter:
          if (!Timer.TargetTrigger()) return;
          if (SuccessfulMovement() && _autoer) Move(registry);
          break;
        case AnimatronicType.TriggerWaiter:
          if (_grant) Move(registry);
          break;
      }
    }, registry);
  
  public override void Draw(Registry registry) => 
    OnlyGameScene(() => { }, registry);
  
  public void GrantMovement() => _grant = true;
  
  private void OnlyGameScene(Action action, Registry registry)
  {
    if (_gameScenePointer == registry.GetSceneManager().GetCurrentScene()) action();
  }

  private bool SuccessfulMovement() => new Random().Next(1, Config.MaxAnimatronicsDifficulty) <= Difficulty;

  public void Move(Registry registry)
  {
    _grant = false;
    if (PlanningLocation is null)
    {
      List<MovementOpportunity> targetMovements = Movements.Where(x => x.From == CurrentLocation).ToList();
      _droppedChance = (float)new Random().NextDouble();
      _chances = [0f];
      _chances.AddRange(targetMovements.Select(m => _chances.Last() + m.Chance));

      for (int i = 0; i < targetMovements.Count; i++)
      {
        if (!(_droppedChance >= _chances[i]) || !(_droppedChance < _chances[i + 1])) continue;
        PlanningLocation = targetMovements[i].To;
        break;
      }
    }

    foreach (Animatronic animatronic in registry.GetFNaF().GetAnimatronicManager().GetAnimatronics())
    {
      if (animatronic.CurrentLocation != PlanningLocation || animatronic == this) continue;
      
      Animatronic visitor = animatronic;
      while (visitor.NextQueue != null) 
        visitor = visitor.NextQueue!.Value.Item1;
      
      visitor.NextQueue = (this, (Location)PlanningLocation!);
      Timer.StopAndResetTimer();
      return;
    }
    
    if (PlanningLocation != CurrentLocation) _locationChanged = true;
    CurrentLocation = (Location)PlanningLocation!;
    PlanningLocation = null!;
  }
}