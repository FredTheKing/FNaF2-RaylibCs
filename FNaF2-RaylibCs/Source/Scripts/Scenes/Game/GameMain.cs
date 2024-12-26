using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;
using static FNaF2_RaylibCs.Source.Config.AnimatronicsNames;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

public class GameMain : ScriptTemplate
{
  private const int ScrollBorder = 576;
  private const float DeadZone = 100f;
  private const float Sensitivity = 0.35f; // lower sensitivity = bigger number

  private SimpleTimer _blackoutFlickeringTimer = new(.1);
  private SimpleTimer _blackoutDurationTimer = new(5);
  private float _blackoutCustomAlpha;
  
  private float _scrollerPositionX;
  private int _officeFrame;
  private bool _balloonMf;
  private bool _mangleMf;
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
    Registration.Objects.GameOfficeToyFreddy!.SetPosition(office.GetPosition() + new Vector2(820, 0));
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
    
    var officers = new Dictionary<string, int>
    {
      { ToyFreddy, 1 },
      { ToyBonnie, 2 },
      { ToyChica, 3 }
    };

    if (nameInside == NoOne)
    {
      foreach (var animatronic in officers.Keys)
        if (nameInside != animatronic) registry.GetSceneManager().GetCurrentScene().HideLayer(officers[animatronic]);
    }
    else
    {
      _officeFrame = nameInside switch
      {
        WitheredFreddy => 19,
        WitheredBonnie => 20,
        WitheredChica => 21,
        _ => 0
      };
      
      foreach (var animatronic in officers.Keys.Where(animatronic => nameInside == animatronic))
        registry.GetSceneManager().GetCurrentScene().ShowLayer(officers[animatronic]);
        
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
    
    _brokenLight = _blackout || _balloonMf;
  }

  private void UpdateBlackout(Registry registry)
  {
    _blackoutFlickeringTimer.Update(registry);
    _blackoutDurationTimer.Update(registry);
    
    if (_blackoutDurationTimer.TargetTrigger())
    {
      _blackout = false;
      _blackoutFlickeringTimer.StopAndResetTimer();
      _blackoutDurationTimer.StopAndResetTimer();
      _blackoutCustomAlpha = 255;
      registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Inside)!.Move(registry);
    }

    if (_blackout)
    {
      _blackoutFlickeringTimer.ContinuousStartTimer();
      _blackoutDurationTimer.ContinuousStartTimer();
    }
    else
    {
      if (_blackoutCustomAlpha > 0) Math.Clamp(_blackoutCustomAlpha -= 71 * Raylib.GetFrameTime(), 0, 255);
      Registration.Objects.GameBlackoutRectangle!.SetColor(new Color(0, 0, 0, (int)_blackoutCustomAlpha));
    }

    if (_blackoutDurationTimer.GetTimeLeft() > 1.6f && _blackout)
    {
      if (!_blackoutFlickeringTimer.TargetTrigger()) return;
      int randomValue = new Random().Next(4);
      Color newColor = randomValue switch
      {
        0 => new Color {R = 0, G = 0, B = 0, A = 243},
        1 => new Color {R = 0, G = 0, B = 0, A = 210},
        2 => new Color {R = 0, G = 0, B = 0, A = 132},
        _ => new Color {R = 0, G = 0, B = 0, A = 40}
      };
      Registration.Objects.GameBlackoutRectangle!.SetColor(newColor);
    }
    else if (_blackout) 
      Registration.Objects.GameBlackoutRectangle!.SetColor(Color.Black);
  }

  public override void Activation(Registry registry)
  {
    _blackoutFlickeringTimer.Activation(registry);
    _blackoutDurationTimer.Activation(registry);
    _blackoutCustomAlpha = 0;
    
    Registration.Objects.GameOffice!.SetPosition(new Vector2(-(ScrollBorder / 2), 0));
    
    registry.GetSceneManager().GetCurrentScene().HideLayer(1);
    registry.GetSceneManager().GetCurrentScene().HideLayer(2);
    registry.GetSceneManager().GetCurrentScene().HideLayer(3);
    registry.GetSceneManager().GetCurrentScene().HideLayer(4);
    registry.GetSceneManager().GetCurrentScene().HideLayer(5);
    
    registry.GetFNaF().GetAnimatronicManager().Activation(registry);
    registry.GetFNaF().GetAnimatronicManager().Update(registry);
    _balloonMf = false;
    _mangleMf = false;
    _brokenLight = false;
    _blackout = false;
  }

  public override void Update(Registry registry)
  {
    UpdateScroller();
    UpdateOffice(registry);
    UpdateBlackout(registry);
    Console.WriteLine("Debug: " + (_brokenLight ? 1 : 0) + " " + (_blackout ? 1 : 0));
  }
}