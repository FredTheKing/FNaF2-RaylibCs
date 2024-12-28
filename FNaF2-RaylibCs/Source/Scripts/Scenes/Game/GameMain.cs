using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;
using static FNaF2_RaylibCs.Source.Config.AnimatronicsNames;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

internal enum Tool { Nothing, Mask, Camera }

public class GameMain : ScriptTemplate
{
  private const float BatteryUsageSpeed = 20f; // 135f is default
  private const int ScrollBorder = 576;
  private const float DeadZone = 100f;
  private const float Sensitivity = 0.35f; // lower sensitivity = bigger number

  private SimpleTimer _blackoutFlickeringTimer = new(.1);
  private SimpleTimer _blackoutDurationTimer = new(5);
  private float _blackoutCustomAlpha;
  
  private float _battery = 10000;
  private float _scrollerPositionX;
  private Tool _currentTool = Tool.Nothing;
  private int _assetFrame;
  private int _cameraPack;
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
    var office = Registration.Objects.GameOfficeCamera!;
    
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
    Registration.Objects.GameLeftLightSwitch!.SetFrame(_assetFrame is 1 or 6 or 5 ? 1 : 0);
    Registration.Objects.GameRightLightSwitch!.SetFrame(_assetFrame is 3 or 7 or 8 ? 1 : 0);
  }

  private void OfficeAssetReaction(Registry registry)
  {
    List<string> nameInside = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Inside)?.Select(a => a.Name).ToList() ?? [];
    Animatronic? actualInside = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Inside)?.FirstOrDefault(a => a.Name is not Mangle and not BalloonBoy);
    
    var separatedAssetsAnimatronics = new Dictionary<string, int>
    {
      { ToyFreddy, 1 },
      { ToyBonnie, 2 },
      { ToyChica, 3 },
      { Mangle, 4 },
      { BalloonBoy, 5 }
    };

    foreach (KeyValuePair<string,int> pair in separatedAssetsAnimatronics)
    {
      if (nameInside.Contains(pair.Key)) registry.GetSceneManager().GetCurrentScene().ShowLayer(pair.Value);
      else registry.GetSceneManager().GetCurrentScene().HideLayer(pair.Value);
    }

    if (actualInside is not null)
    {
      _assetFrame = actualInside.Name switch
      {
        WitheredFreddy => 19,
        WitheredBonnie => 20,
        WitheredChica => 21,
        _ => 0
      };
        
      if (!_blackout) _blackout = true;
      return;
    }
    
    if (registry.GetShortcutManager().IsKeyDown(KeyboardKey.LeftControl))
    {
      List<string> nameFront = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Front)?.Select(a => a.Name).ToList() ?? [];

      if (_battery != 0)
      {
        _battery -= 10000 / BatteryUsageSpeed * Raylib.GetFrameTime();
        _battery = Math.Clamp(_battery, 0, 10000);
      }
      
      if (_brokenLight && nameFront.Intersect([WitheredFreddy, WitheredChica, WitheredBonnie]).Any() || _battery <= 0)
      {
        _assetFrame = 4;
        return;
      }
      
      _assetFrame = nameFront switch
      {
        [] => 2,
        [ToyFreddy] => (byte)new Random().Next(9, 11),
        [ToyChica] => 11,
        [WitheredBonnie] => 12,
        [WitheredFreddy] => 13,
        [Mangle] => 14,
        [WitheredFoxy] => 15,
        [WitheredFoxy, Mangle] or [Mangle, WitheredFoxy] => 16,
        [WitheredFoxy, WitheredBonnie] or [WitheredBonnie, WitheredFoxy] => 17,
        [GoldenFreddy] => 18,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameLeftLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight)
    {
      List<string> name = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Left)?.Select(a => a.Name).ToList() ?? [];
      _assetFrame = name switch
      {
        [] => 1,
        [ToyChica] => 6,
        [BalloonBoy] => 5,
        _ => throw new Exception("No asset this type of list")
      };
    }
    else if (Registration.Objects.GameRightLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight)
    {
      List<string> name = registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Right)?.Select(a => a.Name).ToList() ?? [];
      _assetFrame = name switch
      {
        [] => 3,
        [ToyBonnie] => 7,
        [Mangle] => 8,
        _ => throw new Exception("No asset this type of list")
      };
    }
    else _assetFrame = 0;
  }

  private void UpdateOffice(Registry registry)
  {
    OfficeScroll();
    OfficeAssetReaction(registry);
    Registration.Objects.GameOfficeCamera!.SetFrame(_assetFrame);
    LightButtonsReaction();
    
    _brokenLight = _blackout || !registry.GetSceneManager().GetCurrentScene().IsLayerHidden(5);
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
      registry.GetFNaF().GetAnimatronicManager().GetDirectionalAnimatronic(OfficeDirection.Inside)?.FirstOrDefault(a => a.Name is not Mangle and not BalloonBoy)!.Move(registry);
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

  private void UiButtonsReaction(Registry registry)
  {
    if (Registration.Objects.GameUiMaskButton!.GetHitbox().GetMouseHoverFrame())
      _currentTool = _currentTool switch
      {
        Tool.Nothing => Tool.Mask,
        Tool.Mask => Tool.Nothing,
        _ => _currentTool
      };
    
    if (Registration.Objects.GameUiCameraButton!.GetHitbox().GetMouseHoverFrame())
      _currentTool = _currentTool switch
      {
        Tool.Nothing => Tool.Camera,
        Tool.Camera => Tool.Nothing,
        _ => _currentTool
      };

    switch (_currentTool)
    {
      case Tool.Nothing:
        registry.GetSceneManager().GetCurrentScene().ShowLayer(20, 21);
        break;
      case Tool.Mask:
        registry.GetSceneManager().GetCurrentScene().ShowLayer(20);
        registry.GetSceneManager().GetCurrentScene().HideLayer(21);
        break;
      case Tool.Camera:
        registry.GetSceneManager().GetCurrentScene().HideLayer(20);
        registry.GetSceneManager().GetCurrentScene().ShowLayer(21);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public override void Activation(Registry registry)
  {
    _blackoutFlickeringTimer.Activation(registry);
    _blackoutDurationTimer.Activation(registry);
    _blackoutCustomAlpha = 0;
    _battery = 10000;
    _currentTool = Tool.Nothing;
    foreach (HitboxImage button in (List<HitboxImage>)[Registration.Objects.GameUiMaskButton!, Registration.Objects.GameUiCameraButton!])
    {
      button.GetHitbox().SetSize(button.GetHitbox().GetSize() + new Vector2(12, 20));
      button.GetHitbox().AddPosition(new Vector2(-6, 0));
    }
    
    Registration.Objects.GameOfficeCamera!.SetPosition(new Vector2(-(ScrollBorder / 2), 0));
    Registration.Objects.GameOfficeCamera.SetPack(0);
    
    registry.GetSceneManager().GetCurrentScene().HideLayer(1, 2, 3, 4, 5);
    
    registry.GetFNaF().GetAnimatronicManager().Activation(registry);
    registry.GetFNaF().GetAnimatronicManager().Update(registry);
    _brokenLight = false;
    _blackout = false;
  }

  private void UiMaskAndCameraReaction()
  {
    if (Registration.Objects.GameUiMaskButton!.GetHitbox().GetMouseHoverFrame()) Registration.Objects.GameUiMask!.GetScript()!.TriggerPullAction();
    if (Registration.Objects.GameUiCameraButton!.GetHitbox().GetMouseHoverFrame()) Registration.Objects.GameUiCamera!.GetScript()!.TriggerPullAction();
  }

  private void UiBatteryUpdate() => Registration.Objects.GameUiBattery!.SetFrame((int)Math.Ceiling(_battery / 2500.0));

  public override void Update(Registry registry)
  {
    Console.WriteLine(_battery);
    UiButtonsReaction(registry);
    UiMaskAndCameraReaction();
    UiBatteryUpdate();
    
    if (Registration.Objects.GameOfficeCamera!.GetPackIndex() == 0)
    {
      registry.GetSceneManager().GetCurrentScene().ShowLayer(6);
      UpdateScroller();
      UpdateOffice(registry);
      UpdateBlackout(registry);
    }
    else
    {
      registry.GetSceneManager().GetCurrentScene().HideLayer(6);
    }

    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.Y)) Registration.Objects.GameOfficeCamera!.PreviousPack();
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.U)) Registration.Objects.GameOfficeCamera!.NextPack();
  }
}