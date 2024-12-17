using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public class AnimatronicManager : CallDebuggerInfoTemplate
{
  private List<Animatronic> _animatronics = [];


  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode("Animatronics"))
    {
      foreach (Animatronic animatronic in _animatronics) 
        animatronic.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
  }

  public void Add(Animatronic animatronic) => _animatronics.Add(animatronic);
  public List<Animatronic> GetAnimatronics() => _animatronics;
}