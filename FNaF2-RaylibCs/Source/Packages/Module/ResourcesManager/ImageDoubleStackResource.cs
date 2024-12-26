using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class ImageDoubleStackResource : MaterialTemplate
{
  private readonly Vector2 _size;
  protected new List<List<string>>? Filename;
  protected new List<List<Texture2D>>? Material = [];
  private TextureFilter _filter = TextureFilter.Point;
  
  public override bool IsMaterialLoaded() => Material!.Sum(x => x.Count(a => Raylib.IsTextureReady(a))) == Filename!.Sum(x => x.Count);
  
  public ImageDoubleStackResource(List<List<string>> filenames)
  {
    Filename = filenames;
    Texture2D texture = Raylib.LoadTexture(Filename[0][0]);
    _size = new Vector2(texture.Width, texture.Height);
    Raylib.UnloadTexture(texture);
  }
  public ImageDoubleStackResource(List<List<Image>> images)
  {
    foreach (List<Image> unpackedImages in images)
    {
      Material!.Add([]);
      foreach (Image image in unpackedImages)
        Material!.Last().Add(Raylib.LoadTextureFromImage(image));
    }
    _size = new Vector2(Material![0][0].Width, Material[0][0].Height);
  }
  public ImageDoubleStackResource(List<List<Texture2D>> textures) 
  { 
    Material = textures;
    _size = new Vector2(Material[0][0].Width, Material[0][0].Height);
  }
  
  public Vector2 GetSize() => _size;
  
  public void SetFilter(TextureFilter filter) => _filter = filter;
  
  public new List<List<string>> GetFilename() => Filename!;
  public new List<List<Texture2D>> GetMaterial() => Material!;
  
  public override void Unload()
  {
    foreach (List<Texture2D> unpackedMaterials in Material!)
      foreach (Texture2D material in unpackedMaterials)
        Raylib.UnloadTexture(material);
    Material.Clear();
  }
  
  public override void Load()
  {
    foreach (List<string> unpackedFilenames in Filename!)
    {
      Material!.Add([]);
      foreach (string filename in unpackedFilenames)
      {
        Texture2D newTexture = Raylib.LoadTexture(filename);
        Raylib.SetTextureFilter(newTexture, _filter);
        Material!.Last().Add(newTexture);
      }
    }
  }
  
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Items Count: {Filename?.Sum(x => x.Count) ?? Material?.Sum(x => x.Count)}");
    ImGui.Text($" > Original Size: {_size.X}, {_size.Y}");
    ImGui.Text($" > Loaded: {(Filename != null ? Material?.Sum(x => x.Count) + "/" + Filename.Sum(x => x.Count) : Material?.Sum(x => x.Count))}");
  }
}