using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

public class GameMain : ScriptTemplate
{
  private const int ScrollBorder = 576;
  private const float DeadZone = 100f;
  private const float Sensitivity = 0.35f; // lower sensitivity = bigger number
  
  private float _scrollerPositionX;
  private int _officeFrame; // 0-neither, 1-left, 2-front, 3-right
  private bool _brokenLight;
  private bool _blackout;
  
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
    Registration.Objects.GameOfficeTable!.SetPosition(office.GetPosition() + new Vector2(575, 333));
  }

  private void LightButtonsReaction()
  {
    Registration.Objects.GameLeftLightSwitch!.SetFrame(_officeFrame == 1 ? 1 : 0);
    Registration.Objects.GameRightLightSwitch!.SetFrame(_officeFrame == 3 ? 1 : 0);
  }

  private void OfficeAssetReaction(Registry registry)
  {
    if (registry.GetShortcutManager().IsKeyDown(KeyboardKey.LeftControl))
    {
      string name = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.FrontFar)?.Name ?? Config.AnimatronicsNames.NoOne;
      _officeFrame = name switch
      {
        Config.AnimatronicsNames.NoOne => _brokenLight ? 4 : 2
      };
    }
    else if (Registration.Objects.GameLeftLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight)
    {
      string name = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Left)?.Name ?? Config.AnimatronicsNames.NoOne;
      _officeFrame = name switch
      {
        Config.AnimatronicsNames.NoOne => 1
      };
    }
    else if (Registration.Objects.GameRightLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight)
    {
      string name = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Right)?.Name ?? Config.AnimatronicsNames.NoOne;
      _officeFrame = name switch
      {
        Config.AnimatronicsNames.NoOne => 3
      };
    }
    else
    {
      _officeFrame = 0;
    }
  }

  private void UpdateOffice(Registry registry)
  {
    OfficeScroll();
    OfficeAssetReaction(registry);
    LightButtonsReaction();
    Registration.Objects.GameOffice!.SetFrame(_officeFrame);
    
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.B)) _brokenLight = !_brokenLight;
    
    _brokenLight = _blackout || _brokenLight;
  }

  public override void Activation(Registry registry)
  {
    Registration.Objects.GameOffice!.SetPosition(new Vector2(-(ScrollBorder / 2), 0));
  }

  public override void Update(Registry registry)
  {
    UpdateScroller();
    UpdateOffice(registry);
  }
}