using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;

namespace FNaF2_RaylibCs.Source.ScenesScripts.Game;
public class GameLoading : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.LoadingNightText!.SetText("Night " + registry.GetFNaF().GetNightManager().GetUpcomingNight());
  }
}