using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

public class GameMain : ScriptTemplate
{
  private const int ScrollBorder = 576;
  private const float DeadZone = 100f;
  private const float Sensitivity = 0.35f; // lower sensitivity = bigger number
  private float _scrollerPositionX;
  
  private void UpdateScroller()
  {
    _scrollerPositionX = Raylib.GetMouseX() - Registration.Objects.GameCentralScroller!.GetPosition().X - 2;
    if ((_scrollerPositionX > -DeadZone && _scrollerPositionX < DeadZone) || Raylib.GetMouseX() > Config.WindowWidth) _scrollerPositionX = 0;
    else if (_scrollerPositionX > DeadZone) _scrollerPositionX -= DeadZone;
    else if (_scrollerPositionX < -DeadZone) _scrollerPositionX += DeadZone;
    _scrollerPositionX /= Sensitivity;
  }

  private void OfficeScroll()
  {
    Registration.Objects.GameOffice!.AddPosition(new Vector2(-_scrollerPositionX * Raylib.GetFrameTime(), 0));
    Registration.Objects.GameOffice.SetPosition(new Vector2(Math.Clamp(Registration.Objects.GameOffice.GetPosition().X, -ScrollBorder, 0), 0));
  }

  public override void Activation(Registry registry)
  {
    Registration.Objects.GameOffice!.SetPosition(new Vector2(-(ScrollBorder / 2), 0));
  }

  public override void Update(Registry registry)
  {
    UpdateScroller();
    OfficeScroll();
  }
}