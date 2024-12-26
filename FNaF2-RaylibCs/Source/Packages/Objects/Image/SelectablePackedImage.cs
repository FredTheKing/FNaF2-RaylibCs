using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SelectablePackedImage(Vector2 position, ImageDoubleStackResource resource, Color? tint = null, Vector2? newSize = null) : SelectableImage(position, resource.GetSize(), tint, newSize)
{
  private int _currentPack;
  protected new ImageDoubleStackResource Resource = resource;
  
  public void PreviousPack() => _currentPack = (_currentPack - 1 + Resource.GetMaterial().Count) % Resource.GetMaterial().Count;

  public void NextPack() => _currentPack = (_currentPack + 1) % Resource.GetMaterial().Count;
  
  public void SetPack(int pack) => _currentPack = pack % Resource.GetMaterial().Count;
  
  public int GetPackIndex() => _currentPack;

  public override void Draw(Registry registry) =>
    Raylib.DrawTexturePro(Resource.GetMaterial()[_currentPack][CurrentFrame], new Rectangle(Vector2.Zero, Resource.GetSize()), new Rectangle(Position, Size), Vector2.Zero, Rotation, Tint);
}