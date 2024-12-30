using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Box;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Objects;

internal enum Direction { Standing, Left, Right }

public class CameraScrollerScript(DebugBox obj) : ScriptTemplate
{
  private int _left, _right;
  private Direction _direction = Direction.Left;
  private float _speed = 50;
  private SimpleTimer _timer = new(2f, true);

  public void SetBorder(int left, int right) { _left = left; _right = right; }
  
  public override void Update(Registry registry)
  {
    _timer.Update(registry);
    
    if (_direction is Direction.Standing) _timer.ContinuousStartTimer();
    else if (_direction is Direction.Left) obj.AddPosition(new Vector2(-_speed * Raylib.GetFrameTime(), 0));
    else if (_direction is Direction.Right) obj.AddPosition(new Vector2(_speed * Raylib.GetFrameTime(), 0));

    if (_direction is Direction.Left && obj.GetPosition().X < _left)
    {
      _direction = Direction.Standing;
      obj.SetPosition(new Vector2(_left, 0));
    }
    else if (_direction is Direction.Right && obj.GetPosition().X > _right)
    {
      _direction = Direction.Standing;
      obj.SetPosition(new Vector2(_right, 0));
    }
    
    if (_timer.TargetTrigger())
    {
      if (obj.GetPosition().X == _left) _direction = Direction.Right;
      else if (obj.GetPosition().X == _right) _direction = Direction.Left;
      _timer.StopAndResetTimer();
    }
  }
}