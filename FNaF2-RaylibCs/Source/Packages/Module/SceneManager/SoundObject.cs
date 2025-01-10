using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.SceneManager;

public enum Sequence
{
  Stopped,
  Playing,
  Looped,
  WhatsNext
}

public class SoundObject(SoundResource sndRes, bool activationStart = false, bool isLooped = false, bool multiScenable = false, bool allowedStacking = false, float defaultVolume = 1f) : ScriptTemplate
{
  public Sequence State { get; private set; } = Sequence.Stopped;
  public bool Paused { get; private set; }
  public float Volume { get; private set; } = defaultVolume;

  private bool _volumeUpdateToDo = true;
  
  public void Play()
  {
    State = isLooped ? Sequence.Looped : Sequence.Playing;
    RestartNewAudio();
  }
  public void Resume() => Paused = false;
  public void Pause() => Paused = true;
  public void Stop() => State = Sequence.Stopped;
  public bool IsPlaying() => Raylib.IsSoundPlaying(sndRes.GetMaterial()) ?? false;
  
  private void RestartNewAudio()
  {
    if (!allowedStacking) StopCurrentAudio();
    Raylib.PlaySound(sndRes.GetMaterial());
  }

  private void StopCurrentAudio() => Raylib.StopSound(sndRes.GetMaterial());

  private void SetVolume(float newValue)
  {
    Volume = Math.Clamp(newValue, 0f, 1f);
    _volumeUpdateToDo = true;
  }
  
  private void UpdateVolume(Registry registry) => Raylib.SetSoundVolume(sndRes.GetMaterial(), Volume * registry.scene.MasterVolume);
  
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > State: {State.ToString()}");
    if (State is Sequence.Playing or Sequence.Looped) ImGui.Text($" > Actually Playing: {(IsPlaying() ? 1 : 0)}");
    if (State == Sequence.Stopped) ImGui.Text($" > Is Looped: {(isLooped ? 1 : 0)}");
    ImGui.Text($" > Paused: {(Paused ? 1 : 0)}");
    ImGui.Text($" > Volume: {Volume}");
  }

  public override void Deactivation(Registry registry, string nextSceneName)
  {
    if (multiScenable && registry.scene.All[nextSceneName].GetSoundsList().Contains(this)) return;
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
    
    if (State is Sequence.Playing or Sequence.Looped)
    {
      if (Paused && IsPlaying()) Raylib.PauseSound(sndRes.GetMaterial());
      if (!Paused && !IsPlaying()) Raylib.ResumeSound(sndRes.GetMaterial());
    }
    
    switch (State)
    {
      case Sequence.Stopped:
        StopCurrentAudio();
        break;
      case Sequence.Playing or Sequence.Looped when !IsPlaying() && !Paused:
        State = Sequence.WhatsNext;
        break;
    }

    if (State != Sequence.WhatsNext) return;
    State = isLooped ? Sequence.Looped : Sequence.Stopped;
    if (isLooped) RestartNewAudio();
  }
}