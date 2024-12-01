using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SimpleImage : ObjectTemplate
{
  public SimpleImage(Vector2 position, ImageResource resource, Color? tint = null, Vector2? new_size = null) : base(position, new_size ?? resource.GetSize()) { this._resource = resource; _tint = tint ?? Color.White; }
  public SimpleImage(Vector2 position, Vector2 original_size, Color? tint = null, Vector2? new_size = null) : base(position, new_size ?? original_size) { _tint = tint ?? Color.White; }

  protected Color _tint;
  protected ImageResource _resource = null!;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {_position.X}, {_position.Y}");
    if (_resource is not null) ImGui.Text($" > Size: {_resource.GetSize().X}, {_resource.GetSize().Y}");
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
    if (_resource is null) return;
    Raylib.DrawTexturePro(_resource.GetMaterial(), new Rectangle(Vector2.Zero, _resource.GetMaterial().Width, _resource.GetMaterial().Height), new Rectangle(_position, _size), Vector2.Zero, 0, _tint);
    base.Draw(registry);
  }
}