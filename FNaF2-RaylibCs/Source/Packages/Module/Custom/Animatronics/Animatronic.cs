using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public class Animatronic : ScriptTemplate
{
  public Animatronic(Scene gameScenePointer, string name, float targetTime, List<MovementOpportunity> movements)
  {
    _gameScenePointer = gameScenePointer;
    _startLocation = movements[0].From;
    _timer = new SimpleTimer(targetTime, true);
    
    Name = name;
    Movements = movements;
    CurrentLocation = _startLocation;
  }
  
  private Scene _gameScenePointer;
  private Location _startLocation;
  private SimpleTimer _timer;
  
  public string Name;
  public List<MovementOpportunity> Movements;
  public int Difficulty = 19;
  public Location CurrentLocation;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Name: {Name}");
    ImGui.Text($" > Difficulty: {Difficulty}");
    ImGui.Text($" > Start Location: {_startLocation}");
    ImGui.Text($" > Current Location: {CurrentLocation}");
    ImGui.Text($" > Movements: {Movements.Count}");
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
      if (!_timer.TargetTrigger()) return;
      if (SuccessfulMovement()) Move();
    }, registry);
  
  public override void Draw(Registry registry) => 
    OnlyGameScene(() => { }, registry);
  
  private void OnlyGameScene(Action action, Registry registry)
  {
    if (_gameScenePointer == registry.GetSceneManager().GetCurrentScene()) action();
  }

  private bool SuccessfulMovement() => new Random().Next(1, Config.MaxAnimatronicsDifficulty) <= Difficulty;

  private void Move()
  {
    List<MovementOpportunity> targetMovements = Movements.Where(x => x.From == CurrentLocation).ToList();
    float droppedChance = (float)new Random().NextDouble();
    List<float> chances = [ 0f ];
    chances.AddRange(targetMovements.Select(m => chances.Last() + m.Chance));

    for (int i = 0; i < targetMovements.Count; i++)
    {
      if (!(droppedChance >= chances[i]) || !(droppedChance < chances[i + 1])) continue;
      CurrentLocation = targetMovements[i].To;
      break;
    }
  }
}