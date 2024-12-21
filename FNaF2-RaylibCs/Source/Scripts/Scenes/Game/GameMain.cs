using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;
using static FNaF2_RaylibCs.Source.Config.AnimatronicsNames;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

public class GameMain : ScriptTemplate
{
  private const int ScrollBorder = 576;
  private const float DeadZone = 100f;
  private const float Sensitivity = 0.35f; // lower sensitivity = bigger number

  private float _scrollerPositionX;
  private int _officeFrame; // 0-neither, 1-left, 2-front, 3-right
  private bool _balloonMf = false;
  private bool _mangleMf = true;
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
    Registration.Objects.GameOfficeBalloonBoy!.SetPosition(office.GetPosition() + new Vector2(262, 270));
    Registration.Objects.GameOfficeMangle!.SetPosition(office.GetPosition() + new Vector2(700, 0));
  }

  private void LightButtonsReaction()
  {
    Registration.Objects.GameLeftLightSwitch!.SetFrame(_officeFrame == 1 ? 1 : 0);
    Registration.Objects.GameRightLightSwitch!.SetFrame(_officeFrame == 3 ? 1 : 0);
  }

  private void OfficeAssetReaction(Registry registry)
  {
    string nameInside = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Inside)?.Name ?? NoOne;
    
    if (_mangleMf) registry.GetSceneManager().GetCurrentScene().ShowLayer(4);
    else registry.GetSceneManager().GetCurrentScene().HideLayer(4);
    if (_balloonMf) registry.GetSceneManager().GetCurrentScene().ShowLayer(5);
    else registry.GetSceneManager().GetCurrentScene().HideLayer(5);
    
    if (nameInside != NoOne)
    {
      _officeFrame = nameInside switch
      {
        WitheredFreddy => 19,
        WitheredBonnie => 20,
        WitheredChica => 21,
        _ => 0
      };
        
      registry.GetSceneManager().GetCurrentScene().HideLayer(1);
      registry.GetSceneManager().GetCurrentScene().HideLayer(2);
      registry.GetSceneManager().GetCurrentScene().HideLayer(3);
        
      switch (nameInside)
      {
        case ToyFreddy:
          registry.GetSceneManager().GetCurrentScene().ShowLayer(1);
          break;
        case ToyBonnie:
          registry.GetSceneManager().GetCurrentScene().ShowLayer(2);
          break;
        case ToyChica:
          registry.GetSceneManager().GetCurrentScene().ShowLayer(3);
          break;
      }
        
      if (!_blackout) _blackout = true;
      return;
    }
    
    if (registry.GetShortcutManager().IsKeyDown(KeyboardKey.LeftControl))
    {
      string nameFar = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.FrontFar)?.Name ?? NoOne;
      string nameClose = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.FrontClose)?.Name ?? NoOne;
      
      if (_brokenLight && nameInside is not WitheredFreddy or WitheredBonnie or WitheredChica)
      {
        _officeFrame = 4;
        return;
      }
      
      _officeFrame = nameFar switch
      {
        NoOne when nameClose == NoOne => 2,
        ToyFreddy when nameClose == NoOne => 9,
        NoOne when nameClose == ToyFreddy => 10,
        ToyChica when nameClose == NoOne => 11,
        WitheredBonnie when nameClose == NoOne => 12,
        NoOne when nameClose == WitheredFreddy => 13,
        NoOne when nameClose == Mangle => 14,
        WitheredFoxy when nameClose == NoOne => 15,
        WitheredFoxy when nameClose == Mangle => 16,
        WitheredFoxy when nameClose == WitheredBonnie => 17,
        GoldenFreddy when nameClose == NoOne => 18,
        _ => 0
      };
    }
    else if (Registration.Objects.GameLeftLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight)
    {
      string name = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Left)?.Name ?? NoOne;
      _officeFrame = name switch
      {
        NoOne => 1,
        ToyChica => 6,
        BalloonBoy => 5
      };
    }
    else if (Registration.Objects.GameRightLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight)
    {
      string name = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Right)?.Name ?? NoOne;
      _officeFrame = name switch
      {
        NoOne => 3,
        ToyBonnie => 7,
        Mangle => 8
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
    
    _brokenLight = _blackout || _balloonMf || _brokenLight;
  }

  public override void Activation(Registry registry)
  {
    Registration.Objects.GameOffice!.SetPosition(new Vector2(-(ScrollBorder / 2), 0));
    _brokenLight = false;
    _blackout = false;
    
    registry.GetSceneManager().GetCurrentScene().HideLayer(1);
    registry.GetSceneManager().GetCurrentScene().HideLayer(2);
    registry.GetSceneManager().GetCurrentScene().HideLayer(3);
    registry.GetSceneManager().GetCurrentScene().HideLayer(4);
    registry.GetSceneManager().GetCurrentScene().HideLayer(5);
  }

  public override void Update(Registry registry)
  {
    UpdateScroller();
    UpdateOffice(registry);
  }
}