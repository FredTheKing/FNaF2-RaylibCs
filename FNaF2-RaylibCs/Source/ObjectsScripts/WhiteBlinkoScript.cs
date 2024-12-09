using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.ObjectsScripts;

public class WhiteBlinkoScript(SelectableImage obj) : ScriptTemplate
{
  private int _stage; //0-stay white, 1-random, 2-go blank, 3-dead
  private SimpleTimer _timer = new(0.084f, true);
  private SimpleTimer _changing_whito = new(0.006f);

  public override void Activation(Registry registry)
  {
    _timer.Activation(registry);
    _changing_whito.Activation(registry);
    _stage = 0;
    obj.SetColor(Color.White);
    obj.SetFrame(0);
  }

  public override void Update(Registry registry)
  {
    _timer.Update(registry);

    switch (_stage)
    {
      case 1:
      {
        _changing_whito.ContinuousStartTimer();
        _changing_whito.Update(registry);
        if (_changing_whito.EndedTrigger()) obj.SetFrame(new Random().Next(1, 5));
        break;
      }
      case 2:
        _changing_whito.StopAndResetTimer();
        obj.SetColor(Color.Blank);
        _timer.StopAndResetTimer();
        _stage = 3;
        break;
    }

    if (!_timer.EndedTrigger() || _stage >= 3) return;
    _stage++;
  }
}