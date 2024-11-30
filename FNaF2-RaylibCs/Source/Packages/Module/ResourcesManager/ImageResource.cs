using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class ImageResource : MaterialTemplate
{
  private readonly Vector2 _size;

  public ImageResource(String filename) : base()
  {
    _filename = filename;
    Texture2D _texture = Raylib.LoadTexture(_filename);
    _size = new Vector2(_texture.Width, _texture.Height);
    Raylib.UnloadTexture(_texture);
  }

  public ImageResource(Image image) : base() => _material = Raylib.LoadTextureFromImage(image);

  public ImageResource(Texture2D texture) : base() => _material = texture;

  public override bool IsMaterialLoaded()
  {
    if (_material is null) return false;
    return Raylib.IsTextureReady(_material);
  }
  
  public Vector2 GetSize() => _size;
  
  public new void Unload()
  {
    if (_material is null) return;
    Raylib.UnloadTexture(_material);
    _material = null;
  }

  public new void Load() => _material = Raylib.LoadTexture(_filename);
  
  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Original Size: {_size.X}, {_size.Y}");
    ImGui.Text($" > Loaded: {IsMaterialLoaded()}");
  }
}