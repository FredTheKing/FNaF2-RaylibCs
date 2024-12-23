using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Animation;

public enum AnimationPlayMode { Replacement, Addition };

public class SimpleAnimation(Vector2 position, float fps, Color color, AnimationPlayMode playMode, ImageStackResource resource, SimpleTimer? customUpdateTimer = null, bool restartOnSceneChange = true) : ObjectTemplate(position, resource.GetSize())
{
  private SimpleTimer _updateTimer = customUpdateTimer ?? new SimpleTimer(1f / fps, true);
  private int _currentFrame;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {Position.X}, {Position.Y}");
    ImGui.Text($" > Size: {Size.X}, {Size.Y}");
    ImGui.Text($" > Color: {color.R}, {color.G}, {color.B}, {color.A}");
    ImGui.Separator();
    ImGui.Text($" > Play Mode: {playMode.ToString()}");
    ImGui.Text($" > Current Frame: {_currentFrame + 1} / {resource.GetMaterial().Count}");
    _updateTimer.CallDebuggerInfo(registry);

    if (ImGui.TreeNode("Animation Resource"))
    {
      resource.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
  }

  public SimpleTimer GetUpdateTimer() => _updateTimer;

  public override void Activation(Registry registry)
  {
    if (restartOnSceneChange) _currentFrame = 0;
    _updateTimer.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    _updateTimer.Update(registry);
    if (_updateTimer.TargetTrigger()) _currentFrame = (_currentFrame + 1) % (resource.GetMaterial().Count);
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    if (playMode == AnimationPlayMode.Addition) 
      for (int i = 0; i < _currentFrame; i++) 
        Raylib.DrawTexturePro(resource.GetMaterial()[i], new Rectangle(Vector2.Zero, resource.GetSize().X, resource.GetSize().Y), new Rectangle(Position, Size), Vector2.Zero, 0, color);
    else 
      Raylib.DrawTexturePro(resource.GetMaterial()[_currentFrame], new Rectangle(Vector2.Zero, resource.GetSize().X, resource.GetSize().Y), new Rectangle(Position, Size), Vector2.Zero, 0, color);
    base.Draw(registry);
    DrawDebug(registry);
  }
  
  protected void DrawDebug(Registry registry)
  {
    if (registry.GetShowBounds() & registry.GetDebugMode()) Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size), 1, Color.Blue);
  }
}