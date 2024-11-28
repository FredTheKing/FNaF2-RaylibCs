using System.Numerics;
using ImGuiNET;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;
namespace RaylibArteSonat.Source.Packages.Objects.Image;

public class SimpleImage(ImageResource resource, Vector2 position, Color? tint = null) : ObjectTemplate(position, new Vector2(resource.GetRenderMaterial().Width, resource.GetRenderMaterial().Height))
{
  protected ImageResource Resource = resource;
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
    Raylib.DrawTexture(Resource.GetRenderMaterial(), (int)_position.X, (int)_position.Y, Color.White);
    base.Draw(registry);
  }
}