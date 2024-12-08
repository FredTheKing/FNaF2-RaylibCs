using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class FontResource : MaterialTemplate
{
  private int _textQuality;
  
  public FontResource(String filename, int quality = 64) { Filename = filename; _textQuality = quality; }
  public FontResource(Font font) { Material = font; }

  public override bool IsMaterialLoaded()
  {
    if (Material is null) return false;
    return Raylib.IsFontReady(Material);
  }

  public override void Unload()
  {
    if (Material is null) return;
    Raylib.UnloadFont(Material);
    Material = null;
  }

  public override void Load() => Material = Raylib.LoadFontEx(Filename, _textQuality, null, 0);
  
  public override void CallDebuggerInfo(Registry registry) => ImGui.Text($" > Loaded: {IsMaterialLoaded()}");
}