using FNaF2_RaylibCs.Source.Packages.Module;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Timer;

public class RandomTimer(double startTargetTimeInSeconds = .5f, double endTargetTimeInSeconds = 1f, bool activationStart = false, bool killOrLoopOnEnd = true, bool resetTargetWhenEnded = true) : SimpleTimer(startTargetTimeInSeconds, activationStart, killOrLoopOnEnd, resetTargetWhenEnded)
{
  protected override void SetNewTargetTime(Registry registry) =>
    TargetTime = new Random().NextDouble() * (endTargetTimeInSeconds - startTargetTimeInSeconds) + startTargetTimeInSeconds;
}