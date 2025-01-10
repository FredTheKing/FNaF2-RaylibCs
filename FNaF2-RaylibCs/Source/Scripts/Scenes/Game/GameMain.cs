using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Box;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using FNaF2_RaylibCs.Source.Scripts.Objects;
using Raylib_cs;
using static FNaF2_RaylibCs.Source.Config.AnimatronicsNames;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Game;

internal enum Tool { Nothing, Mask, Camera }

public class GameMain : ScriptTemplate
{
  private const float BatteryUsageSpeed = 135f; // 135f is default. Bigger number = slower drain
  private const int ScrollBorder = 576;
  private const float DeadZone = 170f;
  private const float Sensitivity = 0.3f; // lower sensitivity = bigger number

  private SimpleTimer _reddoTimer = new(.777f, true);
  private SimpleTimer _blackoutFlickeringTimer = new(.1);
  private SimpleTimer _blackoutDurationTimer = new(5);
  private float _blackoutCustomAlpha;
  
  private bool _reddo = false;
  private float _battery = 10000;
  private float _scrollerPositionX;
  private Tool _currentTool = Tool.Nothing;
  private int _assetFrame;
  private int _cameraPack = 1;
  private bool _brokenLight;
  private bool _blackout;
  
  private void UpdateOfficeScroller()
  {
    _scrollerPositionX = Raylib.GetMouseX() - Registration.Objects.GameCentralOfficeScroller!.GetPosition().X - 2;
    if (_scrollerPositionX is > -DeadZone and < DeadZone || Raylib.GetMouseX() > Config.WindowWidth) _scrollerPositionX = 0;
    else if (_scrollerPositionX > DeadZone) _scrollerPositionX -= DeadZone;
    else if (_scrollerPositionX < -DeadZone) _scrollerPositionX += DeadZone;
    _scrollerPositionX /= Sensitivity;
    
    OfficeScroll();
  }

  private void OfficeScroll()
  {
    var anchorPoint = Registration.Objects.GameOfficeScroller!;
    
    anchorPoint.AddPosition(new Vector2(-_scrollerPositionX * Raylib.GetFrameTime(), 0));
    anchorPoint.SetPosition(new Vector2(Math.Clamp(anchorPoint.GetPosition().X, -ScrollBorder, 0), 0));
    Registration.Objects.GameOfficeCamera!.SetPosition(anchorPoint.GetPosition());
    Registration.Objects.GameLeftLightSwitch!.SetPosition(anchorPoint.GetPosition() + new Vector2(100, 356));
    Registration.Objects.GameRightLightSwitch!.SetPosition(anchorPoint.GetPosition() + new Vector2(1408, 356));
    Registration.Objects.GameOfficeTable!.SetPosition(anchorPoint.GetPosition() + new Vector2(575, 333));
    Registration.Objects.GameOfficeBalloonBoy!.SetPosition(anchorPoint.GetPosition() + new Vector2(262, 270));
    Registration.Objects.GameOfficeMangle!.SetPosition(anchorPoint.GetPosition() + new Vector2(700, 0));
    Registration.Objects.GameOfficeToyFreddy!.SetPosition(anchorPoint.GetPosition() + new Vector2(820, 0));
  }

  private void LightButtonsReaction()
  {
    Registration.Objects.GameLeftLightSwitch!.SetFrame(_assetFrame is 1 or 6 or 5 ? 1 : 0);
    Registration.Objects.GameRightLightSwitch!.SetFrame(_assetFrame is 3 or 7 or 8 ? 1 : 0);
  }

