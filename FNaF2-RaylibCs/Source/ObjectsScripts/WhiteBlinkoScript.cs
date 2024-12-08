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

  public override void Activation(Registry registry)
  {
    _timer.Activation(registry);
    _stage = 0;
    obj.SetColor(Color.White);
    obj.SetFrame(0);
  }

  public override void Update(Registry registry)
  {
    _timer.Update(registry);
    
    obj.SetFrame(_stage == 1 ? new Random().Next(1, 5) : 0);

    if (_stage == 2)
    {
      obj.SetColor(Color.Blank);
      _timer.StopAndResetTimer();
      _stage = 3;
    }
    
    if (!_timer.EndedTrigger() || _stage >= 3) return;
    _stage++;
  }
}