using FNaF2_RaylibCs.Source.Packages.Module;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Timer;

public class RandomTimer(double start_target_time_in_seconds = .5f, double end_target_time_in_seconds = 1f, bool start_at_activation = false, bool delete_or_loop_on_end = true, bool reset_target_when_ended = true) : SimpleTimer(start_target_time_in_seconds, start_at_activation, delete_or_loop_on_end, reset_target_when_ended)
{
  protected override void SetNewTargetTime(Registry registry)
  {
    _target_time = new Random().NextDouble() * (end_target_time_in_seconds - start_target_time_in_seconds) + start_target_time_in_seconds;
  }
}