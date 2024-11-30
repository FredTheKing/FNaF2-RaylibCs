using FNaF2_RaylibCs.Source.Packages.Module;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scenes;

public class GlobalOverlay(Registry registry)
{
  public void Activation()
  {

  }
    
  public void Update()
  {
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F3)) registry.SwitchDebugMode();
    if (registry.GetDebugMode())
    {
      Raylib.SetWindowSize(1424, 768);
      Raylib.SetWindowPosition((Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth()) / 2, (Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight()) / 2);
    }
    else
    {
      Raylib.SetWindowSize(1024, 768);
      Raylib.SetWindowPosition((Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth()) / 2, (Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight()) / 2);
    }
  }
    
  public void Draw()
  {
    
  }
}