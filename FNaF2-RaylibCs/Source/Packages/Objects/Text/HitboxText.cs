using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using Raylib_cs;
namespace FNaF2_RaylibCs.Source.Packages.Objects.Text;

public class HitboxText : SimpleText
{
  protected RectangleHitbox? Hitbox;
  
  public HitboxText(Vector2 position, Vector2 size, int fontSize, string text, Color color, bool alignCenterV = false, bool alignCenterH = false) : base(position, size, fontSize, text, color, alignCenterV, alignCenterH) { InitHitbox(position, size); }
  public HitboxText(Vector2 position, Vector2 size, int fontSize, string text, Color color, FontResource font, bool alignCenterV = false, bool alignCenterH = false) : base(position, size, fontSize, text, color, font, alignCenterV, alignCenterH) { InitHitbox(position, size); }
  public HitboxText(Vector2 position, Vector2 size, int fontSize, Color color, bool alignCenterV = false, bool alignCenterH = false) : base(position, size, fontSize, color, alignCenterV, alignCenterH) { InitHitbox(position, size); }
  public HitboxText(Vector2 position, Vector2 size, int fontSize, Color color, FontResource font, bool alignCenterV = false, bool alignCenterH = false) : base(position, size, fontSize, color, font, alignCenterV, alignCenterH) { InitHitbox(position, size); }

  private void InitHitbox(Vector2 position, Vector2 size) => Hitbox = new RectangleHitbox(position, size);

  public RectangleHitbox GetHitbox() => Hitbox!;
  
  public override void CallDebuggerInfo(Registry registry)
  {
    Hitbox!.CallDebuggerInfo(registry);
    base.CallDebuggerInfo(registry);
  }

  public override void Activation(Registry registry)
  {
    Hitbox!.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    Hitbox!.Update(registry);
    base.Update(registry);
  }
  
  public override void Draw(Registry registry)
  {
    Hitbox!.Draw(registry);
    base.Draw(registry);
  }
}