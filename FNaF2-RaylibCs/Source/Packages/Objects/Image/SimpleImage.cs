using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SimpleImage : ObjectTemplate
{
  public SimpleImage(Vector2 position, ImageResource resource, Color? tint = null, Vector2? newSize = null) : base(position, newSize ?? resource.GetSize()) { this.Resource = resource; Tint = tint ?? Color.White; }
  public SimpleImage(Vector2 position, Vector2 originalSize, Color? tint = null, Vector2? newSize = null) : base(position, newSize ?? originalSize) { Tint = tint ?? Color.White; }

  protected Color Tint;
  protected ImageResource? Resource;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {Position.X}, {Position.Y}");
    if (Resource is not null) ImGui.Text($" > Size: {Resource.GetSize().X}, {Resource.GetSize().Y}");
    ImGui.BeginGroup();
    ImGui.Text($" > Color:");
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(255, 0, 0, 255), Tint.R.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(0, 255, 0, 255), Tint.G.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(0, 0, 255, 255), Tint.B.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(60, 60, 60, 120), Tint.A.ToString());
    ImGui.EndGroup();
  }
  
  public override void Draw(Registry registry)
  {
    if (Resource is null) return;
    Raylib.DrawTexturePro(Resource.GetMaterial(), new Rectangle(Vector2.Zero, Resource.GetMaterial().Width, Resource.GetMaterial().Height), new Rectangle(Position, Size), Vector2.Zero, 0, Tint);
    base.Draw(registry);
  }
}