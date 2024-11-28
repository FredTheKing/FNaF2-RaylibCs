using System.Numerics;
using ImGuiNET;
using Raylib_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class AnimationResource : MaterialTemplate
{
  private readonly Vector2 _size;
  protected readonly new List<String> _filename;
  protected readonly new List<Texture2D> _material = [];
  
  public AnimationResource(List<String> filenames) : base()
  {
    _filename = filenames;
    Texture2D _texture = Raylib.LoadTexture(_filename[0]);
    _size = new Vector2(_texture.Width, _texture.Height);
    Raylib.UnloadTexture(_texture);
  }
  public AnimationResource(List<Image> images) : base()
  {
    foreach (Image image in images) _material.Add(Raylib.LoadTextureFromImage(image));
    _size = new Vector2(_material[0].Width, _material[0].Height);
  }
  
  public AnimationResource(List<Texture2D> textures) : base() 
  { 
    _material = textures;
    _size = new Vector2(_material[0].Width, _material[0].Height);
  }
  
  public Vector2 GetSize() => _size;
  
  public new List<String> GetFilename() => _filename;
  public new List<Texture2D> GetMaterial() => _material;
  
  public new void Unload()
  {
    for (int i = 0; i < _filename.Count; i++)
    {
      Raylib.UnloadTexture(_material[i]);
    }
    _material.Clear();
  }

  public new void Load()
  {
    for (int i = 0; i < _filename.Count; i++)
    {
      _material.Add(Raylib.LoadTexture(_filename[i]));
    }
  }

  public void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Items Count: {_filename.Count | _material.Count}");
    ImGui.Text($" > Original Size: {_size.X}, {_size.Y}");
    ImGui.Text($" > Loaded: {(_filename != null ? _material.Count + "/" + _filename.Count : _material.Count)}");
  }
}