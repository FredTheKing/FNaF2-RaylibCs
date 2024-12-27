using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;

public class RawHitbox(Vector2 position, Vector2 size, Color? color = null) : ObjectTemplate(position, size)
{
  protected Color Color = color ?? new Color(255, 0, 0, 42);
  
  // LMB, RMB, MMB in List
  protected bool PreviousHoverState = true;
  protected bool HitboxHover = false;
  protected bool HitboxHoverFrame = false;
  protected readonly List<bool> HitboxClickPress = [false, false, false];
  protected readonly List<bool> HitboxClickOutsidePress = [false, false, false];
  protected readonly List<bool> HitboxClickHold = [false, false, false];
  protected readonly List<bool> HitboxClickDrag = [false, false, false];
  protected readonly List<bool> HitboxClickRelease = [false, false, false];
  
  private void CheckMousePressed()
  {
    HitboxClickPress[0] = Raylib.IsMouseButtonPressed(MouseButton.Left) & HitboxHover;
    HitboxClickPress[1] = Raylib.IsMouseButtonPressed(MouseButton.Right) & HitboxHover;
    HitboxClickPress[2] = Raylib.IsMouseButtonPressed(MouseButton.Middle) & HitboxHover;
  }
  
  private void CheckMouseOutsidePressed()
  {
    HitboxClickOutsidePress[0] = Raylib.IsMouseButtonPressed(MouseButton.Left) & !HitboxHover;
    HitboxClickOutsidePress[1] = Raylib.IsMouseButtonPressed(MouseButton.Right) & !HitboxHover;
    HitboxClickOutsidePress[2] = Raylib.IsMouseButtonPressed(MouseButton.Middle) & !HitboxHover;
  }
  
  private void CheckMouseHeld()
  {
    HitboxClickHold[0] = Raylib.IsMouseButtonDown(MouseButton.Left);
    HitboxClickHold[1] = Raylib.IsMouseButtonDown(MouseButton.Right);
    HitboxClickHold[2] = Raylib.IsMouseButtonDown(MouseButton.Middle);
  }
  
  private void CheckMouseDragged()
  {
    for (int i = 0; i < 3; i++)
    {
      if (!HitboxClickDrag[i] & HitboxClickPress[i] & HitboxHover) 
        HitboxClickDrag[i] = true;
      if (HitboxClickDrag[i] & !HitboxClickHold[i]) 
        HitboxClickDrag[i] = false;
    }
  }
  
  private void CheckMouseReleased()
  {
    HitboxClickRelease[0] = Raylib.IsMouseButtonReleased(MouseButton.Left) & HitboxHover;
    HitboxClickRelease[1] = Raylib.IsMouseButtonReleased(MouseButton.Right) & HitboxHover;
    HitboxClickRelease[2] = Raylib.IsMouseButtonReleased(MouseButton.Middle) & HitboxHover;
  }

  protected void UpdateClicksDetection()
  {
    CheckMousePressed();
    CheckMouseOutsidePressed();
    CheckMouseHeld();
    CheckMouseDragged();
    CheckMouseReleased();
  }

  public bool GetMouseHover() => HitboxHover;

  public bool GetMouseHoverFrame() => HitboxHoverFrame;
  
  public bool GetMousePress(MouseButton button) => HitboxClickPress[(int)button];
  
  public bool GetMouseOutsidePress(MouseButton button) => HitboxClickOutsidePress[(int)button];

  public bool GetMouseHold(MouseButton button) => HitboxClickHold[(int)button];
  
  public bool GetMouseDrag(MouseButton button) => HitboxClickDrag[(int)button];
  
  public bool GetMouseRelease(MouseButton button) => HitboxClickRelease[(int)button];
  
  public override void Update(Registry registry)
  {
    UpdateClicksDetection();
    base.Update(registry);
  }
}