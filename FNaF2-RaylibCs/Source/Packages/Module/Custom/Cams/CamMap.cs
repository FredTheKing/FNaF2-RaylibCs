using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Cams;

public class CamMap(SimpleImage map, ImageStackResource camBg, FontResource font, List<Vector2> camsPositions) : ObjectTemplate(map.GetPosition(), map.GetSize())
{
  private readonly List<CamUnit> _camsUnits = camsPositions.Select((pos, i) => new CamUnit(pos, camBg, font, i+1)).ToList();
  
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Separator();
    if (ImGui.TreeNode("Map"))
    {
      map.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
    foreach (CamUnit unit in _camsUnits)
      unit.CallDebuggerInfo(registry);
  }
  
  public int GetSelectedCam() => _camsUnits.FindIndex(u => u.Selected);

  public override void Activation(Registry registry)
  {
    _camsUnits[0].Selected = true;
  }

  public override void Update(Registry registry)
  {
    map.Update(registry);
    foreach (CamUnit unit in _camsUnits)
    {
      unit.Update(registry);
      if (!unit.Demanding) continue;
      _camsUnits.ForEach(u => u.Selected = false);
      Registration.Objects.WhiteBlinko!.GetScript()!.Play();
      unit.Selected = true;
      unit.Demanding = false;
    }
  }
  
  public override void Draw(Registry registry)
  {
    map.Draw(registry);
    _camsUnits.ForEach(u => u.Draw(registry));
  }
}