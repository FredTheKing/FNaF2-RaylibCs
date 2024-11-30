using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scenes;

public class GlobalOverlay(Registry registry)
{
  private void ResizeAndCenter(int width, int height)
  {
    Raylib.SetWindowSize(width, height);
    Raylib.SetWindowPosition((Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth()) / 2, (Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight()) / 2);
  }
  
  public void Activation()
  {

  }
    
  public void Update()
  {
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F3)) registry.SwitchDebugMode();
    if (registry.GetDebugMode() & Raylib.GetScreenWidth() != 1424) ResizeAndCenter(1424, 768);
    else if (!registry.GetDebugMode() & Raylib.GetScreenWidth() != 1024) ResizeAndCenter(1024, 768);
  }
    
  public void Draw()
  {
    
  }
}