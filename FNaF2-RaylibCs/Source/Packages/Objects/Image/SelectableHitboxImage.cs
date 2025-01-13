using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SelectableHitboxImage : SelectableImage
{
  public RectangleHitbox Hitbox;

  public SelectableHitboxImage(Vector2 position, ImageStackResource resource, Color? tint = null, Vector2? newSize = null) : base(position, resource, tint, newSize)
  {
    Hitbox = new(Position, Size);
  }

  public override void CallDebuggerInfo(Registry registry)
  {
    base.CallDebuggerInfo(registry);
    Hitbox.CallDebuggerInfo(registry);
  }

  public override void SetPosition(Vector2 position)
  {
    Hitbox.SetPosition(position);
    base.SetPosition(position);
  }

  public override void Activation(Registry registry)
  {
    Hitbox.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    Hitbox.Update(registry);
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    base.Draw(registry);
    Hitbox.Draw(registry);
  }
}