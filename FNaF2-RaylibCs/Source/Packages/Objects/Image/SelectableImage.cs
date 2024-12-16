using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SelectableImage(Vector2 position, ImageStackResource resource, Color? tint = null, Vector2? newSize = null, float rotation = 0) : SimpleImage(position, resource.GetSize(), tint, newSize, rotation)
{
  private int _currentFrame;
  
  public void PreviousFrame() => _currentFrame = (_currentFrame - 1 + resource.GetMaterial().Count) % resource.GetMaterial().Count;
  public void NextFrame() => _currentFrame = (_currentFrame + 1) % resource.GetMaterial().Count;
  public void SetFrame(int frame) => _currentFrame = frame % resource.GetMaterial().Count;
  public int GetFrameIndex() => _currentFrame;
  
  public void SetColor(Color color) => Tint = color;
  
  public override void Draw(Registry registry) => Raylib.DrawTexturePro(resource.GetMaterial()[_currentFrame], new Rectangle(Vector2.Zero, resource.GetMaterial()[_currentFrame].Width, resource.GetMaterial()[_currentFrame].Height), new Rectangle(Position, Size), Vector2.Zero, Rotation, Tint);
}