  private void OfficeAssetReaction(Registry registry)
  {
    List<string> nameInside = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.OfficeInside).Select(a => a.Name).ToList();
    Animatronic? actualInside = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.OfficeInside).FirstOrDefault(a => a.Name is not Mangle and not BalloonBoy);
    
    var separatedAssetsAnimatronics = new Dictionary<string, int>
    {
      { ToyFreddy, 1 },
      { ToyBonnie, 3 },
      { ToyChica, 4 },
      { Mangle, 5 },
      { BalloonBoy, 6 }
    };

    foreach (KeyValuePair<string,int> pair in separatedAssetsAnimatronics)
    {
      if (nameInside.Contains(pair.Key)) registry.scene.Current!.ShowLayer(pair.Value);
      else registry.scene.Current!.HideLayer(pair.Value);
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
    
    if (registry.keybinds.IsKeyDown(KeyboardKey.LeftControl) && Registration.Objects.GameUiCamera!.GetPackIndex() is 0 or 3)
    {
      List<string> nameFront = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.OfficeFront).Select(a => a.Name).ToList();

      if (_battery != 0)
      {
        _battery -= 10000 / BatteryUsageSpeed * Raylib.GetFrameTime();
        _battery = Math.Clamp(_battery, 0, 10000);
      }
      
      if (_brokenLight || _battery <= 0)
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
    else if (Registration.Objects.GameLeftLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight && Registration.Objects.GameUiCamera!.GetPackIndex() is 0 or 3)
    {
      List<string> name = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.OfficeLeft).Select(a => a.Name).ToList();
      _assetFrame = name switch
      {
        [] => 1,
        [ToyChica] => 6,
        [BalloonBoy] => 5,
        _ => throw new Exception("No asset this type of list")
      };
    }
    else if (Registration.Objects.GameRightLightSwitch!.GetHitbox().GetMouseDrag(MouseButton.Left) && !_brokenLight && Registration.Objects.GameUiCamera!.GetPackIndex() is 0 or 3)
    {
      List<string> name = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.OfficeRight).Select(a => a.Name).ToList();
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
    OfficeAssetReaction(registry);
    LightButtonsReaction();
    
    _brokenLight = _blackout || !registry.scene.Current!.IsLayerHidden(5) || _currentTool == Tool.Mask;
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
      registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.OfficeInside).FirstOrDefault(a => a.Name is not Mangle and not BalloonBoy)!.Move(registry);
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
    
    if (Registration.Objects.GameUiCameraButton!.GetHitbox().GetMouseHoverFrame() && !_blackout)
      _currentTool = _currentTool switch
      {
        Tool.Nothing => Tool.Camera,
        Tool.Camera => Tool.Nothing,
        _ => _currentTool
      };

    switch (_currentTool)
    {
      case Tool.Nothing:
        registry.scene.Current!.ShowLayer(20, 21);
        break;
      case Tool.Mask:
        registry.scene.Current!.ShowLayer(20);
        registry.scene.Current!.HideLayer(21);
        break;
      case Tool.Camera:
        registry.scene.Current!.HideLayer(20);
        registry.scene.Current!.ShowLayer(21);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void UiMaskAndCameraReaction(Registry registry)
  {
    if (Registration.Objects.GameUiMaskButton!.GetHitbox().GetMouseHoverFrame()) Registration.Objects.GameUiMask!.GetScript()!.TriggerPullAction();
    if (Registration.Objects.GameUiCameraButton!.GetHitbox().GetMouseHoverFrame() && !_blackout) Registration.Objects.GameUiCamera!.GetScript()!.TriggerPullAction();
    
    Animatronic? anima = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.OfficeInside).FirstOrDefault(a => a.Name is not Mangle and not BalloonBoy);
    if (anima is not null && anima.CameraHatering == 0)
    {
      anima.CameraHatering = -1;
      Registration.Objects.GameUiCamera!.GetScript()!.TriggerPullAction();
      _currentTool = Tool.Nothing;
    }
  }

  private void UpdateMusicBox(Registry registry)
  {
    if (Registration.Objects.GameOfficeCamera!.GetPackIndex() == 11)
    {
      Registration.Objects.GameMusicBoxCircular!.SetColor(Color.White);
      registry.scene.Current!.ShowLayer(9);
    }
    else
    {
      Registration.Objects.GameMusicBoxCircular!.SetColor(Color.Blank);
      registry.scene.Current!.HideLayer(9);
    }
    
    if (Registration.Objects.GameOfficeCamera.GetPackIndex() != 11) Registration.Objects.GameMusicBoxBox!.GetHitbox().Update(registry);
    bool holding = Registration.Objects.GameMusicBoxBox!.GetHitbox().GetMouseDrag(MouseButton.Left) && Registration.Objects.GameOfficeCamera.GetPackIndex() == 11;
    Registration.Objects.GameMusicBoxBox.SetFrame(holding ? 1 : 0);
    Registration.Objects.GameMusicBoxCircular.Recovering = holding;
  }

  private void UpdateCamera(Registry registry)
  {
    _cameraPack = Registration.Objects.GameUiMapWithCams!.GetSelectedCam()+1;
    bool lightning = registry.keybinds.IsKeyDown(KeyboardKey.LeftControl);
    if (Registration.Objects.GameOfficeCamera!.GetPackIndex() == 1)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam01).Select(a => a.Name).ToList() switch
      {
        [] or [ToyChica] or [WitheredBonnie] when !lightning => 0,
        [] when lightning => 1,
        [ToyChica] when lightning => 2,
        [WitheredBonnie] when lightning => 3,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 2)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam02).Select(a => a.Name).ToList() switch
      {
        [] or [ToyBonnie] when !lightning => 0,
        [WitheredChica] when !lightning => 1,
        [] when lightning => 2,
        [WitheredChica] when lightning => 3,
        [ToyBonnie] when lightning => 4,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 3)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam03).Select(a => a.Name).ToList() switch
      {
        [] or [ToyBonnie] when !lightning => 0,
        [WitheredFreddy] when !lightning => 1,
        [] when lightning => 2,
        [WitheredFreddy] when lightning => 3,
        [ToyBonnie] when lightning => 4,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 4)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam04).Select(a => a.Name).ToList() switch
      {
        [] or [ToyChica] or [WitheredChica] when !lightning => 0,
        [ToyBonnie] when !lightning => 1,
        [] when lightning => 2,
        [ToyBonnie] when lightning => 3,
        [ToyChica] when lightning => 4,
        [WitheredChica] when lightning => 5,
        //[PAPEREMAAAAN] when lightning => 6,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 5)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam05).Select(a => a.Name).ToList() switch
      {
        [] or [ToyChica] or [WitheredBonnie] or [BalloonBoy] or [Endo] when !lightning => 0,
        [] when lightning => 1,
        [ToyChica] when lightning => 2,
        [WitheredBonnie] when lightning => 3,
        [BalloonBoy] when lightning => 4,
        [Endo] when lightning => 5,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 6)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam06).Select(a => a.Name).ToList() switch
      {
        [] or [ToyBonnie] or [WitheredChica] or [Mangle] when !lightning => 0,
        [] when lightning => 1,
        [ToyBonnie] when lightning => 2,
        [WitheredChica] when lightning => 3,
        [Mangle] when lightning => 4,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 7)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam07).Select(a => a.Name).ToList() switch
      {
        [] or [WitheredBonnie] or [WitheredFreddy] when !lightning => 0,
        [ToyChica] when !lightning => 1,
        [] when lightning => 2,
        [ToyChica] when lightning => 3,
        [WitheredBonnie] when lightning => 4,
        [WitheredFreddy] when lightning => 5,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 8)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam08).Select(a => a.Name).ToList() switch
      {
        [] or [WitheredFoxy] or [WitheredFreddy, WitheredFoxy] or [WitheredFoxy, WitheredFreddy] or [WitheredFreddy, WitheredChica, WitheredFoxy] or [WitheredChica, WitheredFreddy, WitheredFoxy] or [WitheredFreddy, WitheredFoxy, WitheredChica] or [WitheredChica, WitheredFoxy, WitheredFreddy] or [WitheredFoxy, WitheredChica, WitheredFreddy] or [WitheredFreddy, WitheredBonnie, WitheredChica, WitheredFoxy] or [WitheredBonnie, WitheredFreddy, WitheredChica, WitheredFoxy] or [WitheredFreddy, WitheredBonnie, WitheredFoxy, WitheredChica] or [WitheredBonnie, WitheredChica, WitheredFreddy, WitheredFoxy] or [WitheredChica, WitheredBonnie, WitheredFreddy, WitheredFoxy] or [WitheredBonnie, WitheredFoxy, WitheredChica, WitheredFreddy] or [WitheredFoxy, WitheredBonnie, WitheredChica, WitheredFreddy] or [WitheredChica, WitheredFoxy, WitheredBonnie, WitheredFreddy] or [WitheredFoxy, WitheredChica, WitheredBonnie, WitheredFreddy] when !lightning => 0,
        [WitheredFreddy, WitheredBonnie, WitheredChica, WitheredFoxy] or [WitheredBonnie, WitheredFreddy, WitheredChica, WitheredFoxy] or [WitheredFreddy, WitheredBonnie, WitheredFoxy, WitheredChica] or [WitheredBonnie, WitheredChica, WitheredFreddy, WitheredFoxy] or [WitheredChica, WitheredBonnie, WitheredFreddy, WitheredFoxy] or [WitheredBonnie, WitheredFoxy, WitheredChica, WitheredFreddy] or [WitheredFoxy, WitheredBonnie, WitheredChica, WitheredFreddy] or [WitheredChica, WitheredFoxy, WitheredBonnie, WitheredFreddy] or [WitheredFoxy, WitheredChica, WitheredBonnie, WitheredFreddy] when lightning => 1,
        [WitheredFreddy, WitheredChica, WitheredFoxy] or [WitheredChica, WitheredFreddy, WitheredFoxy] or [WitheredFreddy, WitheredFoxy, WitheredChica] or [WitheredChica, WitheredFoxy, WitheredFreddy] or [WitheredFoxy, WitheredChica, WitheredFreddy] when lightning => 2,
        [WitheredFreddy, WitheredFoxy] or [WitheredFoxy, WitheredFreddy] when lightning => 3,
        [] when lightning => 4,
        [WitheredFoxy] when lightning => 5,
        //[SHADDDOOOOWWWWSHIIT?!!?!?!] when lightning => 6,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 9)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam09).Select(a => a.Name).ToList() switch
      {
        [ToyFreddy, ToyBonnie, ToyChica] or [ToyFreddy, ToyChica, ToyBonnie] or [ToyBonnie, ToyFreddy, ToyChica] or [ToyBonnie, ToyChica, ToyFreddy] or [ToyChica, ToyFreddy, ToyBonnie] or [ToyChica, ToyBonnie, ToyFreddy] when !lightning => 0,
        [ToyFreddy, ToyChica] or [ToyChica, ToyFreddy] when !lightning => 1,
        [ToyFreddy] when !lightning => 2,
        [] when !lightning => 3,
        [] when lightning => 3,
        [ToyFreddy, ToyBonnie, ToyChica] or [ToyFreddy, ToyChica, ToyBonnie] or [ToyBonnie, ToyFreddy, ToyChica] or [ToyBonnie, ToyChica, ToyFreddy] or [ToyChica, ToyFreddy, ToyBonnie] or [ToyChica, ToyBonnie, ToyFreddy] when lightning => 4,
        [ToyFreddy, ToyChica] or [ToyChica, ToyFreddy] when lightning => 5,
        [ToyFreddy] when lightning => 6,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 10)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam10).Select(a => a.Name).ToList() switch
      {
        [BalloonBoy] or [BalloonBoy, ToyFreddy] or [ToyFreddy, BalloonBoy] when !lightning => 0,
        [] or [ToyFreddy] when !lightning => 1,
        [BalloonBoy] when lightning => 2,
        [BalloonBoy, ToyFreddy] or [ToyFreddy, BalloonBoy] when lightning => 3,
        [ToyFreddy] when lightning => 4,
        [] when lightning => 5,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 11)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam11).Select(a => a.Name).ToList() switch
      {
        [] or [Marionette] or [Endo] when !lightning => 0,
        [Marionette] when lightning => 1,
        [] when lightning => 4,
        [Endo] when lightning => 5,
        _ => throw new Exception("No asset for this type of list")
      };
    }
    else if (Registration.Objects.GameOfficeCamera.GetPackIndex() == 12)
    {
      _assetFrame = registry.fnaf.GetAnimatronicManager().GetAnimatronics().Where(a => a.CurrentLocation == Location.Cam12).Select(a => a.Name).ToList() switch
      {
        [] or [Mangle] when !lightning => 0,
        [Mangle] when lightning => 1,
        [] when lightning => 2,
        _ => throw new Exception("No asset for this type of list")
      };
    }
  }

  private void UiBatteryUpdate() => Registration.Objects.GameUiBattery!.SetFrame((int)Math.Ceiling(_battery / 2500.0));

  private void ApplyingPack() => Registration.Objects.GameOfficeCamera!.SetPack(Registration.Objects.GameUiCamera!.GetScript()!.State == States.Up ? _cameraPack : 0);

  private void ReddoDotto(Registry registry)
  {
    _reddoTimer.Update(registry);
    if (_reddoTimer.TargetTrigger()) _reddo = !_reddo;
    Registration.Objects.GameCameraRecord!.SetTint(_reddo ? Color.White : Color.Blank);
  }

  private void UpdateCameraScrolling()
  {
    switch (_cameraPack)
    {
      case >= 1 and <= 6:
        Registration.Objects.GameOfficeCamera!.SetPosition(Vector2.Zero);
        return;
      case > 6:
        Registration.Objects.GameOfficeCamera!.SetPosition(Registration.Objects.GameCameraScroller!.GetPosition());
        return;
    }
  }

  public override void Activation(Registry registry)
  {
    _reddoTimer.Activation(registry);
    _blackoutFlickeringTimer.Activation(registry);
    _blackoutDurationTimer.Activation(registry);
    _blackoutCustomAlpha = 0;
    _battery = 10000;
    _currentTool = Tool.Nothing;
    Registration.Objects.GameCameraScroller!.GetScript()!.SetBorder(-ScrollBorder, 0);
    foreach (HitboxImage button in (List<HitboxImage>)[Registration.Objects.GameUiMaskButton!, Registration.Objects.GameUiCameraButton!])
    {
      button.GetHitbox().SetSize(button.GetHitbox().GetSize() + new Vector2(12, 20));
      button.GetHitbox().AddPosition(new Vector2(-6, 0));
    }
    
    Registration.Objects.GameOfficeCamera!.SetPosition(new Vector2(-(ScrollBorder / 2), 0));
    Registration.Objects.GameOfficeCamera.SetPack(0);
    
    registry.scene.Current!.HideLayer(1, 3, 4, 5, 6);
    
    registry.fnaf.GetAnimatronicManager().Activation(registry);
    registry.fnaf.GetAnimatronicManager().Update(registry);
    _brokenLight = false;
    _blackout = false;
  }

  public override void Update(Registry registry)
  {
    UiBatteryUpdate();
    UiButtonsReaction(registry);
    UiMaskAndCameraReaction(registry);

    ApplyingPack();
    UpdateMusicBox(registry);
    
    if (Registration.Objects.GameOfficeCamera!.GetPackIndex() == 0)
    {
      registry.scene.Current!.ShowLayer(7, 2);
      registry.scene.Current!.HideLayer(8);
      if (Registration.Objects.GameUiCamera!.GetPackIndex() is 0 or 3) UpdateOfficeScroller();
      UpdateOffice(registry);
      UpdateBlackout(registry);
    }
    else
    {
      registry.scene.Current!.HideLayer(7, 2);
      registry.scene.Current!.ShowLayer(8);
      _blackoutCustomAlpha = 0;
      ReddoDotto(registry);
      UpdateCamera(registry);
      UpdateCameraScrolling();
      Registration.Objects.GameUiMapCamsTexts!.SetTextIndex(Registration.Objects.GameUiMapWithCams!.GetSelectedCam());
    }
    
    Registration.Objects.GameOfficeCamera.SetFrame(_assetFrame);
  }
}