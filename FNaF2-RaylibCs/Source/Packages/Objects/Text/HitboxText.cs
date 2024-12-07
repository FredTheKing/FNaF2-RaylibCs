using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using Raylib_cs;
namespace FNaF2_RaylibCs.Source.Packages.Objects.Text;

public class HitboxText : SimpleText
{
  protected RectangleHitbox _hitbox;
  
  public HitboxText(Vector2 position, Vector2 size, int font_size, string text, Color color, bool align_center_v = false, bool align_center_h = false) : base(position, size, font_size, text, color, align_center_v, align_center_h) { InitHitbox(position, size); }
  public HitboxText(Vector2 position, Vector2 size, int font_size, string text, Color color, FontResource font, bool align_center_v = false, bool align_center_h = false) : base(position, size, font_size, text, color, font, align_center_v, align_center_h) { InitHitbox(position, size); }
  public HitboxText(Vector2 position, Vector2 size, int font_size, Color color, bool align_center_v = false, bool align_center_h = false) : base(position, size, font_size, color, align_center_v, align_center_h) { InitHitbox(position, size); }
  public HitboxText(Vector2 position, Vector2 size, int font_size, Color color, FontResource font, bool align_center_v = false, bool align_center_h = false) : base(position, size, font_size, color, font, align_center_v, align_center_h) { InitHitbox(position, size); }

  private void InitHitbox(Vector2 position, Vector2 size) => _hitbox = new RectangleHitbox(position, size, new Color(255, 0, 0, 123));

  public RectangleHitbox GetHitbox() => _hitbox;
  
  public override void CallDebuggerInfo(Registry registry)
  {
    _hitbox.CallDebuggerInfo(registry);
    base.CallDebuggerInfo(registry);
  }

  public override void Activation(Registry registry)
  {
    _hitbox.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    _hitbox.Update(registry);
    base.Update(registry);
  }
  
  public override void Draw(Registry registry)
  {
    _hitbox.Draw(registry);
    base.Draw(registry);
  }
}