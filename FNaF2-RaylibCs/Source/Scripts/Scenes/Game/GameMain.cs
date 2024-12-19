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
  private int _light; // 0-neither, 1-left, 2-front, 3-right
  
  private void UpdateScroller()
  {
    _scrollerPositionX = Raylib.GetMouseX() - Registration.Objects.GameCentralScroller!.GetPosition().X - 2;
    if (_scrollerPositionX is > -DeadZone and < DeadZone || Raylib.GetMouseX() > Config.WindowWidth) _scrollerPositionX = 0;
    else if (_scrollerPositionX > DeadZone) _scrollerPositionX -= DeadZone;
    else if (_scrollerPositionX < -DeadZone) _scrollerPositionX += DeadZone;
    _scrollerPositionX /= Sensitivity;
  }

  private void OfficeScroll()
  {
    var office = Registration.Objects.GameOffice!;
    
    office.AddPosition(new Vector2(-_scrollerPositionX * Raylib.GetFrameTime(), 0));
    office.SetPosition(new Vector2(Math.Clamp(office.GetPosition().X, -ScrollBorder, 0), 0));
    Registration.Objects.GameLeftLightSwitch!.SetPosition(office.GetPosition() + new Vector2(100, 356));
    Registration.Objects.GameRightLightSwitch!.SetPosition(office.GetPosition() + new Vector2(1408, 356));
  }

  private void LightButtonsReaction()
  {
    if(Registration.Objects.GameLeftLightSwitch!.GetHitbox().GetMousePress(MouseButton.Left)) _light = _light == 1 ? 0 : 1;
    if(Registration.Objects.GameRightLightSwitch!.GetHitbox().GetMousePress(MouseButton.Left)) _light = _light == 3 ? 0 : 3;
    
    Registration.Objects.GameLeftLightSwitch.SetFrame(_light == 1 ? 1 : 0);
    Registration.Objects.GameRightLightSwitch.SetFrame(_light == 3 ? 1 : 0);
  }

  private void UpdateOffice()
  {
    OfficeScroll();
    LightButtonsReaction();
    Registration.Objects.GameOffice!.SetFrame(_light);
  }

  public override void Activation(Registry registry)
  {
    Registration.Objects.GameOffice!.SetPosition(new Vector2(-(ScrollBorder / 2), 0));
  }

  public override void Update(Registry registry)
  {
    UpdateScroller();
    UpdateOffice();
  }
}