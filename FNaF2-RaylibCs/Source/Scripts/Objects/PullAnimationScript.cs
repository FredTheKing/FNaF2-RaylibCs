using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;

namespace FNaF2_RaylibCs.Source.Scripts.Objects;

internal enum States
{
  Down,
  GoingUp,
  Up,
  GoingDown
}

public class PullAnimationScript(SelectableAnimation obj) : ScriptTemplate
{
  private States _state = States.Down;

  public override void Activation(Registry registry)
  {
    _state = States.Down;
    obj.SetPack(0);
    base.Activation(registry);
  }

  public void TriggerPullAction()
  {
    States gottaGo = _state switch
    {
      States.Down or States.GoingDown => States.GoingUp,
      States.Up or States.GoingUp => States.GoingDown
    };

    switch (gottaGo)
    {
      case States.GoingUp:
        _state = States.GoingUp;
        obj.SetPack(1);
        break;
      case States.GoingDown:
        _state = States.GoingDown;
        obj.SetPack(3);
        break;
    }
  }

  public override void Update(Registry registry)
  {
    if (obj.IsFinished())
    {
      switch (_state)
      {
        case States.GoingUp:
          _state = States.Up;
          obj.SetPack(2);
          break;
        case States.GoingDown:
          _state = States.Down;
          obj.SetPack(0);
          break;
      }
    }
    base.Update(registry);
  }
}