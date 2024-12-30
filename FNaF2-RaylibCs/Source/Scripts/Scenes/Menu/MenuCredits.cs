using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;

public class MenuCredits : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.WhiteBlinko!.GetScript()!.Play();
  }
}