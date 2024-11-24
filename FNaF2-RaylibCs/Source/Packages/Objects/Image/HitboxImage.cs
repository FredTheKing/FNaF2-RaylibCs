using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Objects.Image;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Image;

public class HitboxImage(ImageResource material, Vector2 position, Color? color = null): SimpleImage(material, position)
{
  private RectangleHitbox _hitbox = new RectangleHitbox(position, new Vector2(0, 0), color ?? new Color(255, 0, 0, 123));

  public new void CallDebuggerInfo(Registry registry)
  {
    base.CallDebuggerInfo(registry);
    _hitbox.CallDebuggerInfo(registry);
  }
  
  public new void Activation(Registry registry)
  {
    _hitbox.SetBoundaries(new Vector2(_image.Width, _image.Height));
    base.Activation(registry);
  }
  
  public new void Draw(Registry registry)
  {
    base.Draw(registry);
    _hitbox.Draw(registry);
  }
}