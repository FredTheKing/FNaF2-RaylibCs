using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public enum OfficeDirection
{
  Left,
  Right,
  FrontFar,
  FrontNear,
  Inside
}

public class AnimatronicManager : ScriptTemplate
{
  private List<Animatronic> _animatronics = [];
  private Animatronic? _leftAnimatronic;
  private Animatronic? _rightAnimatronic;
  private Animatronic? _frontFarAnimatronic;
  private Animatronic? _frontNearAnimatronic;
  private Animatronic? _insideAnimatronic;

  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode("Animatronics"))
    {
      foreach (Animatronic animatronic in _animatronics) 
        animatronic.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
  }

  public Animatronic? GetDirectionalAnimatronic(OfficeDirection direction)
  {
    return direction switch
    {
      OfficeDirection.Left => _leftAnimatronic,
      OfficeDirection.Right => _rightAnimatronic,
      OfficeDirection.FrontFar => _frontFarAnimatronic,
      OfficeDirection.FrontNear => _frontNearAnimatronic,
      OfficeDirection.Inside => _insideAnimatronic,
      _ => null
    };
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
    {
      animatronic.Update(registry);
      _leftAnimatronic = animatronic.CurrentLocation == Location.OfficeLeft ? animatronic : null;
      _rightAnimatronic = animatronic.CurrentLocation == Location.OfficeRight ? animatronic : null;
      _frontFarAnimatronic = animatronic.CurrentLocation == Location.OfficeFrontFar ? animatronic : null;
      _frontNearAnimatronic = animatronic.CurrentLocation == Location.OfficeFrontClose ? animatronic : null;
      _insideAnimatronic = animatronic.CurrentLocation == Location.OfficeInside ? animatronic : null;
    }
  }

  public override void Draw(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronics) 
      animatronic.Draw(registry);
  }

  public void Add(Animatronic animatronic) => _animatronics.Add(animatronic);
  public List<Animatronic> GetAnimatronics() => _animatronics;
}