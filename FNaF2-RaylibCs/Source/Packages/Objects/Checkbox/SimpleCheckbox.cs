using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Checkbox;

public class SimpleCheckbox(Vector2 position, int size, Color color, bool checkedByDefault = false) : ObjectTemplate(position, new Vector2(size))
{
  protected bool Checked = checkedByDefault;
  protected RectangleHitbox Hitbox = new(position, new Vector2(size, size), new Color(255, 0, 0, 123));

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Checked: {(Checked ? 1 : 0)}");
    Hitbox.CallDebuggerInfo(registry);
  }

  public override void Activation(Registry registry)
  {
    Hitbox.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    Hitbox.Update(registry);
    if (Hitbox.GetMousePress(MouseButton.Left)) SwitchChecked();
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    Raylib.DrawRectangleLinesEx(new Rectangle(Position, size, size), size/16, color);

    if (Checked) Raylib.DrawRectangleRec(new Rectangle(Position + new Vector2(size/4), new Vector2(size) - new Vector2(size/4)*2), color);

    Hitbox.Draw(registry);
    base.Draw(registry);
  }
  
  public bool GetChecked() => Checked;
  
  public void SetChecked(bool newChecked) => Checked = newChecked;
  
  public void SwitchChecked() => Checked = !Checked;
}