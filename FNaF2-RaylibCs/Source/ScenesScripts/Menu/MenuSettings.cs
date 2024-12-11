using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.ScenesScripts.Menu;

public class MenuSettings : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.SettingsFullscreenCheckbox!.SetChecked(registry.GetFNaF().FullscreenMode);
    Registration.Objects.SettingsVsyncCheckbox!.SetChecked(registry.GetFNaF().VsyncMode);
    Registration.Objects.SettingsVolumeSlider!.SetValue(registry.GetFNaF().Volume);
    Registration.Objects.SettingsFunModeCheckbox!.SetChecked(registry.GetFNaF().FunMode);
    Registration.Objects.SettingsDebugModeCheckbox!.SetChecked(registry.GetDebugMode());
  }

  private void FullscreenFunc(Registry registry)
  {
    if (registry.GetFNaF().FullscreenMode && !Raylib.IsWindowFullscreen()) Raylib.ToggleFullscreen();
    else if (!registry.GetFNaF().FullscreenMode && Raylib.IsWindowFullscreen()) Raylib.ToggleFullscreen();
  }

  private void VsyncFunc(Registry registry)
  {
    Raylib.SetTargetFPS(registry.GetFNaF().VsyncMode ? Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) : 0);
  }

  public override void Update(Registry registry)
  {
    bool previousVsync = registry.GetFNaF().VsyncMode;
    
    registry.SetDebugMode(Registration.Objects.SettingsDebugModeCheckbox!.GetChecked());
    registry.GetFNaF().FullscreenMode = Registration.Objects.SettingsFullscreenCheckbox!.GetChecked();
    registry.GetFNaF().VsyncMode = Registration.Objects.SettingsVsyncCheckbox!.GetChecked();
    registry.GetFNaF().Volume = Registration.Objects.SettingsVolumeSlider!.GetValue();
    registry.GetFNaF().FunMode = Registration.Objects.SettingsFunModeCheckbox!.GetChecked();
    
    FullscreenFunc(registry);
    
    if (previousVsync != registry.GetFNaF().VsyncMode) VsyncFunc(registry);
  }
}