using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

public class GameNewspaper : ScriptTemplate
{
  private SimpleTimer _timer = new(8, true);
  private float customSizeW = 1024;
  
  public override void Activation(Registry registry)
  {
    _timer.Activation(registry);
    customSizeW = 1024;
  }

  public override void Update(Registry registry)
  {
    _timer.Update(registry);

    Registration.Objects.GameNewspapers!.SetSize(new Vector2(customSizeW, (float)(customSizeW * Config.WindowRatioWH)));

    Vector2 screenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    Vector2 debugOffset = registry.GetDebugMode() ? new Vector2(-200, 0) : Vector2.Zero;
    Vector2 centeredPosition = (screenSize - Registration.Objects.GameNewspapers.GetSize()) / 2;
    Registration.Objects.GameNewspapers.SetPosition(centeredPosition + debugOffset);

    customSizeW += 15f * Raylib.GetFrameTime();

    if (_timer.TargetTrigger())
      registry.GetSceneManager().ChangeScene(registry, Config.Scenes.GameLoading);
  }
}