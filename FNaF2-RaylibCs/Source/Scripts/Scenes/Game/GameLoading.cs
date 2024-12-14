using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;
public class GameLoading : ScriptTemplate
{
  private SimpleTimer _timer = new(5, true);
  
  public override void Activation(Registry registry)
  {
    _timer.Activation(registry);
    Registration.Objects.LoadingNightText!.SetText("Night " + registry.GetFNaF().GetNightManager().GetUpcomingNight());
  }

  public override void Update(Registry registry)
  {
    _timer.Update(registry);

    if (_timer.EndedTrigger())
      registry.GetSceneManager().ChangeScene(registry, Config.Scenes.GameMain);
  }
}