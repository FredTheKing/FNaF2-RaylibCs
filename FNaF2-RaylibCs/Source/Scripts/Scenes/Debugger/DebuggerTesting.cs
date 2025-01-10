using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Debugger;

public class DebuggerTesting : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    
  }

  public override void Update(Registry registry)
  {
    if (registry.keybinds.IsKeyPressed(KeyboardKey.G)) Registration.Sounds.MenuMusic!.Play();
    if (registry.keybinds.IsKeyPressed(KeyboardKey.S)) Registration.Sounds.MenuMusic!.Stop();
    if (registry.keybinds.IsKeyPressed(KeyboardKey.P)) Registration.Sounds.MenuMusic!.Pause();
    if (registry.keybinds.IsKeyPressed(KeyboardKey.R)) Registration.Sounds.MenuMusic!.Resume();
  }
}