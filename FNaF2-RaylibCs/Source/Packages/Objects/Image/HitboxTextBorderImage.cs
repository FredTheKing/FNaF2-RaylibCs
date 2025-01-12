using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class HitboxTextBorderImage : BorderImage
{
  public HitboxTextBorderImage(Vector2 position, string text, FontResource font, ImageResource image, float thickness, Color bordColor, Color? tint = null, Vector2? newSize = null, float rotation = 0) : base(position, image, thickness, bordColor, tint, newSize, rotation) {
    Text = new(Position + new Vector2(Size.X - 60, Size.Y - 40), new Vector2(60, 40), 34, text, Color.White, font, true, true);
    Hitbox = new(Position, Size);
  }

  public SimpleText Text;
  public RectangleHitbox Hitbox;

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
    Text.Draw(registry);
  }
}