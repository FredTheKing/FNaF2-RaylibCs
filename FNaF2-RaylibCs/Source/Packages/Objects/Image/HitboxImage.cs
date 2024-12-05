using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class HitboxImage(ImageResource resource, Vector2 position, Color? color = null): SimpleImage(position, resource)
{
  private RectangleHitbox _hitbox = new RectangleHitbox(position, new Vector2(0, 0), color ?? new Color(255, 0, 0, 123));
  
  public override void CallDebuggerInfo(Registry registry)
  {
    base.CallDebuggerInfo(registry);
    _hitbox.CallDebuggerInfo(registry);
  }
  
  public override void Activation(Registry registry)
  {
    _hitbox.SetBoundaries(_resource.GetSize());
    base.Activation(registry);
  }
  
  public override void Draw(Registry registry)
  {
    base.Draw(registry);
    _hitbox.Draw(registry);
  }
}