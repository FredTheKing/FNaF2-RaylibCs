using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SelectablePackedImage(Vector2 position, ImageDoubleStackResource resource, Color? tint = null, Vector2? newSize = null) : SelectableImage(position, resource.GetSize(0), tint, newSize)
{
  private int _currentPack;
  protected new ImageDoubleStackResource Resource = resource;

  public override void CallDebuggerInfo(Registry registry)
  {
    base.CallDebuggerInfo(registry);
    ImGui.Separator();
    ImGui.Text($" > Current Pack: {_currentPack}");
    ImGui.Text($" > Current Pack: {CurrentFrame}");
  }

  public override void PreviousFrame() => CurrentFrame = (CurrentFrame - 1 + Resource.GetMaterial()[_currentPack].Count) % Resource.GetMaterial()[_currentPack].Count;
  public void PreviousPack() => _currentPack = (_currentPack - 1 + Resource.GetMaterial().Count) % Resource.GetMaterial().Count;

  public override void NextFrame() => CurrentFrame = (CurrentFrame + 1) % Resource.GetMaterial()[_currentPack].Count;
  public void NextPack() => _currentPack = (_currentPack + 1) % Resource.GetMaterial().Count;

  public override void SetFrame(int frame) => CurrentFrame = frame % Resource.GetMaterial()[_currentPack].Count;
  public void SetPack(int pack) => _currentPack = pack % Resource.GetMaterial().Count;
  
  public int GetPackIndex() => _currentPack;

  public override void Draw(Registry registry) =>
    Raylib.DrawTexturePro(Resource.GetMaterial()[_currentPack][CurrentFrame], new Rectangle(Vector2.Zero, Resource.GetSize(_currentPack)), new Rectangle(Position, Resource.GetSize(_currentPack)), Vector2.Zero, Rotation, Tint);
}