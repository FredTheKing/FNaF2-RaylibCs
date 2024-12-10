using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;

namespace FNaF2_RaylibCs.Source.ScenesScripts.Menu;

public class MenuSettings : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.SettingsDebugModeCheckbox!.SetChecked(registry.GetDebugMode());
  }

  public override void Update(Registry registry)
  {
    registry.SetDebugMode(Registration.Objects.SettingsDebugModeCheckbox!.GetChecked());
  }
}