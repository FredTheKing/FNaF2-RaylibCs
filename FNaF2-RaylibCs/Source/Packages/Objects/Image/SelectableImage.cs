using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.ImageSelector;

public class SelectableImage(Vector2 position, ImageStackResource resource, Color? tint = null, Vector2? new_size = null) : SimpleImage(position, resource.GetSize(), tint, new_size)
{
  private int _current_frame = 0;
  
  public void PreviousFrame() => _current_frame = (_current_frame - 1 + resource.GetMaterial().Count) % resource.GetMaterial().Count;
  public void NextFrame() => _current_frame = (_current_frame + 1) % resource.GetMaterial().Count;
  public void SetFrame(int frame) => _current_frame = frame % resource.GetMaterial().Count;
  
  public new void Draw(Registry registry) => Raylib.DrawTexturePro(resource.GetMaterial()[_current_frame], new Rectangle(Vector2.Zero, resource.GetMaterial()[_current_frame].Width, resource.GetMaterial()[_current_frame].Height), new Rectangle(_position, _size), Vector2.Zero, 0, _tint);
}