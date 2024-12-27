using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Image;

public class SelectableImage : SimpleImage
{
  public SelectableImage(Vector2 position, ImageStackResource resource, Color? tint = null, Vector2? newSize = null) : base(position, resource.GetSize(), tint, newSize) { Resource = resource; Tint = tint ?? Color.White; }
  public SelectableImage(Vector2 position, Vector2 originalSize, Color? tint = null, Vector2? newSize = null) : base(position, originalSize, tint, newSize) { Tint = tint ?? Color.White; }
  
  protected new ImageStackResource? Resource;
  protected int CurrentFrame;

  public override void CallDebuggerInfo(Registry registry)
  {
    if (Resource is not null) ImGui.Text($" > Current Frame: {CurrentFrame}/{Resource.GetMaterial().Count}");
    base.CallDebuggerInfo(registry);
  }

  protected void OnlyIfNotNull(Action action)
  {
    if (Resource is not null) action();
  }

  public virtual void PreviousFrame() => OnlyIfNotNull(() => { CurrentFrame = (CurrentFrame - 1 + Resource!.GetMaterial().Count) % Resource.GetMaterial().Count; });

  public virtual void NextFrame() => OnlyIfNotNull(() => { CurrentFrame = (CurrentFrame + 1) % Resource!.GetMaterial().Count; });

  public virtual void SetFrame(int frame) => OnlyIfNotNull(() => { CurrentFrame = frame % Resource!.GetMaterial().Count; });
  
  public int GetFrameIndex() => CurrentFrame;
  
  public void SetColor(Color color) => Tint = color;

  public override void Draw(Registry registry) => OnlyIfNotNull(() =>
  {
    Raylib.DrawTexturePro(Resource!.GetMaterial()[CurrentFrame], new Rectangle(Vector2.Zero, Resource.GetSize()), new Rectangle(Position, Size), Vector2.Zero, Rotation, Tint);
    DrawDebug(registry);
  });
}