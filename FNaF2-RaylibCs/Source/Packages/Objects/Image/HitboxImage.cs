using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class HitboxImage(Vector2 position, ImageResource resource, Color? color = null): SimpleImage(position, resource)
{
  protected RectangleHitbox Hitbox = new(position, new Vector2(0, 0), color);
  
  public override void CallDebuggerInfo(Registry registry)
  {
    base.CallDebuggerInfo(registry);
    Hitbox.CallDebuggerInfo(registry);
  }
  
  public RectangleHitbox GetHitbox() => Hitbox;
  
  public override void Deactivation(Registry registry, string nextSceneName)
  {
    Hitbox.Deactivation(registry, nextSceneName);
    base.Deactivation(registry, nextSceneName);
  }

  public override void Activation(Registry registry)
  {
    Hitbox.SetBoundaries(Resource!.GetSize());
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