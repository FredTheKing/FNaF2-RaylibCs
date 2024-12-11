using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class FNaFHost : CallDebuggerInfoTemplate
{
  public bool FullscreenMode = false;
  public bool VsyncMode = true;
  public float Volume = .5f;
  public bool FunMode = false;
  private NightManager _nightManager = new();
  private AnimatronicManager _animatronicManager = new();
  
  public NightManager GetNightManager() => _nightManager;
  public AnimatronicManager GetAnimatronicManager() => _animatronicManager;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Fullscreen Mode: {(FullscreenMode ? 1 : 0)}");
    ImGui.Text($" > Vsync Mode: {(VsyncMode ? 1 : 0)}");
    ImGui.Text($" > Volume: {Volume}");
    ImGui.Text($" > Fun Mode: {(FunMode ? 1 : 0)}");
    ImGui.Separator();
    _nightManager.CallDebuggerInfo(registry);
    _animatronicManager.CallDebuggerInfo(registry);
  }
}