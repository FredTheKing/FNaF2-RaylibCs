using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Box;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class HitboxTextBorderImage : SimpleImage
{
  public HitboxTextBorderImage(Vector2 position, string text, FontResource font, ImageResource image, int thickness, Color bordColor, Color? tint = null, Vector2? newSize = null, float rotation = 0) : base(position, image, tint, newSize, rotation) {
    Text = new(Position + new Vector2(Size.X - 60, Size.Y - 40), new Vector2(60, 40), 34, text, Color.White, font, true, true);
    Hitbox = new(Position, Size);
    Gray = new(Position + new Vector2(Size.X - 60, Size.Y - 40), new Vector2(60, 40), new Color(0, 0, 0, 152), 0.4f);
    Border = new(Position, Size, bordColor, thickness);
    Resource = image;
  }

  public override void CallDebuggerInfo(Registry registry)
  {
    Text.CallDebuggerInfo(registry);
    Hitbox.CallDebuggerInfo(registry);
    Gray.CallDebuggerInfo(registry);
    Border.CallDebuggerInfo(registry);
    ImGui.Separator();
    base.CallDebuggerInfo(registry);
  }

  public SimpleText Text;
  public RectangleHitbox Hitbox;
  public RoundedBox Gray;
  public BorderBox Border;
  public ImageResource Resource;

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
    Gray.Draw(registry);
    Text.Draw(registry);
    Border.Draw(registry);
    Hitbox.Draw(registry);
  }
}