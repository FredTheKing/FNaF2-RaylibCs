using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class FNaFHost : ScriptTemplate
{
  public bool FunMode = false;
  public NightManager nightManager { get; } = new();
  public AnimatronicManager animatronicManager { get; } = new();
  
  private void OnlyGameScene(Action action, Registry registry)
  {
    if (registry.scene.All[Config.FnafSceneName.ToString()] == registry.scene.Current) action();
  }

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Fun Mode: {(FunMode ? 1 : 0)}");
    ImGui.Separator();
    nightManager.CallDebuggerInfo(registry);
    if (ImGui.TreeNode("AnimatronicManager"))
    {
      animatronicManager.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
  }

  public override void Deactivation(Registry registry, string nextSceneName) =>
    OnlyGameScene(() =>
    {
      animatronicManager.Deactivation(registry, nextSceneName);
      nightManager.Deactivation(registry, nextSceneName);
    }, registry);

  public override void Activation(Registry registry) =>
    OnlyGameScene(() =>
    {
      animatronicManager.Activation(registry);
      nightManager.Activation(registry);
    }, registry);

  public override void Update(Registry registry) =>
    OnlyGameScene(() =>
    {
      animatronicManager.Update(registry);
      nightManager.Update(registry);
    }, registry);

  public override void Draw(Registry registry) =>
    OnlyGameScene(() =>
    {
      animatronicManager.Draw(registry);
      nightManager.Draw(registry);
    }, registry);
}