using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public enum AnimatronicType
{
  AutoBlackouter,
  LightHater,
  TriggerWaiter
}

public class Animatronic : ScriptTemplate
{
  public Animatronic(Scene gameScenePointer, string name, float targetTime, AnimatronicType type, List<MovementOpportunity> movements)
  {
    _gameScenePointer = gameScenePointer;
    _startLocation = movements[0].From;
    _timer = new SimpleTimer(targetTime, true);
    
    Name = name;
    Movements = movements;
    Type = type;
    CurrentLocation = _startLocation;
  }
  
  private Scene _gameScenePointer;
  private Location _startLocation;
  private SimpleTimer _timer;
  private float _droppedChance = -1f;
  private List<float> _chances = [];
  
  public string Name;
  public List<MovementOpportunity> Movements;
  public AnimatronicType Type;
  public int Difficulty = 15;
  public Location CurrentLocation;
  public bool GrantMovement;

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
      ImGui.Text($" > Difficulty: {Difficulty}");
      ImGui.Text($" > Type: {Type}");
      ImGui.Text($" > Time to Try: {_timer.GetTimeLeft()}");
      ImGui.Text($" > Start Location: {_startLocation}");
      OnlyGameScene(() => { ImGui.Text($" > Current Location: {CurrentLocation}"); }, registry);
      ImGui.Text($" > Movements: {Movements.Count}");
      OnlyGameScene(() => { ImGui.Text($" > Current Movements: {Movements.Count(x => x.From == CurrentLocation)}"); }, registry);
      ImGui.TreePop();
    }
  }

  public override void Deactivation(Registry registry, string nextSceneName) => 
    OnlyGameScene(() => { _timer.StopAndResetTimer(); }, registry);
  
  public override void Activation(Registry registry) => 
    OnlyGameScene(() =>
    {
      _timer.Activation(registry);
      CurrentLocation = _startLocation;
    }, registry);
  
  public override void Update(Registry registry) => 
    OnlyGameScene(() =>
    {
      _timer.Update(registry);
      switch (Type)
      {
        case AnimatronicType.AutoBlackouter or AnimatronicType.LightHater:
          if (!_timer.TargetTrigger()) return;
          if (SuccessfulMovement() && CurrentLocation != Location.OfficeInside) Move();
          break;
        case AnimatronicType.TriggerWaiter:
          if (GrantMovement) Move();
          break;
      }
    }, registry);
  
  public override void Draw(Registry registry) => 
    OnlyGameScene(() => { }, registry);
  
  private void OnlyGameScene(Action action, Registry registry)
  {
    if (_gameScenePointer == registry.GetSceneManager().GetCurrentScene()) action();
  }

  private bool SuccessfulMovement() => new Random().Next(1, Config.MaxAnimatronicsDifficulty) <= Difficulty;

  public void Move()
  {
    GrantMovement = false;
    List<MovementOpportunity> targetMovements = Movements.Where(x => x.From == CurrentLocation).ToList();
    _droppedChance = (float)new Random().NextDouble();
    _chances = [ 0f ];
    _chances.AddRange(targetMovements.Select(m => _chances.Last() + m.Chance));

    for (int i = 0; i < targetMovements.Count; i++)
    {
      if (!(_droppedChance >= _chances[i]) || !(_droppedChance < _chances[i + 1])) continue;
      CurrentLocation = targetMovements[i].To;
      break;
    }
  }
}