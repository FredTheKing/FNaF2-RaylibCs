using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes;

public class GlobalOverlay : ScriptTemplate
{
  private void ResizeAndCenter(int width, int height)
  {
    Raylib.SetWindowSize(width, height);
    Center();
  }
  
  private void Center() => Raylib.SetWindowPosition((Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth()) / 2, (Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight()) / 2);
    
  public override void Update(Registry registry)
  {
    #if DEBUG
    if (registry.keybinds.IsKeyPressed(KeyboardKey.F3)) registry.DebugMode = !registry.DebugMode;
    if (registry.keybinds.IsKeyPressed(KeyboardKey.F1)) registry.scene.Previous(registry);
    if (registry.keybinds.IsKeyPressed(KeyboardKey.F2)) registry.scene.Next(registry);
    #endif
    
    if (registry.keybinds.IsKeyPressed(KeyboardKey.F10)) Center();
    
    if (registry.DebugMode & Raylib.GetScreenWidth() != Config.WindowWidth + 400) ResizeAndCenter(Config.WindowWidth + 400, Config.WindowHeight);
    else if (!registry.DebugMode & Raylib.GetScreenWidth() != Config.WindowWidth) ResizeAndCenter(Config.WindowWidth, Config.WindowHeight);
  }

  public override void Draw(Registry registry)
  {
    #if DEBUG
    Raylib.DrawRectangle(Config.WindowWidth, 0, 400, Config.WindowHeight, Color.Black);
    #endif
  }
}