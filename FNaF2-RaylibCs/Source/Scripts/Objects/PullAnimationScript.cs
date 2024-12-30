using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;

namespace FNaF2_RaylibCs.Source.Scripts.Objects;

public enum States
{
  Down,
  GoingUp,
  Up,
  GoingDown
}

public class PullAnimationScript(SelectableAnimation obj) : ScriptTemplate
{
  public States State = States.Down;

  public override void Activation(Registry registry)
  {
    State = States.Down;
    obj.SetPack(0);
    base.Activation(registry);
  }

  public void TriggerPullAction()
  {
    States gottaGo = State switch
    {
      States.Down or States.GoingDown => States.GoingUp,
      States.Up or States.GoingUp => States.GoingDown
    };

    switch (gottaGo)
    {
      case States.GoingUp:
        State = States.GoingUp;
        obj.SetPack(1);
        break;
      case States.GoingDown:
        State = States.GoingDown;
        obj.SetPack(3);
        break;
    }
  }

  public override void Update(Registry registry)
  {
    if (obj.IsFinished())
    {
      switch (State)
      {
        case States.GoingUp:
          State = States.Up;
          obj.SetPack(2);
          break;
        case States.GoingDown:
          State = States.Down;
          obj.SetPack(0);
          break;
      }
    }
    base.Update(registry);
  }
}