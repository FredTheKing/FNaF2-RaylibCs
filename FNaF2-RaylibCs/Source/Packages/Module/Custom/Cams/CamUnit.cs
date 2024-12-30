using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Hitbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Cams;

public class CamUnit(Vector2 position, ImageStackResource resource, FontResource font, int number, Color? tint = null, Vector2? newSize = null) : SelectableImage(position, resource, tint, newSize)
{
  private SimpleTimer _timer = new(0.6f);
  private bool _flicker;
  private RectangleHitbox Hitbox = new(position, resource.GetSize());
  private SimpleText _topText = new(position + new Vector2(-4, -5), Vector2.Zero, 18, "CAM", Color.White, font);
  private SimpleText _bottomText = new(position + new Vector2(-4, 9), Vector2.Zero, 18, number.ToString().PadLeft(2, '0'), Color.White, font);
  public bool Selected;
  public bool Demanding;

  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode(number.ToString()))
    {
      base.CallDebuggerInfo(registry);
      ImGui.Separator();
      ImGui.Text($" > Selected: {Selected}");
      ImGui.Text($" > Demanding: {Demanding}");
      ImGui.Text($" > Time Left: {_timer.GetTimeLeft()}");
      ImGui.Text($" > Flicker: {(_flicker ? 1 : 0)}");
      ImGui.TreePop();
    }
  }

  public override void Activation(Registry registry)
  {
    _timer.Activation(registry);
    Hitbox.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    _timer.Update(registry);
    Hitbox.Update(registry);
    if (_timer.TargetTrigger()) _flicker = !_flicker;
    if (Selected)
    {
      SetFrame(_flicker ? 1 : 0);
      _timer.ContinuousStartTimer();
    }
    else
    {
      SetFrame(0);
      _timer.StopAndResetTimer();
    }
    
    if (Hitbox.GetMousePress(MouseButton.Left))
    {
      Demanding = true;
      _flicker = true;
    }
    
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    base.Draw(registry);
    Hitbox.Draw(registry);
    _topText.Draw(registry);
    _bottomText.Draw(registry);
  }
}