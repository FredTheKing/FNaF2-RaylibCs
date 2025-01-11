using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public class AnimatronicManager : ScriptTemplate
{
  public List<Animatronic> all { get; } = [];

  public override void CallDebuggerInfo(Registry registry)
  {
    foreach (Animatronic animatronic in all) 
      animatronic.CallDebuggerInfo(registry);
    ImGui.TreePop();
  }

  public override void Deactivation(Registry registry, string nextSceneName)
  {
    foreach (Animatronic animatronic in all) 
      animatronic.Deactivation(registry, nextSceneName);
  }

  public override void Activation(Registry registry)
  {
    foreach (Animatronic animatronic in all) 
      animatronic.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    foreach (Animatronic animatronic in all.OrderBy(a => Guid.NewGuid()))
      animatronic.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    foreach (Animatronic animatronic in all) 
      animatronic.Draw(registry);
  }

  public void Add(Animatronic animatronic) => all.Add(animatronic);
}