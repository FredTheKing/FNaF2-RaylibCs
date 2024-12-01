using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Animation;

public enum AnimationPlayMode { Replacement, Addition };

public class SimpleAnimation(Vector2 position, float fps, Color color, AnimationPlayMode play_mode, ImageStackResource resource, SimpleTimer? custom_update_timer = null, bool restart_on_scene_change = true) : ObjectTemplate(position, resource.GetSize())
{
  private SimpleTimer _update_timer = custom_update_timer ?? new SimpleTimer(1f / fps, true);
  private int _current_frame = 0;
  
  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {_position.X}, {_position.Y}");
    ImGui.Text($" > Size: {_size.X}, {_size.Y}");
    ImGui.Text($" > Color: {color.R}, {color.G}, {color.B}, {color.A}");
    ImGui.Separator();
    ImGui.Text($" > Play Mode: {play_mode.ToString()}");
    ImGui.Text($" > Current Frame: {_current_frame + 1} / {resource.GetMaterial().Count}");
    _update_timer.CallDebuggerInfo(registry);

    if (ImGui.TreeNode("Animation Resource"))
    {
      resource.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
  }
  
  public SimpleTimer GetUpdateTimer() => _update_timer;
  
  public new void Activation(Registry registry)
  {
    if (restart_on_scene_change) _current_frame = 0;
    _update_timer.Activation(registry);
    base.Activation(registry);
  }
  
  public new void Update(Registry registry)
  {
    _update_timer.Update(registry);
    if (_update_timer.EndedTrigger()) _current_frame = (_current_frame + 1) % (resource.GetMaterial().Count);
    base.Update(registry);
  }

  public new void Draw(Registry registry)
  {
    if (play_mode == AnimationPlayMode.Addition) 
      for (int i = 0; i < _current_frame; i++) 
        Raylib.DrawTexturePro(resource.GetMaterial()[i], new Rectangle(Vector2.Zero, resource.GetSize().X, resource.GetSize().Y), new Rectangle(_position, _size), Vector2.Zero, 0, color);
    else 
      Raylib.DrawTexturePro(resource.GetMaterial()[_current_frame], new Rectangle(Vector2.Zero, resource.GetSize().X, resource.GetSize().Y), new Rectangle(_position, _size), Vector2.Zero, 0, color);
    base.Draw(registry);
    if(registry.GetShowBounds() & registry.GetDebugMode()) Raylib.DrawRectangleLinesEx(new Rectangle(_position, _size), 1, Color.Lime);
  }
}