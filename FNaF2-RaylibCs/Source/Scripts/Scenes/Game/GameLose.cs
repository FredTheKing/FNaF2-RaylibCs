using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

public class GameLose : ScriptTemplate
{
  public override void Deactivation(Registry registry, string nextSceneName)
  {
    Registration.Objects.Static!.SetTint(new Color(255, 255, 255, 100));
  }

  public override void Activation(Registry registry)
  {
    Registration.Objects.Static!.SetTint(Color.White);
  }
}