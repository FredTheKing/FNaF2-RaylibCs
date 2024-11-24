using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;

namespace RaylibArteSonat.Source.Packages.Objects.Timer;

public class SimpleTimer(double target_time_in_seconds = 1f, bool start_at_activation = false, bool delete_or_loop_on_end = true, bool reset_target_when_ended = true) : ObjectTemplate
{
  protected double _time = 0f;
  protected double _current_time = 0f;
  protected double _start_time = 0f;
  protected double _target_time = target_time_in_seconds;
  protected bool _go = false;
  protected bool _dead = false;
  protected bool _target_activate = false;
  
  protected bool _start_at_activation = start_at_activation;
  protected bool _delete_or_loop_on_end = delete_or_loop_on_end;
  protected bool _reset_target_when_ended = reset_target_when_ended;
  
  protected string debugger_name = "Timer-" + new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4)
    .Select(s => s[new Random().Next(s.Length)]).ToArray());
  

  public new void CallDebuggerInfo(Registry registry)
  {
    if(ImGui.TreeNode(debugger_name))
    {
      ImGui.Text($" > Time: {_time}");
      ImGui.Text($" > Start Time: {_start_time}");
      ImGui.Text($" > Current Time: {_current_time}");
      ImGui.Separator();
      ImGui.Text($" > Going: {(_go ? 1 : 0)}");
      ImGui.Text($" > Dead: {(_dead ? 1 : 0)}");
      ImGui.Text($" > Target Time: {_target_time}");
      ImGui.Text($" > Triggered: {(_target_activate ? 1 : 0)}");
      ImGui.Separator();
      ImGui.Text($" > Start on Activation: {(_start_at_activation ? 1 : 0)}");
      ImGui.Text($" > Todo on end: {(_delete_or_loop_on_end ? "Loop" : "Delete")}");
      if (_delete_or_loop_on_end) ImGui.Text($" > Auto reset on end: {(_reset_target_when_ended ? 1 : 0)}");

      ImGui.TreePop();
    }
  }
  
  public bool IsEnded() => _target_activate;
  
  public bool IsWorking() => _go;
  
  public void ContinuousStartTimer()
  {
    if (IsWorking()) return;
    StartTimer();
  }
  
  public void StartTimer()
  {
    _time = 0f;
    _start_time = Raylib.GetTime();
    _go = true;
  }

  public void StopTimer()
  {
    _start_time = -1f;
    _go = false;
    _target_activate = false;
  }
  
  public void StopAndResetTimer()
  {
    _time = 0f;
    StopTimer();
  }

  public double GetTime() => _time;

  public void KillTimer()
  {
    _go = false;
    _dead = true;
  }

  public new void Activation(Registry registry)
  {
    if (!_start_at_activation) return;
    StartTimer();
    
    base.Activation(registry);
  }
  
  public new void Update(Registry registry)
  {
    _current_time = Raylib.GetTime();
    if (_go) _time = _current_time - _start_time;
    
    if (_reset_target_when_ended) _target_activate = false;
    if (_time >= _target_time)
    {
      _target_activate = true;
      if (_delete_or_loop_on_end)
      {
        if (_reset_target_when_ended)
        {
          StartTimer();
        }
      } else KillTimer();
    }
    
    base.Update(registry);
  }
}