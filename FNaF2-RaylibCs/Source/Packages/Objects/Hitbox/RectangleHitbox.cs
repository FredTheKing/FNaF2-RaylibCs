using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;

public class RectangleHitbox(Vector2 position, Vector2 size, Color? color = null) : RawHitbox(position, size, color)
{
  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode(DebuggerName))
    {
      ImGui.Text($" > Position: {Position.X}, {Position.Y}");
      ImGui.Text($" > Size: {Size.X}, {Size.Y}");
      ImGui.BeginGroup();
      ImGui.Text($" > Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), Color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), Color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), Color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), Color.A.ToString());
      
      ImGui.Separator();
      
      ImGui.TextColored(new Vector4(30, 30, 30, 1), " # Mouse list = [LMB|RMB|MMB]");
      ImGui.Text($" > Mouse Hovered: {(HitboxHover ? 1 : 0)}");
      ImGui.Text($" > Mouse Hovered (Frame): {(HitboxHoverFrame ? 1 : 0)}");
      ImGui.Text($" > Mouse Pressed: {(HitboxClickPress[0] ? 1 : 0)}|{(HitboxClickPress[1] ? 1 : 0)}|{(HitboxClickPress[2] ? 1 : 0)}");
      ImGui.Text($" > Mouse Outside Pressed: {(HitboxClickOutsidePress[0] ? 1 : 0)}|{(HitboxClickOutsidePress[1] ? 1 : 0)}|{(HitboxClickOutsidePress[2] ? 1 : 0)}");
      ImGui.Text($" > Mouse Held: {(HitboxClickHold[0] ? 1 : 0)}|{(HitboxClickHold[1] ? 1 : 0)}|{(HitboxClickHold[2] ? 1 : 0)}");
      ImGui.Text($" > Mouse Dragged: {(HitboxClickDrag[0] ? 1 : 0)}|{(HitboxClickDrag[1] ? 1 : 0)}|{(HitboxClickDrag[2] ? 1 : 0)}");
      ImGui.Text($" > Mouse Released: {(HitboxClickRelease[0] ? 1 : 0)}|{(HitboxClickRelease[1] ? 1 : 0)}|{(HitboxClickRelease[2] ? 1 : 0)}");
      
      ImGui.TreePop();
    }
  }
  
  protected string DebuggerName = "Hitbox-" + new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4)
    .Select(s => s[new Random().Next(s.Length)]).ToArray());
  
  public void SetBoundaries(Vector2 newSize) => Size = newSize;

  private void CheckMouseHover()
  {
    HitboxHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position, Size));
    HitboxHoverFrame = HitboxHover && !PreviousHoverState;
    PreviousHoverState = HitboxHover;
  }
  
  public override void Update(Registry registry)
  {
    CheckMouseHover();
    UpdateClicksDetection();
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    if(registry.DebugMode & registry.ShowHitboxes) Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, Color);
    base.Draw(registry);
  }
}