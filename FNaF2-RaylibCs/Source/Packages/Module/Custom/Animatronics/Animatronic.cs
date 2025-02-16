using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using FNaF2_RaylibCs.Source.Scripts.Objects;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public record struct DifficultyBehavior(int night, int hour, int changeTo)
{
  // struct for animatronics to change difficulty as night goes
    // night = on what night
    // hour = on what hour
    // changeTo = on what difficulty change to
}

public record struct MovementOpportunity(Location from, Location to, float chance)
{
  // struct for animatronics to know where to go from where and with what chance
    // from = From what location
    // to = To what location
    // chance = Chance of him going there
}

public record struct ExcludeOpportunity(Location planningTo, string who, Location where)
{
  // struct for animatronics to avoid going to certain locations if some animatronics are on specific locations
    // planningTo = Where current animatronic planning to go
    // who = Which animatronic compare to
    // where = Where selected animatronic need to be for current animatronic to fail his location change
}

public record struct GrantOpportunity(string who, Location where)
{
  // struct for animatronics to ignore existence of presented animatronic on certain location (therefore not getting into queue)
    // who = What animatronic to ignore
    // where = When trying to go on this location
}

public enum AnimatronicType
{
  AutoBlackouter,
  TriggerWaiter
}

public class Animatronic : ScriptTemplate
{
  public Animatronic(string name, float targetTime, AnimatronicType type, Location afkRoom, List<DifficultyBehavior> difficulties, List<MovementOpportunity> movements, List<GrantOpportunity>? grants = null, List<ExcludeOpportunity>? excludes = null)
  {
    _startLocation = movements[0].from;
    _autoerBrakeLocation = afkRoom;
    Timer = new SimpleTimer(targetTime, true);
    NextQueue = null;

    if (Type is AnimatronicType.TriggerWaiter) _autoer = false;
    
    Name = name;
    Difficulties = difficulties;
    Movements = movements;
    Excludes = excludes;
    Grants = grants;
    Type = type;
    CurrentLocation = _startLocation;
  }
  
  private Location _autoerBrakeLocation;
  private Location _startLocation;
  private bool _locationChanged;
  private bool _autoer = true;
  private float _droppedChance = -1f;
  private List<float> _chances = [];
  private bool _forceMove;
  private static float _cameraHateringDefault = 3000f;
  
  public float CameraHatering = _cameraHateringDefault;
  public string Name;
  public SimpleTimer Timer;
  public List<DifficultyBehavior> Difficulties;
  public List<MovementOpportunity> Movements;
  public List<ExcludeOpportunity>? Excludes;
  public List<GrantOpportunity>? Grants;
  public AnimatronicType Type;
  public int Difficulty;
  public (Animatronic, Location)? NextQueue;
  public Location? PlanningLocation;
  public Location CurrentLocation;
  public bool waitingToGoIntoOffice { get; private set; }
  
  public void SetDifficulty(int difficulty) => Difficulty = difficulty;

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
      ImGui.Text($" > Current Location: {CurrentLocation}");
      ImGui.Text($" > Waiting To Go Into Office: {waitingToGoIntoOffice}");
      ImGui.Text($" > Movements: {Movements.Count}");
      ImGui.Text($" > Current Movements: {Movements.Count(x => x.from == CurrentLocation)}");
      ImGui.TreePop();
    }
  }

  private void UpdateDifficulty(Registry registry)
  {
    if (Difficulties is [])
    {
      Difficulty = Config.MaxAnimatronicsDifficulty;
      return;
    }

    foreach (DifficultyBehavior behavior in Difficulties)
    {
      if (registry.fnaf.nightManager.current == behavior.night && registry.fnaf.nightManager.am == behavior.hour) Difficulty = behavior.changeTo;
    }
  }

  public override void Deactivation(Registry registry, string nextSceneName) => 
    Timer.StopAndResetTimer();

  public override void Activation(Registry registry)
  {
    Timer.Activation(registry);
    CurrentLocation = _startLocation;
    CameraHatering = _cameraHateringDefault;
    NextQueue = null;
  }

  public override void Update(Registry registry)
  {
    UpdateDifficulty(registry);
    Timer.Update(registry);
    _autoer = CurrentLocation != _autoerBrakeLocation;
      
    if (CurrentLocation == Location.OfficeInside && Registration.Objects.GameUiCamera!.GetScript()!.State == States.Up && CameraHatering > 0) CameraHatering = Math.Clamp(CameraHatering - 300 * Raylib.GetFrameTime(), 0, 3000f);
      
    if (waitingToGoIntoOffice && Registration.Objects.GameUiCamera!.GetScript()!.State == States.Up)
    {
      waitingToGoIntoOffice = false;
      Timer.StartTimer();
      Move(registry);
    }
      
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
        if (_forceMove) Move(registry);
        break;
    }
  }
  
  public void TriggerMove() => _forceMove = true;

  private bool SuccessfulMovement() => new Random().Next(1, Config.MaxAnimatronicsDifficulty) <= Difficulty;

  public void Move(Registry registry)
  {
    _forceMove = false;
    CameraHatering = _cameraHateringDefault;
    if (PlanningLocation is null)
    {
      List<MovementOpportunity> targetMovements = Movements.Where(x => x.from == CurrentLocation).ToList();
      _droppedChance = (float)new Random().NextDouble();
      _chances = [0f];
      _chances.AddRange(targetMovements.Select(m => _chances.Last() + m.chance));

      for (int i = 0; i < targetMovements.Count; i++)
      {
        if (!(_droppedChance >= _chances[i]) || !(_droppedChance < _chances[i + 1])) continue;
        PlanningLocation = targetMovements[i].to;
        break;
      }
    }
    
    if (PlanningLocation == Location.OfficeInside)
    {
      if (Registration.Objects.GameUiCamera!.GetScript()!.State != States.Up)
      {
        Timer.StopAndResetTimer();
        waitingToGoIntoOffice = true;
        return;
      }
    }
    
    if (Excludes is not null && Excludes.Any(x => x.planningTo == PlanningLocation && registry.fnaf.animatronicManager.all.FirstOrDefault(a => a.Name == x.who && a.CurrentLocation == x.where) is not null)) return;
    
    foreach (Animatronic animatronic in registry.fnaf.animatronicManager.all)
    {
      if (animatronic.CurrentLocation != PlanningLocation || animatronic == this) continue;
      if (animatronic.Grants != null && (bool)Grants?.Any(a => a.who == animatronic.Name && a.where == PlanningLocation)) continue;
      
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