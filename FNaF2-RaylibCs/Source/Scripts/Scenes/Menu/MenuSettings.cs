using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;

public class MenuSettings : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.SettingsFullscreenCheckbox!.SetChecked(registry.GetSceneManager().GetFullscreen());
    Registration.Objects.SettingsVsyncCheckbox!.SetChecked(registry.GetSceneManager().GetVsync());
    Registration.Objects.SettingsVolumeSlider!.SetValue(registry.GetSceneManager().GetMasterVolume());
    Registration.Objects.SettingsFunModeCheckbox!.SetChecked(registry.GetFNaF().FunMode);
    Registration.Objects.SettingsDebugModeCheckbox!.SetChecked(registry.GetDebugMode());
  }

  private void FullscreenFunc(Registry registry)
  {
    if (registry.GetSceneManager().GetFullscreen() && !Raylib.IsWindowFullscreen()) Raylib.ToggleFullscreen();
    else if (!registry.GetSceneManager().GetFullscreen() && Raylib.IsWindowFullscreen()) Raylib.ToggleFullscreen();
  }

  private void VsyncFunc(Registry registry)
  {
    Raylib.SetTargetFPS(registry.GetSceneManager().GetVsync() ? Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) : Config.WindowTargetFramerate);
  }

  public override void Update(Registry registry)
  {
    bool previousVsync = registry.GetSceneManager().GetVsync();
    
    if (Registration.Objects.SettingsVsyncCheckbox!.GetHitbox().GetMouseHover())
      Registration.Objects.SettingsFpsShowText!.SetCurrentFrameColor(Color.Gray);
    
    registry.SetDebugMode(Registration.Objects.SettingsDebugModeCheckbox!.GetChecked());
    registry.GetSceneManager().SetFullscreen(Registration.Objects.SettingsFullscreenCheckbox!.GetChecked());
    registry.GetSceneManager().SetVsync(Registration.Objects.SettingsVsyncCheckbox.GetChecked());
    registry.GetSceneManager().SetMasterVolume(Registration.Objects.SettingsVolumeSlider!.GetValue());
    registry.GetFNaF().FunMode = Registration.Objects.SettingsFunModeCheckbox!.GetChecked();
    
    FullscreenFunc(registry);
    Registration.Objects.SettingsFpsShowText!.SetText("FPS: " + Raylib.GetFPS());
    
    if (previousVsync != registry.GetSceneManager().GetVsync()) VsyncFunc(registry);
  }
}