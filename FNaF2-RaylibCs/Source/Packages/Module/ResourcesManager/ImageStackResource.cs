using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class ImageStackResource : MaterialTemplate
{
  private readonly Vector2 _size;
  protected new List<string> _filename;
  protected new List<Texture2D>? _material = [];

  public override bool IsMaterialLoaded() => _material.Count(x => Raylib.IsTextureReady(x)) == _filename.Count;

  public ImageStackResource(List<string> filenames) : base()
  {
    _filename = filenames;
    Texture2D _texture = Raylib.LoadTexture(_filename[0]);
    _size = new Vector2(_texture.Width, _texture.Height);
    Raylib.UnloadTexture(_texture);
  }
  public ImageStackResource(List<Image> images) : base()
  {
    foreach (Image image in images) 
      _material.Add(Raylib.LoadTextureFromImage(image));
    _size = new Vector2(_material[0].Width, _material[0].Height);
  }
  
  public ImageStackResource(List<Texture2D> textures) : base() 
  { 
    _material = textures;
    _size = new Vector2(_material[0].Width, _material[0].Height);
  }
  
  public Vector2 GetSize() => _size;
  
  public new List<string> GetFilename() => _filename;
  public new List<Texture2D> GetMaterial() => _material;
  
  public override void Unload()
  {
    foreach (Texture2D material in _material)
      Raylib.UnloadTexture(material);
    _material.Clear();
  }

  public override void Load()
  {
    foreach (string filename in _filename)
      _material.Add(Raylib.LoadTexture(filename));
  }

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Items Count: {_filename.Count | _material.Count}");
    ImGui.Text($" > Original Size: {_size.X}, {_size.Y}");
    ImGui.Text($" > Loaded: {(_filename != null ? _material.Count + "/" + _filename.Count : _material.Count)}");
  }
}