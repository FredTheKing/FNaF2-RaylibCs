using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Debugger;

public class DebuggerTesting : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    //Raylib.PlaySound(Raylib.LoadSound(Config.ResPath + "Sound/test.mp3"));
  }

  public override void Update(Registry registry)
  {
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.G)) Registration.Sounds.TestSound!.Play();
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.S)) Registration.Sounds.TestSound!.Stop();
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.P)) Registration.Sounds.TestSound!.Pause();
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.R)) Registration.Sounds.TestSound!.Resume();
  }
}