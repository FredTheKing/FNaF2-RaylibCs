using Raylib_cs;
namespace RaylibArteSonat.Source.Packages.Module;

public class FontResource : MaterialTemplate
{
  public FontResource(String filename) : base() { _filename = filename; }
  public FontResource(Font font) : base() { _material = font; }
  
  public new void Unload()
  {
    if (_filename == null) return;
    Raylib.UnloadFont(_material);
  }

  public new void Load()
  { 
    _material = Raylib.LoadFont(_filename);
  }
}