using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Circular;

public class SimpleCircular(Vector2 position, float radius, float decreaseSpeed, float recoverySpeed, Color color, float? defaultValue = null, bool? defaultActivated = null) : ObjectTemplate(position, new Vector2(radius*2))
{
  public float Value;
  public bool Activated = true;
  public bool Recovering = false;
  private Color _color = color;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Value: {Value}");
    ImGui.Text($" > Activated: {(Activated ? 1 : 0)}");
    ImGui.Text($" > Recovering: {(Recovering ? 1 : 0)}");
  }
  
  public void SetColor(Color color) => _color = color;
  public Color GetColor() => _color;

  private void UpdateValue()
  {
    if (!Activated) return;
    Value = !Recovering ? Math.Clamp(Value - decreaseSpeed * Raylib.GetFrameTime(), 0, 1) : Math.Clamp(Value + recoverySpeed * Raylib.GetFrameTime(), 0, 1);
  }

  public override void Activation(Registry registry)
  {
    Value = defaultValue ?? 1f;
    Activated = defaultActivated ?? true;
  }

  public override void Update(Registry registry)
  {
    UpdateValue();
  }

  public override void Draw(Registry registry)
  {
    Raylib.DrawCircleSector(position, radius, 270, 360 * (- Value + 0.75f), 64, _color);
  }
}
// Calculating frame time: 1 second / 60 frames = 0.0167 seconds per frame
// Further dividing the frame time by 60: 0.0167 / 60 = 0.00027778