using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;

public class RectangleHitbox(Vector2 position, Vector2 size, Color color) : RawHitbox(position, size, color)
{
  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode("Hitbox"))
    {
      ImGui.Text($" > Position: {_position.X}, {_position.Y}");
      ImGui.Text($" > Size: {_size.X}, {_size.Y}");
      ImGui.BeginGroup();
      ImGui.Text($" > Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), _color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), _color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), _color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), _color.A.ToString());
      
      ImGui.Separator();
      
      ImGui.TextColored(new Vector4(30, 30, 30, 1), " # Mouse list = [LMB|RMB|MMB]");
      ImGui.Text($" > Mouse Hovered: {(_hitbox_click_hover ? 1 : 0)}");
      ImGui.Text($" > Mouse Pressed: {(_hitbox_click_press[0] ? 1 : 0)}|{(_hitbox_click_press[1] ? 1 : 0)}|{(_hitbox_click_press[2] ? 1 : 0)}");
      ImGui.Text($" > Mouse Outside Pressed: {(_hitbox_click_outside_press[0] ? 1 : 0)}|{(_hitbox_click_outside_press[1] ? 1 : 0)}|{(_hitbox_click_outside_press[2] ? 1 : 0)}");
      ImGui.Text($" > Mouse Held: {(_hitbox_click_hold[0] ? 1 : 0)}|{(_hitbox_click_hold[1] ? 1 : 0)}|{(_hitbox_click_hold[2] ? 1 : 0)}");
      ImGui.Text($" > Mouse Released: {(_hitbox_click_release[0] ? 1 : 0)}|{(_hitbox_click_release[1] ? 1 : 0)}|{(_hitbox_click_release[2] ? 1 : 0)}");
      
      ImGui.TreePop();
    }
  }
  
  public void SetBoundaries(Vector2 new_size)
  {
    _size = new_size;
  }

  private void CheckMouseHover()
  {
    Vector2 nonref_position = _position;
    _hitbox_click_hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(nonref_position, _size));
  }
  
  public override void Update(Registry registry)
  {
    CheckMouseHover();
    UpdateClicksDetection();
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    if(registry.GetDebugMode() & registry.GetShowHitboxes()) Raylib.DrawRectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y, _color);
    base.Draw(registry);
  }
}