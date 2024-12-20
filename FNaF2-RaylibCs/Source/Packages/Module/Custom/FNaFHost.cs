using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class FNaFHost : ScriptTemplate
{
  public bool FunMode = false;
  private NightManager _nightManager = new();
  private AnimatronicManager _animatronicManager = new();
  
  public NightManager GetNightManager() => _nightManager;
  public AnimatronicManager GetAnimatronicManager() => _animatronicManager;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Fun Mode: {(FunMode ? 1 : 0)}");
    ImGui.Separator();
    _nightManager.CallDebuggerInfo(registry);
    if (ImGui.TreeNode("AnimatronicManager"))
    {
      _animatronicManager.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
  }

  public override void Deactivation(Registry registry, string nextSceneName)
  {
    _animatronicManager.Deactivation(registry, nextSceneName);
  }

  public override void Activation(Registry registry)
  {
    _animatronicManager.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    _animatronicManager.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    _animatronicManager.Draw(registry);
  }
}