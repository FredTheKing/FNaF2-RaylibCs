using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Box;

public class DebugBox(Vector2 position, Vector2 size, Color color) : SimpleBox(position, size, color)
{
  public override void Draw(Registry registry)
  {
    if (registry.DebugMode) base.Draw(registry);
  }
}