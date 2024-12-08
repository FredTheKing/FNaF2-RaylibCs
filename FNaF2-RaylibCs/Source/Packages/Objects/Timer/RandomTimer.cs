using FNaF2_RaylibCs.Source.Packages.Module;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Timer;

public class RandomTimer(double startTargetTimeInSeconds = .5f, double endTargetTimeInSeconds = 1f, bool startAtActivation = false, bool deleteOrLoopOnEnd = true, bool resetTargetWhenEnded = true) : SimpleTimer(startTargetTimeInSeconds, startAtActivation, deleteOrLoopOnEnd, resetTargetWhenEnded)
{
  protected override void SetNewTargetTime(Registry registry) =>
    TargetTime = new Random().NextDouble() * (endTargetTimeInSeconds - startTargetTimeInSeconds) + startTargetTimeInSeconds;
}