using Raylib_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class ImageResource : MaterialTemplate
{
  private Texture2D _render_material;

  public ImageResource(String filename) : base()
  {
    _filename = filename;
  }
  public ImageResource(Image image) : base() 
  { 
    _material = image;
    _render_material = Raylib.LoadTextureFromImage(image);
  }
  
  public ImageResource(Texture2D texture) : base() 
  { 
    _render_material = texture;
    _material = Raylib.LoadImageFromTexture(texture);
  }
  
  public new void Unload()
  {
    if (_filename == null) return;
    Raylib.UnloadImage(_material);
    Raylib.UnloadTexture(_render_material);
  }

  public new void Load()
  {
    _material = Raylib.LoadImage(_filename);
    _render_material = Raylib.LoadTexture(_filename);
  }
  
  public new Image GetMaterial()
  {
    return _material;
  }
  
  public Texture2D GetRenderMaterial()
  {
    return _render_material;
  }
}