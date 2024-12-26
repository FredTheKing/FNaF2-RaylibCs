using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public enum OfficeDirection
{
  Left,
  Right,
  Front,
  Inside
}

public class AnimatronicManager : ScriptTemplate
{
  private List<Animatronic> _animatronics = [];
  private List<Animatronic>? _leftAnimatronic;
  private List<Animatronic>? _rightAnimatronic;
  private List<Animatronic>? _frontAnimatronic;
  private List<Animatronic>? _insideAnimatronic;

  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode("Directions Filled"))
    {
      ImGui.Text($" > Left: {(_leftAnimatronic != null ? _leftAnimatronic.Select(x => x.Name) : "-")}");
      ImGui.Text($" > Right: {(_rightAnimatronic != null ? _rightAnimatronic.Select(x => x.Name) : "-")}");
      ImGui.Text($" > Front: {(_frontAnimatronic != null ? _frontAnimatronic.Select(x => x.Name) : "-")}");
      ImGui.Text($" > Inside: {(_insideAnimatronic != null ? _insideAnimatronic.Select(x => x.Name) : "-")}");
      ImGui.TreePop();
    }
    ImGui.Separator();
    foreach (Animatronic animatronic in _animatronics) 
      animatronic.CallDebuggerInfo(registry);
    ImGui.TreePop();
  }

  public List<Animatronic>? GetDirectionalAnimatronic(OfficeDirection direction)
  {
    return direction switch
    {
      OfficeDirection.Left => _leftAnimatronic,
      OfficeDirection.Right => _rightAnimatronic,
      OfficeDirection.Front => _frontAnimatronic,
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
      _leftAnimatronic = _animatronics.Where(a => a.CurrentLocation == Location.OfficeFront).ToList();
      _rightAnimatronic = _animatronics.Where(a => a.CurrentLocation == Location.OfficeFront).ToList();
      _frontAnimatronic = _animatronics.Where(a => a.CurrentLocation == Location.OfficeFront).ToList();
      _insideAnimatronic = _animatronics.Where(a => a.CurrentLocation == Location.OfficeInside).ToList();
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