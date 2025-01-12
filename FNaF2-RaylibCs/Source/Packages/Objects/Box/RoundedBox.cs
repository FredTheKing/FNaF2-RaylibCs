using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Box;

public class RoundedBox(Vector2 position, Vector2 size, Color color, float roundness) : SimpleBox(position, size, color)
{
  public float Roundness = roundness;
  
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text(" > Roundness: " + roundness);
    base.CallDebuggerInfo(registry);
  }

  public override void Draw(Registry registry)
  {
    Raylib.DrawRectangleRounded(new Rectangle(Position, Size), Roundness, 32, color);
    ScriptInstance?.Draw(registry);
  }
}