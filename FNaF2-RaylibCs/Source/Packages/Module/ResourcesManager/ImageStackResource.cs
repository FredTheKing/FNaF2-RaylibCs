using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class ImageStackResource : MaterialTemplate
{
  private readonly Vector2 _size;
  protected new List<string>? Filename;
  protected new List<Texture2D>? Material = [];
  private TextureFilter _filter = TextureFilter.Point;

  public override bool IsMaterialLoaded() => Material!.Count(x => Raylib.IsTextureReady(x)) == Filename!.Count;

  public ImageStackResource(List<string> filenames)
  {
    Filename = filenames;
    Texture2D texture = Raylib.LoadTexture(Filename[0]);
    _size = new Vector2(texture.Width, texture.Height);
    Raylib.UnloadTexture(texture);
  }
  public ImageStackResource(List<Image> images)
  {
    foreach (Image image in images) 
      Material!.Add(Raylib.LoadTextureFromImage(image));
    _size = new Vector2(Material![0].Width, Material[0].Height);
  }
  
  public ImageStackResource(List<Texture2D> textures) 
  { 
    Material = textures;
    _size = new Vector2(Material[0].Width, Material[0].Height);
  }
  
  public Vector2 GetSize() => _size;
  
  public void SetFilter(TextureFilter filter) => _filter = filter;
  
  public new List<string> GetFilename() => Filename!;
  public new List<Texture2D> GetMaterial() => Material!;
  
  public override void Unload()
  {
    foreach (Texture2D material in Material!)
      Raylib.UnloadTexture(material);
    Material.Clear();
  }

  public override void Load()
  {
    foreach (string filename in Filename!)
    {
      Texture2D newTexture = Raylib.LoadTexture(filename);
      Raylib.SetTextureFilter(newTexture, _filter);
      Material!.Add(newTexture);
    }
  }

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Items Count: {Filename!.Count | Material!.Count}");
    ImGui.Text($" > Original Size: {_size.X}, {_size.Y}");
    ImGui.Text($" > Loaded: {(Filename != null ? Material.Count + "/" + Filename.Count : Material.Count)}");
  }
}