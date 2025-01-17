using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Animation;

public class SelectableAnimation(Vector2 position, float fps, Color color, AnimationPlayMode playMode, ImageDoubleStackResource resource, bool restartOnSceneChange = true) : 
  SimpleAnimation(position, fps, color, playMode, resource.GetSize(0), restartOnSceneChange)
{
  private int _currentPack;
  protected new ImageDoubleStackResource? Resource = resource;

  private void ChangedPack(Action action) { CurrentFrame = 0; action(); }

  public void PreviousPack() => ChangedPack(() => _currentPack = (_currentPack - 1 + Resource!.GetMaterial().Count) % Resource.GetMaterial().Count);
  public void NextPack() => ChangedPack(() => _currentPack = (_currentPack + 1) % Resource!.GetMaterial().Count);
  public void SetPack(int pack) => ChangedPack(() => _currentPack = pack % Resource!.GetMaterial().Count);
  public int GetPackIndex() => _currentPack;

  public override void Activation(Registry registry)
  {
    IgnoreParent = true;
    base.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    CurrentTime += Raylib.GetFrameTime();
    if (CurrentTime >= FrameTime)
    {
      CurrentFrame = (CurrentFrame + 1) % (Resource!.GetMaterial()[_currentPack].Count);
      CurrentTime -= FrameTime;
    }
    base.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    if (PlayMode == AnimationPlayMode.Addition) 
      for (int i = 0; i < CurrentFrame; i++) 
        Raylib.DrawTexturePro(Resource!.GetMaterial()[_currentPack][i], new Rectangle(Vector2.Zero, Resource.GetSize(_currentPack)), new Rectangle(Position, Size), Vector2.Zero, 0, Tint);
    else 
      Raylib.DrawTexturePro(Resource!.GetMaterial()[_currentPack][CurrentFrame], new Rectangle(Vector2.Zero, Resource.GetSize(_currentPack)), new Rectangle(Position, Resource.GetSize(_currentPack)), Vector2.Zero, 0, Tint);
    base.Draw(registry);
    DrawDebug(registry);
  }

  public override bool IsFinished() => CurrentFrame >= Resource!.GetMaterial()[_currentPack].Count - 1;
}