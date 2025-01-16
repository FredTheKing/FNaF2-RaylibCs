using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Cams;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;
using FNaF2_RaylibCs.Source.Packages.Objects.Box;
using FNaF2_RaylibCs.Source.Packages.Objects.Checkbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Circular;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.Slider;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.Scripts.Objects;
using FNaF2_RaylibCs.Source.Scripts.Scenes;
using FNaF2_RaylibCs.Source.Scripts.Scenes.Debugger;
using FNaF2_RaylibCs.Source.Scripts.Scenes.Game;
using FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public static class Registration
{
  public struct Materials
  {
    public static SoundResource? MenuMusicResource;
    public static SoundResource? SetSoundResource;
    
    public static SoundResource? GamePhoneGuy1Resource;
    public static SoundResource? GamePhoneGuy2Resource;
    public static SoundResource? GamePhoneGuy3Resource;
    public static SoundResource? GamePhoneGuy4Resource;
    public static SoundResource? GamePhoneGuy5Resource;
    public static SoundResource? GamePhoneGuy6Resource;
    
    public static FontResource? GlobalFont;

    public static ImageStackResource? MenuBackgroundStackResource;
    public static ImageStackResource? StaticStackResource;
    public static ImageStackResource? MenuWhiteBlinkoStackResource;
    
    public static ImageResource? CreditsRaylibResource;
    public static ImageResource? CreditsMyResource;
    public static ImageResource? CreditsScottResource;
    
    public static ImageResource? CustomNightWitheredFreddyResource;
    public static ImageResource? CustomNightWitheredBonnieResource;
    public static ImageResource? CustomNightWitheredChicaResource;
    public static ImageResource? CustomNightWitheredFoxyResource;
    public static ImageResource? CustomNightBalloonBoyResource;
    public static ImageResource? CustomNightToyFreddyResource;
    public static ImageResource? CustomNightToyBonnieResource;
    public static ImageResource? CustomNightToyChicaResource;
    public static ImageResource? CustomNightMangleResource;
    public static ImageResource? CustomNightGoldenFreddyResource;

    public static ImageResource? LoadingClockResource;

    public static FontResource? PixilatedFont;
    public static ShaderResource? GamePerspectiveShaderResource;
    public static ImageResource? GameNewspaperYooo;
    public static ImageResource? GameCameraOutlineResource;
    public static ImageResource? GameCameraRecordResource;
    public static ImageResource? GameCameraMapResource;
    public static ImageStackResource? GameCameraCamsUnitResource;
    public static ImageStackResource? GameMusicBoxWindUpButtonResource;
    public static ImageDoubleStackResource? GameOfficeCameraResource;
    public static ImageStackResource? GameOfficeTableResource;
    public static ImageStackResource? GameLeftLightResource;
    public static ImageStackResource? GameRightLightResource;
    public static ImageResource? GameOfficeToyFreddyResource;
    public static ImageResource? GameOfficeToyBonnieResource;
    public static ImageResource? GameOfficeToyChicaResource;
    public static ImageResource? GameOfficeMangleResource;
    public static ImageResource? GameOfficeBalloonBoyResource;
    public static ImageStackResource? GameUiBatteryResource;
    public static ImageDoubleStackResource? GameUiMaskResource;
    public static ImageDoubleStackResource? GameUiCameraResource;
    public static ImageDoubleStackResource? GameJumpscaresResource;
    public static ImageResource? GameUiMuteResource;
    public static ImageResource? GameUiMaskButtonResource;
    public static ImageResource? GameUiCameraButtonResource;
  }

  public struct Sounds
  {
    public static SoundObject? MenuMusic;
    public static SoundObject? SetSound;

    public static SoundObject? GamePhoneGuy1;
    public static SoundObject? GamePhoneGuy2;
    public static SoundObject? GamePhoneGuy3;
    public static SoundObject? GamePhoneGuy4;
    public static SoundObject? GamePhoneGuy5;
    public static SoundObject? GamePhoneGuy6;
  }

  public struct Objects
  {
    public static SelectableImage? WhiteBlinko;
    
    public static SelectableImage? MenuBackground;
    public static SimpleAnimation? Static;
    public static SimpleText? MenuGameName;
    public static SimpleText? MenuSet;
    public static HitboxText? MenuNewGame;
    public static HitboxText? MenuContinue;
    public static SimpleText? MenuContinueNightText;
    public static HitboxText? MenuExtras;
    
    public static SimpleText? ExtrasTitle;
    public static HitboxText? ExtrasBackToPage;
    public static HitboxText? ExtrasAuthorLinkGithub;
    public static HitboxText? ExtrasProjectLinkGithub;
    public static HitboxText? ExtrasCustomNight;
    public static HitboxText? ExtrasSettings;
    public static HitboxText? ExtrasCredits;
    public static HitboxText? ExtrasBack;
    
    public static SimpleText? SettingsTitle;
    public static SimpleText? SettingsFullscreenText;
    public static SimpleCheckbox? SettingsFullscreenCheckbox;
    public static SimpleText? SettingsVsyncText;
    public static SimpleText? SettingsFpsShowText;
    public static SimpleCheckbox? SettingsVsyncCheckbox;
    public static SimpleText? SettingsVolumeText;
    public static SimpleSlider? SettingsVolumeSlider;
    public static SimpleText? SettingsFunModeText;
    public static SimpleCheckbox? SettingsFunModeCheckbox;
    public static SimpleText? SettingsDebugModeText;
    public static SimpleCheckbox? SettingsDebugModeCheckbox;
    
    public static SimpleText? CreditsTitle;
    public static HitboxImage? CreditsRaylibLogo;
    public static SimpleText? CreditsRaylibText;
    public static HitboxImage? CreditsMyLogo;
    public static SimpleText? CreditsMyText;
    public static HitboxImage? CreditsScottLogo;
    public static SimpleText? CreditsScottText;
    
    public static SimpleText? CustomNightTitle;
    public static SelectableHitboxImage? CustomNightStartBox;
    public static SelectableText? CustomNightStartText;
    public static SelectableHitboxImage? CustomNightPresetBox;
    public static SimpleText? CustomNightPresetText;
    public static HitboxTextBorderImage? CustomNightWitheredFreddy;
    public static HitboxTextBorderImage? CustomNightWitheredBonnie;
    public static HitboxTextBorderImage? CustomNightWitheredChica;
    public static HitboxTextBorderImage? CustomNightWitheredFoxy;
    public static HitboxTextBorderImage? CustomNightBalloonBoy;
    public static HitboxTextBorderImage? CustomNightToyFreddy;
    public static HitboxTextBorderImage? CustomNightToyBonnie;
    public static HitboxTextBorderImage? CustomNightToyChica;
    public static HitboxTextBorderImage? CustomNightMangle;
    public static HitboxTextBorderImage? CustomNightGoldenFreddy;

    public static SimpleText? LoadingNightText;
    public static SimpleText? LoadingAmText;
    public static SimpleImage? LoadingClockThingo;

    public static SimpleImage? GameNewspapers;
    public static SelectablePackedImage? GameOfficeCamera;
    public static SimpleBox? GameBlackoutRectangle;
    public static SimpleAnimation? GameOfficeTable;
    public static SimpleImage? GameCameraOutline;
    public static SimpleImage? GameCameraRecord;
    public static DebugBox? GameCentralOfficeScroller;
    public static DebugBox? GameOfficeScroller;
    public static DebugBox? GameCameraScroller;
    public static SelectableHitboxImage? GameLeftLightSwitch;
    public static SelectableHitboxImage? GameRightLightSwitch;
    public static SimpleImage? GameOfficeToyFreddy;
    public static SimpleImage? GameOfficeToyBonnie;
    public static SimpleImage? GameOfficeToyChica;
    public static SimpleImage? GameOfficeMangle;
    public static SimpleImage? GameOfficeBalloonBoy;
    public static CamMap? GameUiMapWithCams;
    public static SelectableText? GameUiMapCamsTexts;
    public static SelectableImage? GameUiBattery;
    public static SimpleText? GameUiNight;
    public static SimpleText? GameUiHour;
    public static HitboxImage? GameUiMaskButton;
    public static HitboxImage? GameUiCameraButton;
    public static SelectableAnimation? GameUiMask;
    public static SelectableAnimation? GameUiCamera;
    public static HitboxImage? GameUiMute;
    public static SelectableAnimation? GameJumpscares;
    public static SimpleCircular? GameMusicBoxCircular;
    public static SelectableHitboxImage? GameMusicBoxBox;
    public static SimpleText? GameMusicBoxText;
    public static SimpleText? GameMusicBoxBottomText;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.MenuMusicResource = registry.RegisterMaterial("MenuMusicResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameNewspaper], new SoundResource(Config.ResPath + "Sound/MenuMusic.wav"));
    Materials.SetSoundResource = registry.RegisterMaterial("SetSoundResource", [Config.Scenes.All], new SoundResource(Config.ResPath + "Sound/Set.wav"));
    
    Materials.GamePhoneGuy1Resource = registry.RegisterMaterial("GamePhoneGuy1Resource", [Config.Scenes.GameMain], new SoundResource(Config.ResPath + "Sound/PhoneGuy1.wav"));
    Materials.GamePhoneGuy2Resource = registry.RegisterMaterial("GamePhoneGuy2Resource", [Config.Scenes.GameMain], new SoundResource(Config.ResPath + "Sound/PhoneGuy2.wav"));
    Materials.GamePhoneGuy3Resource = registry.RegisterMaterial("GamePhoneGuy3Resource", [Config.Scenes.GameMain], new SoundResource(Config.ResPath + "Sound/PhoneGuy3.wav"));
    Materials.GamePhoneGuy4Resource = registry.RegisterMaterial("GamePhoneGuy4Resource", [Config.Scenes.GameMain], new SoundResource(Config.ResPath + "Sound/PhoneGuy4.wav"));
    Materials.GamePhoneGuy5Resource = registry.RegisterMaterial("GamePhoneGuy5Resource", [Config.Scenes.GameMain], new SoundResource(Config.ResPath + "Sound/PhoneGuy5.wav"));
    Materials.GamePhoneGuy6Resource = registry.RegisterMaterial("GamePhoneGuy6Resource", [Config.Scenes.GameMain], new SoundResource(Config.ResPath + "Sound/PhoneGuy6.wav"));
    
    Materials.GlobalFont = registry.RegisterMaterial("GlobalFont", [Config.Scenes.All], new FontResource(Config.ResPath + "Font/regular.ttf", 128));
    
    Materials.MenuBackgroundStackResource = registry.RegisterMaterial("MenuBackgroundStackResource", [Config.Scenes.MenuMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/Background", 4)));
    Materials.StaticStackResource = registry.RegisterMaterial("MenuStaticStackResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameMain, Config.Scenes.GameLose], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/Static", 8)));
    
    Materials.CreditsRaylibResource = registry.RegisterMaterial("CreditsRaylibResource", [Config.Scenes.MenuCredits], new ImageResource(Config.ResPath + "Credits/Raylib.png"));
    Materials.CreditsMyResource = registry.RegisterMaterial("CreditsMyResource", [Config.Scenes.MenuCredits], new ImageResource(Config.ResPath + "Credits/Me.png"));
    Materials.CreditsScottResource = registry.RegisterMaterial("CreditsScottResource", [Config.Scenes.MenuCredits], new ImageResource(Config.ResPath + "Credits/Scott.png"));
    
    Materials.CustomNightWitheredFreddyResource = registry.RegisterMaterial("CustomNightWitheredFreddyResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/WithFreddy.png"));
    Materials.CustomNightWitheredBonnieResource = registry.RegisterMaterial("CustomNightWitheredBonnieResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/WithBonnie.png"));
    Materials.CustomNightWitheredChicaResource = registry.RegisterMaterial("CustomNightWitheredChicaResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/WithChica.png"));
    Materials.CustomNightWitheredFoxyResource = registry.RegisterMaterial("CustomNightWitheredFoxyResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/WithFoxy.png"));
    Materials.CustomNightBalloonBoyResource = registry.RegisterMaterial("CustomNightBalloonBoyResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/BalloonBoy.png"));
    Materials.CustomNightToyFreddyResource = registry.RegisterMaterial("CustomNightToyFreddyResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/ToyFreddy.png"));
    Materials.CustomNightToyBonnieResource = registry.RegisterMaterial("CustomNightToyBonnieResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/ToyBonnie.png"));
    Materials.CustomNightToyChicaResource = registry.RegisterMaterial("CustomNightToyChicaResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/ToyChica.png"));
    Materials.CustomNightMangleResource = registry.RegisterMaterial("CustomNightMangleResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/Mangle.png"));
    Materials.CustomNightGoldenFreddyResource = registry.RegisterMaterial("CustomNightGoldenFreddyResource", [Config.Scenes.MenuCustomNight], new ImageResource(Config.ResPath + "Menu/Avatars/GoldenFreddy.png"));
    
    Materials.LoadingClockResource = registry.RegisterMaterial("LoadingClockResource", [Config.Scenes.GameLoading], new ImageResource(Config.ResPath + "Game/Etc/Clock.png"));
    
    Materials.PixilatedFont = registry.RegisterMaterial("PixilatedFont", [Config.Scenes.GameMain], new FontResource(Config.ResPath + "Font/lcd.ttf", 48));
    Materials.GameNewspaperYooo = registry.RegisterMaterial("GameNewspaperYooo", [Config.Scenes.GameNewspaper], new ImageResource(Config.ResPath + "Game/Etc/Newspaper.png"));
    Materials.GameNewspaperYooo.SetFilter(TextureFilter.Bilinear);
    Materials.GamePerspectiveShaderResource = registry.RegisterMaterial("GamePerspectiveShaderResource", [Config.Scenes.GameMain], new ShaderResource(Config.ResPath + "Shaders/perspective.vs", Config.ResPath + "Shaders/perspective.fs"));
    Materials.GameOfficeCameraResource = registry.RegisterMaterial("GameOfficeCameraResource", [Config.Scenes.GameMain], new ImageDoubleStackResource([
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office", 22),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 4),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 5, 4),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 5, 9),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 7, 14),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 6, 21),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 5, 27),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 6, 32),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 7, 38),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 7, 45),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 6, 52),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 6, 58),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Camera", 3, 64)
    ]));
    Materials.GameCameraOutlineResource = registry.RegisterMaterial("GameCameraOutlineResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/UI/Outline.png"));
    Materials.GameCameraRecordResource = registry.RegisterMaterial("GameCameraRecordResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/UI/Record.png"));
    Materials.GameCameraMapResource = registry.RegisterMaterial("GameCameraMapResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/UI/Map.png"));
    Materials.GameCameraCamsUnitResource = registry.RegisterMaterial("GameCameraCamsUnitResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/UI/CamBoxes", 2)));
    Materials.GameMusicBoxWindUpButtonResource = registry.RegisterMaterial("GameMusicBoxWindUpButtonResource", [Config.Scenes.GameMain, Config.Scenes.MenuCustomNight], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/UI/CamBoxes", 2, 2)));
    Materials.GameOfficeTableResource = registry.RegisterMaterial("GameOfficeTableResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office/Table", 4)));
    Materials.GameLeftLightResource = registry.RegisterMaterial("GameLeftLightResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office/LightButtons", 2)));
    Materials.GameRightLightResource = registry.RegisterMaterial("GameRightLightResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office/LightButtons", 2, 2)));
    Materials.GameOfficeToyFreddyResource = registry.RegisterMaterial("GameOfficeToyFreddyResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/ToyFreddy.png"));
    Materials.GameOfficeToyBonnieResource = registry.RegisterMaterial("GameOfficeToyBonnieResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/ToyBonnie.png"));
    Materials.GameOfficeToyChicaResource = registry.RegisterMaterial("GameOfficeToyChicaResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/ToyChica.png"));
    Materials.GameOfficeMangleResource = registry.RegisterMaterial("GameOfficeMangleResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/Mango.png"));
    Materials.GameOfficeBalloonBoyResource = registry.RegisterMaterial("GameOfficeBalloonBoyResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/BB.png"));
    Materials.GameUiBatteryResource = registry.RegisterMaterial("GameUiBatteryResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/UI/Battery", 5)));
    Materials.GameUiMuteResource = registry.RegisterMaterial("GameUiMuteResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/UI/Mute.png"));
    Materials.GameUiMaskButtonResource = registry.RegisterMaterial("GameUiMaskButtonResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/UI/Mask.png"));
    Materials.GameUiCameraButtonResource = registry.RegisterMaterial("GameUiCameraButtonResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/UI/Camera.png"));
    Materials.GameUiMaskResource = registry.RegisterMaterial("GameUiMaskResource", [Config.Scenes.GameMain], new ImageDoubleStackResource([
      Loaders.LoadSingleFilenameAsList(Config.ResPath + "Game/Main/UI/Nothing.png"),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/UI/Masking", 9),
      Loaders.LoadSingleFilenameAsList(Config.ResPath + "Game/Main/UI/Masking/8.png"),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/UI/Masking", 9, inverted: true)
    ]));
    Materials.GameUiCameraResource = registry.RegisterMaterial("GameUiCameraResource", [Config.Scenes.GameMain], new ImageDoubleStackResource([
      Loaders.LoadSingleFilenameAsList(Config.ResPath + "Game/Main/UI/Nothing.png"),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/UI/Camering", 11),
      Loaders.LoadSingleFilenameAsList(Config.ResPath + "Game/Main/UI/Nothing.png"),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/UI/Camering", 11, inverted: true)
    ]));
    Materials.GameJumpscaresResource = registry.RegisterMaterial("GameJumpscaresResource", [Config.Scenes.GameMain], new ImageDoubleStackResource([
      Loaders.LoadSingleFilenameAsList(Config.ResPath + "Game/Main/Jumpscares/Nothing.png"),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/WitheredFreddy", 13),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/WitheredBonnie", 16),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/WitheredChica", 12),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/WitheredFoxy", 14),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/ToyFreddy", 12),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/ToyBonnie", 13),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/ToyChica", 13),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/Mangle", 16),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/Marionette", 15),
      Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Jumpscares/GoldenFreddy", 13)
    ]));
    
    Materials.MenuWhiteBlinkoStackResource = registry.RegisterMaterial("GlobalWhiteBlinkoStackResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameLoading, Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/WhiteBlinko", 6)));
  }
  
  public static void SoundsInitialisation(Registry registry)
  {
    Sounds.MenuMusic = registry.RegisterSound("MenuMusic", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameNewspaper], new SoundObject(Materials.MenuMusicResource!, true, true, true));
    Sounds.SetSound = registry.RegisterSound("SetSound", [Config.Scenes.All], new SoundObject(Materials.SetSoundResource!, false, false, true, true));
    
    Sounds.GamePhoneGuy1 = registry.RegisterSound("GamePhoneGuy1", [Config.Scenes.GameMain], new SoundObject(Materials.GamePhoneGuy1Resource!));
    Sounds.GamePhoneGuy2 = registry.RegisterSound("GamePhoneGuy2", [Config.Scenes.GameMain], new SoundObject(Materials.GamePhoneGuy2Resource!));
    Sounds.GamePhoneGuy3 = registry.RegisterSound("GamePhoneGuy3", [Config.Scenes.GameMain], new SoundObject(Materials.GamePhoneGuy3Resource!));
    Sounds.GamePhoneGuy4 = registry.RegisterSound("GamePhoneGuy4", [Config.Scenes.GameMain], new SoundObject(Materials.GamePhoneGuy4Resource!));
    Sounds.GamePhoneGuy5 = registry.RegisterSound("GamePhoneGuy5", [Config.Scenes.GameMain], new SoundObject(Materials.GamePhoneGuy5Resource!));
    Sounds.GamePhoneGuy6 = registry.RegisterSound("GamePhoneGuy6", [Config.Scenes.GameMain], new SoundObject(Materials.GamePhoneGuy6Resource!)); 
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.WhiteBlinko = registry.RegisterObject("GlobalWhiteBlinko", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameLoading, Config.Scenes.GameMain], [12], new SelectableImage(Vector2.Zero, Materials.MenuWhiteBlinkoStackResource!, Color.White));
    Objects.WhiteBlinko.AssignScript(new WhiteBlinkoScript(Objects.WhiteBlinko));
    
    Objects.MenuBackground = registry.RegisterObject("MenuBackground", [Config.Scenes.MenuMain], [0], new SelectableImage(Vector2.Zero, Materials.MenuBackgroundStackResource!, Color.White));
    Objects.Static = registry.RegisterObject("MenuStatic", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameMain, Config.Scenes.GameLose], [0, 0, 0, 0, 0, 8, 0], new SimpleAnimation(Vector2.Zero, 24, new Color(255, 255, 255, 100), AnimationPlayMode.Replacement, Materials.StaticStackResource!));
    Objects.MenuGameName = registry.RegisterObject("MenuGameName", [Config.Scenes.MenuMain], [1], new SimpleText(new Vector2(92, 16), Vector2.Zero, 62, "Five\nNights\nAt\nFreddy's\n2", Color.White, Materials.GlobalFont!));
    
    // each new line = 65px
    Objects.MenuSet = registry.RegisterObject("MenuSet", [Config.Scenes.MenuMain, Config.Scenes.MenuExtras], [1], new SimpleText(new Vector2(20, 428), Vector2.Zero, 48, ">>", Color.White, Materials.GlobalFont!));
    Objects.MenuNewGame = registry.RegisterObject("MenuNewGame", [Config.Scenes.MenuMain], [1], new HitboxText(new Vector2(92, 428), new Vector2(250, 65), 48, "New Game", Color.White, Materials.GlobalFont!));
    Objects.MenuContinue = registry.RegisterObject("MenuContinue", [Config.Scenes.MenuMain], [1], new HitboxText(new Vector2(92, 493), new Vector2(250, 65), 48, "Continue", Color.White, Materials.GlobalFont!));
    Objects.MenuExtras = registry.RegisterObject("MenuExtras", [Config.Scenes.MenuMain], [1], new HitboxText(new Vector2(92, 558), new Vector2(190, 65), 48, "Extras", Color.White, Materials.GlobalFont!));
    Objects.MenuContinueNightText = registry.RegisterObject("MenuContinueNightText", [Config.Scenes.MenuMain], [1], new SimpleText(new Vector2(326, 518), Vector2.Zero, 18, "Night #", Color.Blank, Materials.GlobalFont!));
    
    Objects.ExtrasTitle = registry.RegisterObject("ExtrasTitle", [Config.Scenes.MenuExtras], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Extras", Color.White, Materials.GlobalFont!, true, true));
    Objects.ExtrasBackToPage = registry.RegisterObject("ExtrasBackToPage", [Config.Scenes.MenuSettings, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight], [1], new HitboxText(new Vector2(50, 30), new Vector2(80, 60), 48, "<<", Color.White, Materials.GlobalFont!, true));
    Objects.ExtrasBackToPage.AssignScript(new BackToExtrasScript(Objects.ExtrasBackToPage));
    Objects.ExtrasProjectLinkGithub = registry.RegisterObject("ExtrasProjectLinkGithub", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 120), new Vector2(500, 65), 48, "Project's Github", new Color(210, 210, 255, 255), Materials.GlobalFont!, true));
    Objects.ExtrasCustomNight = registry.RegisterObject("ExtrasCustomNight", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 185), new Vector2(500, 65), 48, "Custom Night", Color.White, Materials.GlobalFont!, true));
    Objects.ExtrasSettings = registry.RegisterObject("ExtrasSettings", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 250), new Vector2(500, 65), 48, "Settings", Color.White, Materials.GlobalFont!, true));
    Objects.ExtrasCredits = registry.RegisterObject("ExtrasCredits", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 315), new Vector2(500, 65), 48, "Credits", Color.White, Materials.GlobalFont!, true));
    Objects.ExtrasBack = registry.RegisterObject("ExtrasBack", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 380), new Vector2(500, 65), 48, "Back", Color.White, Materials.GlobalFont!, true));
    
    Objects.SettingsTitle = registry.RegisterObject("SettingsTitle", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Settings", Color.White, Materials.GlobalFont!, true, true));
    Objects.SettingsFullscreenText = registry.RegisterObject("SettingsFullscreenText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 175), new Vector2(500, 65), 48, "Fullscreen", Color.White, Materials.GlobalFont!, true));
    Objects.SettingsFullscreenCheckbox = registry.RegisterObject("SettingsFullscreenCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 182), 50, Color.White));

    Objects.SettingsVsyncText = registry.RegisterObject("SettingsVsyncText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 255), new Vector2(500, 65), 48, "Vsync", Color.White, Materials.GlobalFont!, true));
    Objects.SettingsFpsShowText = registry.RegisterObject("SettingsFpsShowText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(670, 255), new Vector2(160, 65), 24, "FPS: ", Color.Blank, Materials.GlobalFont!, true));
    Objects.SettingsVsyncCheckbox = registry.RegisterObject("SettingsVsyncCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 262), 50, Color.White));

    Objects.SettingsVolumeText = registry.RegisterObject("SettingsVolumeText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 335), new Vector2(500, 65), 48, "Volume", Color.White, Materials.GlobalFont!, true));
    Objects.SettingsVolumeSlider = registry.RegisterObject("SettingsVolumeSlider", [Config.Scenes.MenuSettings], [1], new SimpleSlider(new Vector2(698, 357), new Vector2(178, 20), Color.White, 3, .5f));

    Objects.SettingsFunModeText = registry.RegisterObject("SettingsFunModeText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 415), new Vector2(500, 65), 48, "Fun Mode", Color.White, Materials.GlobalFont!, true));
    Objects.SettingsFunModeCheckbox = registry.RegisterObject("SettingsFunModeCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 422), 50, Color.White));

    Objects.SettingsDebugModeText = registry.RegisterObject("SettingsDebugModeText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 575), new Vector2(500, 65), 48, "Debug Mode", Color.White, Materials.GlobalFont!, true));
    Objects.SettingsDebugModeCheckbox = registry.RegisterObject("SettingsDebugModeCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 582), 50, Color.White));
    
    Objects.CreditsTitle = registry.RegisterObject("CreditsTitle", [Config.Scenes.MenuCredits], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Credits", Color.White, Materials.GlobalFont!, true, true));
    Objects.CreditsRaylibLogo = registry.RegisterObject("CreditsRaylibLogo", [Config.Scenes.MenuCredits], [1], new HitboxImage(new Vector2(Config.WindowWidth/2 - 275, 352), Materials.CreditsRaylibResource!, newSize: new Vector2(150, 150)));
    Objects.CreditsRaylibText = registry.RegisterObject("CreditsRaylibText", [Config.Scenes.MenuCredits], [1], new SimpleText(Objects.CreditsRaylibLogo.GetPosition() + new Vector2(0, -90), new Vector2(150, 90), 28, "Powered\nwith:", Color.White, Materials.GlobalFont!, false, true));
    Objects.CreditsScottLogo = registry.RegisterObject("CreditsScottLogo", [Config.Scenes.MenuCredits], [1], new HitboxImage(new Vector2(Config.WindowWidth/2 - 70, 352), Materials.CreditsScottResource!, newSize: new Vector2(150, 150)));
    Objects.CreditsScottText = registry.RegisterObject("CreditsScottText", [Config.Scenes.MenuCredits], [1], new SimpleText(Objects.CreditsScottLogo.GetPosition() + new Vector2(0, -90), new Vector2(150, 90), 28, "Original\nDev:", Color.White, Materials.GlobalFont!, false, true));
    Objects.CreditsMyLogo = registry.RegisterObject("CreditsMyLogo", [Config.Scenes.MenuCredits], [1], new HitboxImage(new Vector2(Config.WindowWidth/2 + 125, 352), Materials.CreditsMyResource!, newSize: new Vector2(150, 150)));
    Objects.CreditsMyText = registry.RegisterObject("CreditsMyText", [Config.Scenes.MenuCredits], [1], new SimpleText(Objects.CreditsMyLogo.GetPosition() + new Vector2(0, -90), new Vector2(150, 90), 28, "Remake\nDev:", Color.White, Materials.GlobalFont!, false, true));
    
    Objects.CustomNightTitle = registry.RegisterObject("CustomNightTitle", [Config.Scenes.MenuCustomNight], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Custom Night", Color.White, Materials.GlobalFont!, true, true));
    Objects.CustomNightStartBox = registry.RegisterObject("CustomNightStartBox", [Config.Scenes.MenuCustomNight], [1], new SelectableHitboxImage(new Vector2(140, 550), Materials.GameMusicBoxWindUpButtonResource!, Color.White, new Vector2(300, 100)));
    Objects.CustomNightStartText = registry.RegisterObject("CustomNightStartText", [Config.Scenes.MenuCustomNight], [1], new SelectableText(Objects.CustomNightStartBox.GetPosition(), Objects.CustomNightStartBox.GetSize(), 26, ["{ GOING_BACK_FROM_NONE }", "Custom preset", "{ GOING_FORWARD_FROM_NONE }", "{ GOING_BACK_FROM_PRESET }", "20/20/20/20", "New & Shiny", "Double Trouble", "Night of Misfits", "Foxy Foxy", "Ladies' Night", "Freddy's Circus", "Cupcake Challenge", "Fazbear Fever", "Golden Freddy"], Color.White, Materials.GlobalFont!, true, true));
    Objects.CustomNightPresetBox = registry.RegisterObject("CustomNightPresetBox", [Config.Scenes.MenuCustomNight], [1], new SelectableHitboxImage(new Vector2(565, 550), Materials.GameMusicBoxWindUpButtonResource!, Color.White, new Vector2(300, 100)));
    Objects.CustomNightPresetText = registry.RegisterObject("CustomNightPresetText", [Config.Scenes.MenuCustomNight], [1], new SimpleText(Objects.CustomNightPresetBox.GetPosition(), Objects.CustomNightPresetBox.GetSize(), 30, "Start night!", Color.White, Materials.GlobalFont!, true, true));
    Objects.CustomNightWitheredFreddy = registry.RegisterObject("CustomNightWitheredFreddy", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(100, 150), "0", Materials.GlobalFont!, Materials.CustomNightWitheredFreddyResource!, 2, Color.White));
    Objects.CustomNightWitheredFreddy.AssignScript(new CustomNightButtonScript(Objects.CustomNightWitheredFreddy));
    Objects.CustomNightWitheredBonnie = registry.RegisterObject("CustomNightWitheredBonnie", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(270, 150), "0", Materials.GlobalFont!, Materials.CustomNightWitheredBonnieResource!, 2, Color.White));
    Objects.CustomNightWitheredBonnie.AssignScript(new CustomNightButtonScript(Objects.CustomNightWitheredBonnie));
    Objects.CustomNightWitheredChica = registry.RegisterObject("CustomNightWitheredChica", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(440, 150), "0", Materials.GlobalFont!, Materials.CustomNightWitheredChicaResource!, 2, Color.White));
    Objects.CustomNightWitheredChica.AssignScript(new CustomNightButtonScript(Objects.CustomNightWitheredChica));
    Objects.CustomNightWitheredFoxy = registry.RegisterObject("CustomNightWitheredFoxy", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(610, 150), "0", Materials.GlobalFont!, Materials.CustomNightWitheredFoxyResource!, 2, Color.White));
    Objects.CustomNightWitheredFoxy.AssignScript(new CustomNightButtonScript(Objects.CustomNightWitheredFoxy));
    Objects.CustomNightBalloonBoy = registry.RegisterObject("CustomNightBalloonBoy", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(780, 150), "0", Materials.GlobalFont!, Materials.CustomNightBalloonBoyResource!, 2, Color.White));
    Objects.CustomNightBalloonBoy.AssignScript(new CustomNightButtonScript(Objects.CustomNightBalloonBoy));
    Objects.CustomNightToyFreddy = registry.RegisterObject("CustomNightToyFreddy", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(100, 350), "0", Materials.GlobalFont!, Materials.CustomNightToyFreddyResource!, 2, Color.White));
    Objects.CustomNightToyFreddy.AssignScript(new CustomNightButtonScript(Objects.CustomNightToyFreddy));
    Objects.CustomNightToyBonnie = registry.RegisterObject("CustomNightToyBonnie", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(270, 350), "0", Materials.GlobalFont!, Materials.CustomNightToyBonnieResource!, 2, Color.White));
    Objects.CustomNightToyBonnie.AssignScript(new CustomNightButtonScript(Objects.CustomNightToyBonnie));
    Objects.CustomNightToyChica = registry.RegisterObject("CustomNightToyChica", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(440, 350), "0", Materials.GlobalFont!, Materials.CustomNightToyChicaResource!, 2, Color.White));
    Objects.CustomNightToyChica.AssignScript(new CustomNightButtonScript(Objects.CustomNightToyChica));
    Objects.CustomNightMangle = registry.RegisterObject("CustomNightMangle", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(610, 350), "0", Materials.GlobalFont!, Materials.CustomNightMangleResource!, 2, Color.White));
    Objects.CustomNightMangle.AssignScript(new CustomNightButtonScript(Objects.CustomNightMangle));
    Objects.CustomNightGoldenFreddy = registry.RegisterObject("CustomNightGoldenFreddy", [Config.Scenes.MenuCustomNight], [1], new HitboxTextBorderImage(new Vector2(780, 350), "0", Materials.GlobalFont!, Materials.CustomNightGoldenFreddyResource!, 2, Color.White));
    Objects.CustomNightGoldenFreddy.AssignScript(new CustomNightButtonScript(Objects.CustomNightGoldenFreddy));
    
    Objects.LoadingNightText = registry.RegisterObject("LoadingNightText", [Config.Scenes.GameLoading], [1], new SimpleText(new Vector2(0, -40), new Vector2(1024, 768), 48, "Night #", Color.White, Materials.GlobalFont!, true, true));
    Objects.LoadingAmText = registry.RegisterObject("LoadingAmText", [Config.Scenes.GameLoading], [1], new SimpleText(new Vector2(0, 40), new Vector2(1024, 768), 48, "12 AM", Color.White, Materials.GlobalFont!, true, true));
    Objects.LoadingClockThingo = registry.RegisterObject("LoadingClockThingo", [Config.Scenes.GameLoading], [1], new SimpleImage(new Vector2(Config.WindowWidth-Materials.LoadingClockResource!.GetSize().X-20, Config.WindowHeight-Materials.LoadingClockResource.GetSize().Y-20), Materials.LoadingClockResource, Color.Blank));
    
    Objects.GameNewspapers = registry.RegisterObject("GameNewspapers", [Config.Scenes.GameNewspaper], [0], new SimpleImage(Vector2.Zero, Materials.GameNewspaperYooo!, Color.White));
    
    Objects.GameOfficeCamera = registry.RegisterObject("GameOffice", [Config.Scenes.GameMain], [0], new SelectablePackedImage(Vector2.Zero, Materials.GameOfficeCameraResource!, Color.White));
    Objects.GameOfficeTable = registry.RegisterObject("GameOfficeTable", [Config.Scenes.GameMain], [2], new SimpleAnimation(Vector2.Zero, 18, Color.White, AnimationPlayMode.Replacement, Materials.GameOfficeTableResource!));
    Objects.GameCameraOutline = registry.RegisterObject("GameCameraOutline", [Config.Scenes.GameMain], [8], new SimpleImage(Vector2.Zero, Materials.GameCameraOutlineResource!));
    Objects.GameCameraRecord = registry.RegisterObject("GameCameraRecord", [Config.Scenes.GameMain], [8], new SimpleImage(new Vector2(36, 30), Materials.GameCameraRecordResource!));
    Objects.GameCentralOfficeScroller = registry.RegisterObject("GameCentralOfficeScroller", [Config.Scenes.GameMain], [7], new DebugBox(new Vector2(Config.WindowWidth/2 - 2, 0), new Vector2(4, Config.WindowHeight), Color.Gold));
    Objects.GameOfficeScroller = registry.RegisterObject("GameOfficeScroller", [Config.Scenes.GameMain], [0], new DebugBox(Vector2.Zero, new Vector2(4), Color.Lime));
    Objects.GameCameraScroller = registry.RegisterObject("GameCameraScroller", [Config.Scenes.GameMain], [0], new DebugBox(Vector2.Zero, new Vector2(4), Color.SkyBlue));
    Objects.GameCameraScroller.AssignScript(new CameraScrollerScript(Objects.GameCameraScroller));
    Objects.GameLeftLightSwitch = registry.RegisterObject("GameLeftLightSwitch", [Config.Scenes.GameMain], [2], new SelectableHitboxImage(Vector2.Zero, Materials.GameLeftLightResource!));
    Objects.GameRightLightSwitch = registry.RegisterObject("GameRightLightSwitch", [Config.Scenes.GameMain], [2], new SelectableHitboxImage(Vector2.Zero, Materials.GameRightLightResource!));
    Objects.GameBlackoutRectangle = registry.RegisterObject("GameBlackoutRectangle", [Config.Scenes.GameMain], [7], new SimpleBox(Vector2.Zero, new Vector2(Config.WindowWidth, Config.WindowHeight), Color.Blank));
    Objects.GameOfficeToyFreddy = registry.RegisterObject("GameOfficeToyFreddy", [Config.Scenes.GameMain], [1], new SimpleImage(Vector2.Zero, Materials.GameOfficeToyFreddyResource!));
    Objects.GameOfficeToyBonnie = registry.RegisterObject("GameOfficeToyBonnie", [Config.Scenes.GameMain], [3], new SimpleImage(Vector2.Zero, Materials.GameOfficeToyBonnieResource!));
    Objects.GameOfficeToyChica = registry.RegisterObject("GameOfficeToyChica", [Config.Scenes.GameMain], [4], new SimpleImage(Vector2.Zero, Materials.GameOfficeToyChicaResource!));
    Objects.GameOfficeMangle = registry.RegisterObject("GameOfficeMangle", [Config.Scenes.GameMain], [5], new SimpleImage(Vector2.Zero, Materials.GameOfficeMangleResource!));
    Objects.GameOfficeBalloonBoy = registry.RegisterObject("GameOfficeBalloonBoy", [Config.Scenes.GameMain], [6], new SimpleImage(Vector2.Zero, Materials.GameOfficeBalloonBoyResource!));
    Objects.GameUiMapWithCams = registry.RegisterObject("GameUiMapWithCams", [Config.Scenes.GameMain], [8], new CamMap(new SimpleImage(new Vector2(560, 385), Materials.GameCameraMapResource!), Materials.GameCameraCamsUnitResource!, Materials.PixilatedFont!, [
      new Vector2(587, 575),
      new Vector2(720, 576),
      new Vector2(586, 508),
      new Vector2(722, 508),
      new Vector2(592, 670),
      new Vector2(715, 670),
      new Vector2(742, 450),
      new Vector2(588, 437),
      new Vector2(900, 408),
      new Vector2(833, 528),
      new Vector2(934, 484),
      new Vector2(921, 577)
    ], 8));
    Objects.GameUiMapCamsTexts = registry.RegisterObject("GameUiMapCamsTexts", [Config.Scenes.GameMain], [8], new SelectableText(new Vector2(563, 330), Vector2.Zero, 40, [
      "Party Room 1",
      "Party Room 2",
      "Party Room 3",
      "Party Room 4",
      "Left Air Vent",
      "Right Air Vent",
      "Main Hall",
      "Parts/Services",
      "Show Stage",
      "Game Area",
      "Prize Corner",
      "Kid's Cove"
    ], Color.White, Materials.PixilatedFont!));
    Objects.GameMusicBoxCircular = registry.RegisterObject("GameMusicBoxCircular", [Config.Scenes.GameMain], [10], new SimpleCircular(new Vector2(328, 658), 27, 0.025f, 0.059f, Color.White));
    Objects.GameMusicBoxBox = registry.RegisterObject("GameMusicBoxBox", [Config.Scenes.GameMain], [9], new SelectableHitboxImage(new Vector2(365, 608), Materials.GameMusicBoxWindUpButtonResource!));
    Objects.GameMusicBoxText = registry.RegisterObject("GameMusicBoxText", [Config.Scenes.GameMain], [9], new SimpleText(Objects.GameMusicBoxBox.GetPosition(), Materials.GameMusicBoxWindUpButtonResource!.GetSize(), 23, "Wind Up\nMusic Box", Color.White, Materials.PixilatedFont!));
    Objects.GameMusicBoxBottomText = registry.RegisterObject("GameMusicBoxBottomText", [Config.Scenes.GameMain], [9], new SimpleText(Objects.GameMusicBoxBox.GetPosition() + new Vector2(0, 60), new Vector2(Materials.GameMusicBoxWindUpButtonResource.GetSize().X, 30), 19, "click & hold", Color.White, Materials.PixilatedFont!, false, true));
    Objects.GameUiBattery = registry.RegisterObject("GameUiBattery", [Config.Scenes.GameMain], [7], new SelectableImage(new Vector2(18), Materials.GameUiBatteryResource!));
    Objects.GameUiNight = registry.RegisterObject("GameUiNight", [Config.Scenes.GameMain], [99], new SimpleText(new Vector2(863, 14), Vector2.Zero, 28, "Night #", Color.White, Materials.GlobalFont!));
    Objects.GameUiHour = registry.RegisterObject("GameUiHour", [Config.Scenes.GameMain], [99], new SimpleText(new Vector2(883, 50), Vector2.Zero, 28, "## AM", Color.White, Materials.GlobalFont!));
    Objects.GameUiMaskButton = registry.RegisterObject("GameUiMaskButton", [Config.Scenes.GameMain], [20], new HitboxImage(new Vector2(6, Config.WindowHeight - 50), Materials.GameUiMaskButtonResource!));
    Objects.GameUiCameraButton = registry.RegisterObject("GameUiCameraButton", [Config.Scenes.GameMain], [21], new HitboxImage(new Vector2(518, Config.WindowHeight - 50), Materials.GameUiCameraButtonResource!));
    Objects.GameUiCamera = registry.RegisterObject("GameUiCamera", [Config.Scenes.GameMain], [19], new SelectableAnimation(Vector2.Zero, 30, Color.White, AnimationPlayMode.Replacement, Materials.GameUiCameraResource!));
    Objects.GameUiCamera.AssignScript(new PullAnimationScript(Objects.GameUiCamera));
    Objects.GameUiMask = registry.RegisterObject("GameUiMask", [Config.Scenes.GameMain], [19], new SelectableAnimation(Vector2.Zero, 30, Color.White, AnimationPlayMode.Replacement, Materials.GameUiMaskResource!));
    Objects.GameUiMask.AssignScript(new PullAnimationScript(Objects.GameUiMask));
    Objects.GameUiMute = registry.RegisterObject("GameUiMute", [Config.Scenes.GameMain], [98], new HitboxImage(new Vector2(130, 26), Materials.GameUiMuteResource!));
    Objects.GameJumpscares = registry.RegisterObject("GameJumpscares", [Config.Scenes.GameMain], [7], new SelectableAnimation(Vector2.Zero, 30, Color.White, AnimationPlayMode.Replacement, Materials.GameJumpscaresResource!));
  }
  
  

  public static void CustomInitialisation(Registry registry)
  {
    //registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.Mangle, 3f, AnimatronicType.AutoBlackouter, Location.OfficeInside, [
    //  new MovementOpportunity(Location.Cam09, Location.OfficeFront, 1f),
    //  new MovementOpportunity(Location.OfficeFront, Location.OfficeFront, .5f),
    //  new MovementOpportunity(Location.OfficeFront, Location.OfficeInside, .5f),
    //  new MovementOpportunity(Location.OfficeInside, Location.Cam09, 1f)
    //], [
    //  new GrantOpportunity(Config.AnimatronicsNames.WitheredFoxy, Location.OfficeFront)
    //]));
    
    registry.fnaf.animatronicManager.Add(new Animatronic(Config.AnimatronicsNames.ToyFreddy, 3.1f, AnimatronicType.AutoBlackouter, Location.OfficeInside, 
    [], [
      new MovementOpportunity(Location.Cam09, Location.OfficeFront, .6f),
      new MovementOpportunity(Location.Cam09, Location.Cam09, .4f),       
      
      new MovementOpportunity(Location.OfficeFront, Location.OfficeInside, .3f),
      new MovementOpportunity(Location.OfficeFront, Location.OfficeFront, .7f),
      
      new MovementOpportunity(Location.OfficeInside, Location.Cam09, 1f),
    ]));

    //registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.ToyBonnie, 3.1f, AnimatronicType.AutoBlackouter, Location.OfficeInside, [
    //  new MovementOpportunity(Location.Cam09, Location.Cam06, .6f),
    //  new MovementOpportunity(Location.Cam09, Location.Cam09, .6f),
    //  
    //  new MovementOpportunity(Location.Cam06, Location.OfficeRight, .4f),
    //  new MovementOpportunity(Location.Cam06, Location.Cam06, .6f),
    //  
    //  new MovementOpportunity(Location.OfficeRight, Location.OfficeInside, 1f),
    //  
    //  new MovementOpportunity(Location.OfficeInside, Location.Cam09, 1f),
    //]));
    
    //registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.WitheredFoxy, 3.2f, AnimatronicType.AutoBlackouter, Location.Cam01, [
    //  new MovementOpportunity(Location.Cam08, Location.OfficeFront, 1f),
    //  new MovementOpportunity(Location.OfficeFront, Location.Cam01, .1f),
    //  new MovementOpportunity(Location.OfficeFront, Location.OfficeFront, .9f)
    //], [
    //  new GrantOpportunity(Config.AnimatronicsNames.Mangle, Location.OfficeFront)
    //]));
  }
  
  public static void RegistryInitialisation(Registry registry)
  {
    registry.AssignSceneScript(Config.Scenes.DebuggerTesting, new DebuggerTesting());
    
    registry.AssignSceneScript(Config.Scenes.MenuMain, new MenuMain());
    registry.AssignSceneScript(Config.Scenes.MenuSettings, new MenuSettings());
    registry.AssignSceneScript(Config.Scenes.MenuExtras, new MenuExtras());
    registry.AssignSceneScript(Config.Scenes.MenuCredits, new MenuCredits());
    registry.AssignSceneScript(Config.Scenes.MenuCustomNight, new MenuCustomNight());
    
    registry.AssignSceneScript(Config.Scenes.GameLose, new GameLose());
    registry.AssignSceneScript(Config.Scenes.GameWin, new GameWin());
    registry.AssignSceneScript(Config.Scenes.GameMain, new GameMain());
    registry.AssignSceneScript(Config.Scenes.GameLoading, new GameLoading());
    registry.AssignSceneScript(Config.Scenes.GameNewspaper, new GameNewspaper());
    
    registry.AssignGlobalScript(new GlobalOverlay());
    registry.AssignGuiScript(new ImGuiWindow());
    
    #if DEBUG
    registry.DebugMode = true;
    #endif
  }
}