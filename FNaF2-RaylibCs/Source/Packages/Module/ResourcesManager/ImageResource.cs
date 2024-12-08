using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class ImageResource : MaterialTemplate
{
  private readonly Vector2 _size;

  public ImageResource(String filename)
  {
    Filename = filename;
    Texture2D texture = Raylib.LoadTexture(Filename);
    _size = new Vector2(texture.Width, texture.Height);
    Raylib.UnloadTexture(texture);
  }

  public ImageResource(Image image) => Material = Raylib.LoadTextureFromImage(image);

  public ImageResource(Texture2D texture) => Material = texture;

  public override bool IsMaterialLoaded()
  {
    if (Material is null) return false;
    return Raylib.IsTextureReady(Material);
  }
  
  public Vector2 GetSize() => _size;
  
  public override void Unload()
  {
    if (Material is null) return;
    Raylib.UnloadTexture(Material);
    Material = null;
  }

  public override void Load() => Material = Raylib.LoadTexture(Filename);
  
  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Original Size: {_size.X}, {_size.Y}");
    ImGui.Text($" > Loaded: {IsMaterialLoaded()}");
  }
}