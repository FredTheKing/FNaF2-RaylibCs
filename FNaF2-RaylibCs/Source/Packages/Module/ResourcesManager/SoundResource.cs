using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class SoundResource : MaterialTemplate
{
  public SoundResource(string filename) { Filename = filename; }
  public SoundResource(Sound sound) { Material = sound; }

  public override bool IsMaterialLoaded()
  {
    if (Material is null) return false;
    return Raylib.IsSoundReady(Material);
  }
  
  public override void CallDebuggerInfo(Registry registry) => ImGui.Text($" > Loaded: {IsMaterialLoaded()}");

  public override void Unload()
  {
    if (Material is null) return;
    Raylib.UnloadSound(Material);
    Material = null;
  }

  public override void Load() => Material = Raylib.LoadSound(Filename);
}