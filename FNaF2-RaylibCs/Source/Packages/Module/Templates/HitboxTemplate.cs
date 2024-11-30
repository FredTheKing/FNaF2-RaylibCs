using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.Templates;

public class HitboxTemplate : ObjectTemplate
{
  protected RectangleHitbox _hitbox;

  protected HitboxTemplate() : base() => _hitbox = new RectangleHitbox(_position, _size, new Color{R = 200, G = 200, B = 255, A = 123});
  protected HitboxTemplate(Color color) : base() => _hitbox = new RectangleHitbox(_position, _size, color);
  protected HitboxTemplate(Vector2 position, Vector2 size) : base(position, size) => _hitbox = new RectangleHitbox(_position, _size, new Color{R = 200, G = 200, B = 255, A = 123});
  protected HitboxTemplate(Vector2 position, Vector2 size, Color color) : base(position, size) => _hitbox = new RectangleHitbox(_position, _size, color);
  
  public new void CallDebuggerInfo(Registry registry) => _hitbox.CallDebuggerInfo(registry);
  
  public new void SetPosition(Vector2 new_position)
  {
    base.SetPosition(new_position);
    _hitbox.SetPosition(new_position);
  }
  
  public new void SetSize(Vector2 new_size)
  {
    base.SetSize(new_size);
    _hitbox.SetSize(new_size);
  }

  public new void Draw(Registry registry)
  {
    base.Draw(registry);
    _hitbox.Draw(registry);
  }
}