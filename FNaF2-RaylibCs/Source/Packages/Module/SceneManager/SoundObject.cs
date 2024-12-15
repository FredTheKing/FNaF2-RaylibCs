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

public class SoundObject(SoundResource sndRes, bool activationStart = false, bool isLooped = false, bool multiScenable = false, bool allowedStacking = false, float defaultVolume = 1f) : ScriptTemplate
{
  private Sequence _state = Sequence.Stopped;
  private bool _paused;
  private float _volume = defaultVolume;

  private bool _volumeUpdateToDo = true;
  
  public void Play()
  {
    _state = isLooped ? Sequence.Looped : Sequence.Playing;
    RestartNewAudio();
  }
  public void Resume() => _paused = false;
  public void Pause() => _paused = true;
  public void Stop() => _state = Sequence.Stopped;
  public bool IsPlaying() => Raylib.IsSoundPlaying(sndRes.GetMaterial()) ?? false;
  
  private void RestartNewAudio()
  {
    if (!allowedStacking) StopCurrentAudio();
    Raylib.PlaySound(sndRes.GetMaterial());
  }

  private void StopCurrentAudio() => Raylib.StopSound(sndRes.GetMaterial());

  private void SetVolume(float newValue)
  {
    _volume = Math.Clamp(newValue, 0f, 1f);
    _volumeUpdateToDo = true;
  }
  public float GetVolume() => _volume;
  
  private void UpdateVolume(Registry registry) => Raylib.SetSoundVolume(sndRes.GetMaterial(), _volume * registry.GetSceneManager().GetMasterVolume());
  
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > State: {_state.ToString()}");
    if (_state is Sequence.Playing or Sequence.Looped) ImGui.Text($" > Actually Playing: {(IsPlaying() ? 1 : 0)}");
    if (_state == Sequence.Stopped) ImGui.Text($" > Is Looped: {(isLooped ? 1 : 0)}");
    ImGui.Text($" > Paused: {(_paused ? 1 : 0)}");
    ImGui.Text($" > Volume: {_volume}");
  }

  public override void Deactivation(Registry registry, string nextSceneName)
  {
    if (multiScenable && registry.GetSceneManager().GetScenes()[nextSceneName].GetSoundsList().Contains(this)) return;
    Stop();
    StopCurrentAudio();
  }

  public override void Activation(Registry registry)
  {
    if (activationStart && !IsPlaying()) Play();
  }

  public override void Update(Registry registry)
  {
    if (_volumeUpdateToDo) UpdateVolume(registry);
    
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
      case Sequence.Playing or Sequence.Looped when !IsPlaying() && !_paused:
        _state = Sequence.WhatsNext;
        break;
    }

    if (_state != Sequence.WhatsNext) return;
    _state = isLooped ? Sequence.Looped : Sequence.Stopped;
    if (isLooped) RestartNewAudio();
  }
}