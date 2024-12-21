using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public enum OfficeDirection
{
  Left,
  Right,
  FrontFar,
  FrontClose,
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
    if (ImGui.TreeNode("Directions Filled"))
    {
      ImGui.Text($" > Left: {(_leftAnimatronic != null ? _leftAnimatronic.Name : "-")}");
      ImGui.Text($" > Right: {(_rightAnimatronic != null ? _rightAnimatronic.Name : "-")}");
      ImGui.Text($" > Front Far: {(_frontFarAnimatronic != null ? _frontFarAnimatronic.Name : "-")}");
      ImGui.Text($" > Front Close: {(_frontNearAnimatronic != null ? _frontNearAnimatronic.Name : "-")}");
      ImGui.Text($" > Inside: {(_insideAnimatronic != null ? _insideAnimatronic.Name : "-")}");
      ImGui.TreePop();
    }
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
      OfficeDirection.FrontClose => _frontNearAnimatronic,
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
      
      _leftAnimatronic = _leftAnimatronic?.CurrentLocation == Location.OfficeLeft ? _leftAnimatronic : _animatronics.FirstOrDefault(a => a.CurrentLocation == Location.OfficeLeft);
      _rightAnimatronic = _rightAnimatronic?.CurrentLocation == Location.OfficeRight ? _rightAnimatronic : _animatronics.FirstOrDefault(a => a.CurrentLocation == Location.OfficeRight);
      _frontFarAnimatronic = _frontFarAnimatronic?.CurrentLocation == Location.OfficeFrontFar ? _frontFarAnimatronic : _animatronics.FirstOrDefault(a => a.CurrentLocation == Location.OfficeFrontFar);
      _frontNearAnimatronic = _frontNearAnimatronic?.CurrentLocation == Location.OfficeFrontClose ? _frontNearAnimatronic : _animatronics.FirstOrDefault(a => a.CurrentLocation == Location.OfficeFrontClose);
      _insideAnimatronic = _insideAnimatronic?.CurrentLocation == Location.OfficeInside ? _insideAnimatronic : _animatronics.FirstOrDefault(a => a.CurrentLocation == Location.OfficeInside);
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