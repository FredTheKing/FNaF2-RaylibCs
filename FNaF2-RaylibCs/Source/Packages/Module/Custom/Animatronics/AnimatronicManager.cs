using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public class AnimatronicManager : ScriptTemplate
{
  private List<Animatronic> _animatronics = [];

  public override void CallDebuggerInfo(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronics) 
      animatronic.CallDebuggerInfo(registry);
    ImGui.TreePop();
  }

  public override void Deactivation(Registry registry, string nextSceneName)
  {
    foreach (Animatronic animatronic in _animatronics) 
      animatronic.Deactivation(registry, nextSceneName);
  }

  public override void Activation(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronics) 
      animatronic.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronics)
      animatronic.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronics) 
      animatronic.Draw(registry);
  }

  public void Add(Animatronic animatronic) => _animatronics.Add(animatronic);
  public List<Animatronic> GetAnimatronics() => _animatronics;
}