using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SimpleImage(Vector2 position, ImageResource resource, Color? tint = null, Vector2? new_size = null) : ObjectTemplate(position, new_size ?? resource.GetSize())
{
  protected Raylib_cs.Image _image;
  protected Color _tint = tint ?? Color.White;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {_position.X}, {_position.Y}");
    ImGui.Text($" > Size: {_image.Width}, {_image.Height}");
    
    ImGui.BeginGroup();
    ImGui.Text($" > Color:");
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(255, 0, 0, 255), _tint.R.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(0, 255, 0, 255), _tint.G.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(0, 0, 255, 255), _tint.B.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(60, 60, 60, 120), _tint.A.ToString());
    ImGui.EndGroup();
  }
  
  public new void Draw(Registry registry)
  {
    Raylib.DrawTexturePro(resource.GetMaterial(), new Rectangle(Vector2.Zero, resource.GetMaterial().Width, resource.GetMaterial().Height), new Rectangle(_position, _size), Vector2.Zero, 0, _tint);
    base.Draw(registry);
  }
}