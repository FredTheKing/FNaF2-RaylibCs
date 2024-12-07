using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class FontResource : MaterialTemplate
{
  private int _text_quality;
  
  public FontResource(String filename, int quality = 64) { _filename = filename; _text_quality = quality; }
  public FontResource(Font font) { _material = font; }

  public override bool IsMaterialLoaded()
  {
    if (_material is null) return false;
    return Raylib.IsFontReady(_material);
  }

  public override void Unload()
  {
    if (_material is null) return;
    Raylib.UnloadFont(_material);
    _material = null;
  }

  public override void Load() => _material = Raylib.LoadFontEx(_filename, _text_quality, null, 0);
  
  public override void CallDebuggerInfo(Registry registry) => ImGui.Text($" > Loaded: {IsMaterialLoaded()}");
}