using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Timer;

public class SimpleTimer(double targetTimeInSeconds = 1f, bool activationStart = false, bool deleteOrLoopOnEnd = true, bool resetTargetWhenEnded = true) : ObjectTemplate
{
  protected double Time;
  protected double CurrentTime;
  protected double StartTime;
  protected double TargetTime = targetTimeInSeconds;
  protected bool Go;
  protected bool Dead;
  protected bool TargetActivate;
  
  protected bool ActivationStart = activationStart;
  protected bool DeleteOrLoopOnEnd = deleteOrLoopOnEnd;
  protected bool ResetTargetWhenEnded = resetTargetWhenEnded;
  
  protected string DebuggerName = "Timer-" + new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4)
    .Select(s => s[new Random().Next(s.Length)]).ToArray());
  

  public override void CallDebuggerInfo(Registry registry)
  {
    if(ImGui.TreeNode(DebuggerName))
    {
      ImGui.Text($" > Time: {Time}");
      ImGui.Text($" > Start Time: {StartTime}");
      ImGui.Text($" > Current Time: {CurrentTime}");
      ImGui.Separator();
      ImGui.Text($" > Going: {(Go ? 1 : 0)}");
      ImGui.Text($" > Dead: {(Dead ? 1 : 0)}");
      ImGui.Text($" > Target Time: {TargetTime}");
      ImGui.Text($" > Triggered: {(TargetActivate ? 1 : 0)}");
      ImGui.Separator();
      ImGui.Text($" > Start on Activation: {(ActivationStart ? 1 : 0)}");
      ImGui.Text($" > Todo on end: {(DeleteOrLoopOnEnd ? "Loop" : "Delete")}");
      if (DeleteOrLoopOnEnd) ImGui.Text($" > Auto reset on end: {(ResetTargetWhenEnded ? 1 : 0)}");

      ImGui.TreePop();
    }
  }
  
  public bool TargetTrigger() => TargetActivate;
  
  public bool IsWorking() => Go;
  
  public void ContinuousStartTimer()
  {
    if (IsWorking()) return;
    StartTimer();
  }
  
  public void StartTimer()
  {
    Time = 0f;
    StartTime = Raylib.GetTime();
    Go = true;
  }

  public void StopTimer()
  {
    StartTime = -1f;
    Go = false;
    TargetActivate = false;
  }
  
  public void StopAndResetTimer()
  {
    Time = 0f;
    StopTimer();
  }

  public double GetTime() => Time;
  
  public double GetTargetTime() => TargetTime;

  public double GetTimeLeft() => TargetTime - Time;

  public void KillTimer()
  {
    Go = false;
    Dead = true;
  }

  public new void Activation(Registry registry)
  {
    if (!ActivationStart) return;
    StartTimer();
    
    base.Activation(registry);
  }

  protected virtual void SetNewTargetTime(Registry registry) { }
  
  public override void Update(Registry registry)
  {
    CurrentTime = Raylib.GetTime();
    if (Go) Time = CurrentTime - StartTime;
    
    if (ResetTargetWhenEnded) TargetActivate = false;
    if (Time >= TargetTime)
    {
      TargetActivate = true;
      SetNewTargetTime(registry);
      if (DeleteOrLoopOnEnd)
      {
        if (ResetTargetWhenEnded)
        {
          StartTimer();
        }
      } else KillTimer();
    }
    
    base.Update(registry);
  }
}