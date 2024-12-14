using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.SceneManager;

internal enum Sequence
{
  Stopped,
  Playing,
  Looped,
  WhatsNext
}

public class SoundObject(SoundResource sndRes, bool activationStart, bool isLooped) : ScriptTemplate
{
  private Sequence _state = Sequence.Stopped;
  private bool _paused;
  private float _volume;
  
  public void Play()
  {
    _state = isLooped ? Sequence.Looped : Sequence.Playing;
    RestartNewAudio();
  }
  public void Resume() => _paused = false;
  public void Pause() => _paused = true;
  public void Stop() => _state = Sequence.Stopped;
  public bool IsPlaying() => Raylib.IsSoundPlaying(sndRes.GetMaterial());
  
  private void RestartNewAudio()
  {
    StopCurrentAudio();
    Raylib.PlaySound(sndRes.GetMaterial());
  }

  private void StopCurrentAudio() => Raylib.StopSound(sndRes.GetMaterial());
  
  
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > State: {_state.ToString()}");
    if (_state == Sequence.Playing) ImGui.Text($" > Actually Playing: {(IsPlaying() ? 1 : 0)}");
    ImGui.Text($" > Paused: {(_paused ? 1 : 0)}");
    ImGui.Text($" > Volume: {_volume}");
  }

  public override void Activation(Registry registry)
  {
    if (activationStart) Play();
  }

  public override void Update(Registry registry)
  {
    Raylib.SetSoundVolume(sndRes.GetMaterial(), _volume * registry.GetSceneManager().GetMasterVolume());
    
    if (_state is Sequence.Playing or Sequence.Looped)
    {
      if (_paused && IsPlaying()) Raylib.PauseSound(sndRes.GetMaterial());
      if (!_paused && !IsPlaying()) Raylib.ResumeSound(sndRes.GetMaterial());
    }
    
    switch (_state)
    {
      case Sequence.Stopped:
        StopCurrentAudio();
        break;
      case Sequence.Playing when !IsPlaying() && !_paused:
        _state = Sequence.WhatsNext;
        break;
    }

    if (_state == Sequence.WhatsNext)
    {
      if (isLooped) RestartNewAudio();
      else _state = Sequence.Stopped;
    }
  }
}