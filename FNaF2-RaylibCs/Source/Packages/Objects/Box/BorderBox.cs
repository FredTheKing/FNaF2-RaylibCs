using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Box;

public class BorderBox(Vector2 position, Vector2 size, Color color, int thickness) : SimpleBox(position, size, color)
{
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text(" > Thickness: " + thickness);
    base.CallDebuggerInfo(registry);
  }

  public override void Draw(Registry registry)
  {
    Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size), thickness, Color);
    ScriptInstance?.Draw(registry);
  }
}