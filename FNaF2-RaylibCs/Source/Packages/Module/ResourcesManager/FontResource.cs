using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class FontResource : MaterialTemplate
{
  public FontResource(String filename) : base() { _filename = filename; }
  public FontResource(Font font) : base() { _material = font; }

  public override bool IsMaterialLoaded()
  {
    if (_material is null) return false;
    return Raylib.IsFontReady(_material);
  }

  public new void Unload()
  {
    if (_material is null) return;
    Raylib.UnloadFont(_material);
    _material = null;
  }

  public new void Load() => _material = Raylib.LoadFont(_filename);
  
  public new void CallDebuggerInfo(Registry registry) => ImGui.Text($" > Loaded: {IsMaterialLoaded()}");
}