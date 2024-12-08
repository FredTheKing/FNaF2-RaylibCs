using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Slider;

public class SimpleSlider(Vector2 position, Vector2 size, Color color, int lineSize, bool showPercentage = true) : ObjectTemplate(position, size)
{
  protected float Value = 0f;
  protected RectangleHitbox Hitbox = new(position, size, new Color(255, 0, 0, 123));
  protected SimpleText Percentage = new(position + new Vector2(size.X + 4, 0), new Vector2(60, size.Y), 20, "0%", color, true);

  private float leftSide = position.X;
  private float rightSide = position.X + size.X;

  public override void Activation(Registry registry)
  {
    Hitbox.Activation(registry);
    if (showPercentage) Percentage.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    Value = (float)Math.Sin(Raylib.GetTime())/2 + 0.5f;
    if (showPercentage)
    {
      Percentage.Update(registry);
      Percentage.SetText((Value * 100).ToString("F0") + "%"); 
    }
    Hitbox.Update(registry);
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    float middleLine = position.Y + size.Y / 2;
    Raylib.DrawLineEx(new Vector2(leftSide, middleLine), new Vector2(rightSide, middleLine), lineSize, color);
    Raylib.DrawCircle((int)(leftSide + (rightSide - leftSide) * Value), (int)middleLine, size.Y / 2, color);
    
    if (showPercentage) Percentage.Draw(registry);
    Hitbox.Draw(registry);
    base.Draw(registry);
  }
}