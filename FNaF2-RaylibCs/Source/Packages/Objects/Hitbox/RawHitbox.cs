using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;

public class RawHitbox(Vector2 position, Vector2 size, Color color) : ObjectTemplate(position, size)
{
  protected Color _color = color;
  
  // LMB, RMB, MMB in List
  protected bool _hitbox_click_hover = false;
  protected List<bool> _hitbox_click_press = [false, false, false];
  protected List<bool> _hitbox_click_outside_press = [false, false, false];
  protected List<bool> _hitbox_click_hold = [false, false, false];
  protected List<bool> _hitbox_click_release = [false, false, false];
  
  private void CheckMousePressed()
  {
    _hitbox_click_press[0] = Raylib.IsMouseButtonPressed(MouseButton.Left) & _hitbox_click_hover;
    _hitbox_click_press[1] = Raylib.IsMouseButtonPressed(MouseButton.Right) & _hitbox_click_hover;
    _hitbox_click_press[2] = Raylib.IsMouseButtonPressed(MouseButton.Middle) & _hitbox_click_hover;
  }
  
  private void CheckMouseOutsidePressed()
  {
    _hitbox_click_outside_press[0] = Raylib.IsMouseButtonPressed(MouseButton.Left) & !_hitbox_click_hover;
    _hitbox_click_outside_press[1] = Raylib.IsMouseButtonPressed(MouseButton.Right) & !_hitbox_click_hover;
    _hitbox_click_outside_press[2] = Raylib.IsMouseButtonPressed(MouseButton.Middle) & !_hitbox_click_hover;
  }
  
  private void CheckMouseHeld()
  {
    _hitbox_click_hold[0] = Raylib.IsMouseButtonDown(MouseButton.Left) & _hitbox_click_hover;
    _hitbox_click_hold[1] = Raylib.IsMouseButtonDown(MouseButton.Right) & _hitbox_click_hover;
    _hitbox_click_hold[2] = Raylib.IsMouseButtonDown(MouseButton.Middle) & _hitbox_click_hover;
  }
  
  private void CheckMouseReleased()
  {
    _hitbox_click_release[0] = Raylib.IsMouseButtonReleased(MouseButton.Left) & _hitbox_click_hover;
    _hitbox_click_release[1] = Raylib.IsMouseButtonReleased(MouseButton.Right) & _hitbox_click_hover;
    _hitbox_click_release[2] = Raylib.IsMouseButtonReleased(MouseButton.Middle) & _hitbox_click_hover;
  }

  protected void UpdateClicksDetection()
  {
    CheckMousePressed();
    CheckMouseOutsidePressed();
    CheckMouseHeld();
    CheckMouseReleased();
  }

  public bool GetMouseHover() => _hitbox_click_hover;
  
  public bool GetMousePressed(MouseButton button) => _hitbox_click_press[(int)button];
  
  public bool GetMouseOutsidePressed(MouseButton button) => _hitbox_click_outside_press[(int)button];

  public bool GetMouseHold(MouseButton button) => _hitbox_click_hold[(int)button];
  
  public bool GetMouseReleased(MouseButton button) => _hitbox_click_release[(int)button];
  
  public new void Update(Registry registry)
  {
    UpdateClicksDetection();
    base.Update(registry);
  }

  public new void Draw(Registry registry)
  {
    base.Draw(registry);
  }
}