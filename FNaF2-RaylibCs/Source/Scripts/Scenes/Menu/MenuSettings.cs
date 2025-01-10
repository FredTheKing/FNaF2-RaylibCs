using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;

public class MenuSettings : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.WhiteBlinko!.GetScript()!.Play();
    Registration.Objects.SettingsFullscreenCheckbox!.Checked = registry.scene.Fullscreen;
    Registration.Objects.SettingsVsyncCheckbox!.Checked = registry.scene.Vsync;
    Registration.Objects.SettingsVolumeSlider!.Value = registry.scene.MasterVolume;
    Registration.Objects.SettingsFunModeCheckbox!.Checked = registry.fnaf.FunMode;
    Registration.Objects.SettingsDebugModeCheckbox!.Checked = registry.DebugMode;
  }

  private void FullscreenFunc(Registry registry)
  {
    if (registry.scene.Fullscreen && !Raylib.IsWindowFullscreen()) Raylib.ToggleFullscreen();
    else if (!registry.scene.Fullscreen && Raylib.IsWindowFullscreen()) Raylib.ToggleFullscreen();
  }

  private void VsyncFunc(Registry registry)
  {
    Raylib.SetTargetFPS(registry.scene.Vsync ? Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) : Config.WindowTargetFramerate);
  }

  public override void Update(Registry registry)
  {
    bool previousVsync = registry.scene.Vsync;
    
    if (Registration.Objects.SettingsVsyncCheckbox!.Hitbox.GetMouseHover())
      Registration.Objects.SettingsFpsShowText!.SetCurrentFrameColor(Color.Gray);
    
    registry.DebugMode = Registration.Objects.SettingsDebugModeCheckbox!.Checked;
    registry.scene.Fullscreen = Registration.Objects.SettingsFullscreenCheckbox!.Checked;
    registry.scene.Vsync = Registration.Objects.SettingsVsyncCheckbox!.Checked;
    registry.scene.MasterVolume = Registration.Objects.SettingsVolumeSlider!.Value;
    registry.fnaf.FunMode = Registration.Objects.SettingsFunModeCheckbox!.Checked;
    
    FullscreenFunc(registry);
    Registration.Objects.SettingsFpsShowText!.SetText("FPS: " + Raylib.GetFPS());
    
    if (previousVsync != registry.scene.Vsync) VsyncFunc(registry);
  }
}