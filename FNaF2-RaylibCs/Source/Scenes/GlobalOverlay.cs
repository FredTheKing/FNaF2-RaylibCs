using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;

namespace RaylibArteSonat.Source.Scenes;

public class GlobalOverlay(Registry registry)
{
  public void Activation()
  {

  }
    
  public void Update()
  {
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F3)) registry.SwitchDebugMode();
  }
    
  public void Draw()
  {
    
  }
}