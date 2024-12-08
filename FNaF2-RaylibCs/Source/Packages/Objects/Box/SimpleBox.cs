using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Box;

public class SimpleBox(Vector2 position, Vector2 size, Color color) : ObjectTemplate(position, size)
{
  protected Color Color = color;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {Position.X}, {Position.Y}");
    ImGui.Text($" > Size: {Size.X}, {Size.Y}");
    
    ImGui.BeginGroup();
      ImGui.Text($" > Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), Color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), Color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), Color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), Color.A.ToString());
    ImGui.EndGroup();
  }

  public override void Draw(Registry registry)
  {
    Raylib.DrawRectangleV(Position, Size, Color);
    base.Update(registry);
  }
}