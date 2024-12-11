using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Slider;

public class SimpleSlider : ObjectTemplate
{
  public SimpleSlider(Vector2 position, Vector2 size, Color color, int lineSize, float defaultValue, bool showPercentage = true) : base(position, size)
  {
    Value = defaultValue;
    Radius = size.Y / 2;
    Color = color;
    LineSize = lineSize;
    ShowPercentage = showPercentage;
    
    _percentSpace = size.X + size.Y / 4;
    _leftSide = position.X;
    _rightSide = position.X + size.X;
    
    Hitbox = new RectangleHitbox(position with { X = position.X - Radius }, size with { X = size.X + 2 * Radius }, new Color(255, 0, 0, 123));
    Percentage = new SimpleText(position + new Vector2(_percentSpace, 0), size with { X = 60 }, 20, "0%", Color, true);
  }
  
  protected float Value;
  protected float Radius;
  protected readonly RectangleHitbox Hitbox;
  protected readonly SimpleText Percentage;
  protected Color Color;
  protected readonly int LineSize;
  protected readonly bool ShowPercentage;
  
  private float _leftSide;
  private float _rightSide;
  private readonly float _percentSpace;
  
  public void SetValue(float value) => Value = value;
  public float GetValue() => Value;
  
  public override void SetPosition(Vector2 position)
  {
    _leftSide = position.X;
    _rightSide = position.X + Size.X;
    Hitbox.SetPosition(position with {X = position.X - Radius});
    Percentage.SetPosition(position + new Vector2(_percentSpace, 0));
    base.SetPosition(position);
  }

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Value: {Value}");
    Hitbox.CallDebuggerInfo(registry);
    if (ShowPercentage) Percentage.CallDebuggerInfo(registry);
  }

  public override void Activation(Registry registry)
  {
    Hitbox.Activation(registry);
    if (ShowPercentage) Percentage.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    if (Hitbox.GetMouseDrag(MouseButton.Left)) 
      Value = Math.Clamp((Raylib.GetMousePosition().X - _leftSide) / (_rightSide - _leftSide), 0f, 1f);

    if (ShowPercentage)
    {
      Percentage.Update(registry);
      Percentage.SetText((Value * 100).ToString("F0") + "%"); 
    }
    Hitbox.Update(registry);
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    float middleLine = Position.Y + Size.Y / 2;
    Raylib.DrawLineEx(new Vector2(_leftSide, middleLine), new Vector2(_rightSide, middleLine), LineSize, Color);
    Raylib.DrawCircle((int)(_leftSide + (_rightSide - _leftSide) * Value), (int)middleLine, Radius, Color);
    
    if (ShowPercentage) Percentage.Draw(registry);
    Hitbox.Draw(registry);
    base.Draw(registry);
  }
}