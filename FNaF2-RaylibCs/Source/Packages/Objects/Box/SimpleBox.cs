using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Box;

public class SimpleBox(Vector2 position, Vector2 size, Color color) : ObjectTemplate(position, size)
{
  protected Color _color = color;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {_position.X}, {_position.Y}");
    ImGui.Text($" > Size: {_size.X}, {_size.Y}");
    
    ImGui.BeginGroup();
      ImGui.Text($" > Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), _color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), _color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), _color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), _color.A.ToString());
    ImGui.EndGroup();
  }

  public override void Draw(Registry registry)
  {
    Raylib.DrawRectangleV(_position, _size, _color);
    base.Update(registry);
  }
}