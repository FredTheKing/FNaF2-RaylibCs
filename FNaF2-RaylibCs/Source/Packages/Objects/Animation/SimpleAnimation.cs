using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Animation;

public enum AnimationPlayMode { Replacement, Addition };

public class SimpleAnimation : ObjectTemplate
{
  public SimpleAnimation(Vector2 position, float fps, Color color, AnimationPlayMode playMode, ImageStackResource resource, bool restartOnSceneChange = true, SimpleTimer? customUpdateTimer = null) : base(position, resource.GetSize()) { InitAnimation(fps, color, playMode, customUpdateTimer, restartOnSceneChange); Resource = resource; }
  public SimpleAnimation(Vector2 position, float fps, Color color, AnimationPlayMode playMode, Vector2 originalSize, bool restartOnSceneChange = true, SimpleTimer? customUpdateTimer = null) : base(position, originalSize) { InitAnimation(fps, color, playMode, customUpdateTimer, restartOnSceneChange); }

  private void InitAnimation(float fps, Color color, AnimationPlayMode playMode, SimpleTimer? customUpdateTimer, bool restartOnSceneChange)
  {
    UpdateTimer = customUpdateTimer ?? new SimpleTimer(1 / fps, restartOnSceneChange);
    Tint = color;
    RestartOnSceneChange = restartOnSceneChange;
    PlayMode = playMode;
  }
  
  protected int CurrentFrame;
  protected SimpleTimer UpdateTimer = null!;
  protected Color Tint;
  protected bool RestartOnSceneChange;
  protected AnimationPlayMode PlayMode;
  protected ImageStackResource? Resource;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {Position.X}, {Position.Y}");
    ImGui.Text($" > Size: {Size.X}, {Size.Y}");
    ImGui.Text($" > Color: {Tint.R}, {Tint.G}, {Tint.B}, {Tint.A}");
    ImGui.Separator();
    ImGui.Text($" > Play Mode: {PlayMode.ToString()}");
    ImGui.Text($" > Current Frame: {CurrentFrame + 1} / {(Resource is null ? "???" : Resource.GetMaterial().Count)}");
    UpdateTimer.CallDebuggerInfo(registry);

    if (ImGui.TreeNode("Animation Resource"))
    {
      Resource?.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
  }

  public SimpleTimer GetUpdateTimer() => UpdateTimer;

  public override void Activation(Registry registry)
  {
    if (RestartOnSceneChange) CurrentFrame = 0;
    UpdateTimer.Activation(registry);
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    UpdateTimer.Update(registry);
    if (Resource is not null) 
      if (UpdateTimer.TargetTrigger()) CurrentFrame = (CurrentFrame + 1) % (Resource.GetMaterial().Count);
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    if (Resource is not null)
    {
      if (PlayMode == AnimationPlayMode.Addition)
        for (int i = 0; i < CurrentFrame; i++)
          Raylib.DrawTexturePro(Resource.GetMaterial()[i],
            new Rectangle(Vector2.Zero, Resource.GetSize().X, Resource.GetSize().Y), new Rectangle(Position, Size),
            Vector2.Zero, 0, Tint);
      else
        Raylib.DrawTexturePro(Resource.GetMaterial()[CurrentFrame],
          new Rectangle(Vector2.Zero, Resource.GetSize().X, Resource.GetSize().Y), new Rectangle(Position, Size),
          Vector2.Zero, 0, Tint);
    }
    base.Draw(registry);
    DrawDebug(registry);
  }

  public virtual bool IsFinished() => Resource != null && CurrentFrame == Resource.GetMaterial().Count - 1;
  
  protected void DrawDebug(Registry registry)
  {
    if (registry.ShowBounds & registry.DebugMode) Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size), 1, Color.Blue);
  }
}