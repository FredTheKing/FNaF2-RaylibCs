using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;
public class GameLoading : ScriptTemplate
{
  private bool gotoGame;
  private SimpleTimer _timer = new(5, true);
  
  public override void Activation(Registry registry)
  {
    _timer.Activation(registry);
    gotoGame = false; 
    Registration.Objects.LoadingNightText!.SetText("Night " + registry.GetFNaF().GetNightManager().GetUpcomingNight());
    Registration.Objects.LoadingClockThingo!.SetTint(Color.Blank);
  }

  public override void Update(Registry registry)
  {
    if (gotoGame) registry.GetSceneManager().ChangeScene(registry, Config.Scenes.GameMain);
    _timer.Update(registry);

    if (!_timer.TargetTrigger()) return;
    Registration.Objects.LoadingClockThingo!.SetTint(Color.White);
    gotoGame = true;
  }
}