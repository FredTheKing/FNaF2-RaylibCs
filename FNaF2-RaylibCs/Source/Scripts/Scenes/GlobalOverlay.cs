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
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F3)) registry.SwitchDebugMode();
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F1)) registry.GetSceneManager().PreviousScene(registry);
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F2)) registry.GetSceneManager().NextScene(registry);
    #endif
    
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F10)) Center();
    
    if (registry.GetDebugMode() & Raylib.GetScreenWidth() != 1424) ResizeAndCenter(1424, 768);
    else if (!registry.GetDebugMode() & Raylib.GetScreenWidth() != 1024) ResizeAndCenter(1024, 768);
  }
}