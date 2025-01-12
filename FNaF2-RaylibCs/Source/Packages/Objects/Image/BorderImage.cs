using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class BorderImage(Vector2 position, ImageResource resource, float thickness, Color bordColor, Color? tint = null, Vector2? newSize = null, float rotation = 0) : SimpleImage(position, resource, tint, newSize, rotation)
{
  public override void CallDebuggerInfo(Registry registry)
  {
    base.CallDebuggerInfo(registry);
    ImGui.Separator();
    ImGui.Text(" > Thickness: " + thickness);
    ImGui.Text(" > Pos: " + Position);
    ImGui.Text(" > Size: " + Size);
    ImGui.Text(" > Border Color: " + bordColor);
  }

  public override void Draw(Registry registry)
  {
    base.Draw(registry);
    Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size), thickness, bordColor);
  }
}