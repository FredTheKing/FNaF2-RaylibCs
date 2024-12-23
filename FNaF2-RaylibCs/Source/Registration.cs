using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;
using FNaF2_RaylibCs.Source.Packages.Objects.Box;
using FNaF2_RaylibCs.Source.Packages.Objects.Checkbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.Slider;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.Scripts.Scenes;
using FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;
using FNaF2_RaylibCs.Source.Scripts.Scenes.Game;
using FNaF2_RaylibCs.Source.Scripts.Objects;
using FNaF2_RaylibCs.Source.Scripts.Scenes.Debugger;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public static class Registration
{
  public struct Materials
  {
    public static SoundResource? MenuMusicResource;
    public static SoundResource? SetSoundResource;
    
    public static FontResource? GlobalFont;
    public static FontResource? MenuFont;

    public static ImageStackResource? MenuBackgroundStackResource;
    public static ImageStackResource? MenuStaticStackResource;
    public static ImageStackResource? MenuWhiteBlinkoStackResource;
    
    public static ImageResource? CreditsRaylibResource;
    public static ImageResource? CreditsMyResource;
    public static ImageResource? CreditsScottResource;

    public static ImageResource? LoadingClockResource;

    public static ShaderResource? GamePerspectiveShaderResource;
    public static ImageResource? GameNewspaperYooo;
    public static ImageStackResource? GameOfficeResource;
    public static ImageStackResource? GameOfficeTableResource;
    public static ImageStackResource? GameLeftLightResource;
    public static ImageStackResource? GameRightLightResource;
    public static ImageResource? GameOfficeToyFreddyResource;
    public static ImageResource? GameOfficeToyBonnieResource;
    public static ImageResource? GameOfficeToyChicaResource;
    public static ImageResource? GameOfficeMangleResource;
    public static ImageResource? GameOfficeBalloonBoyResource;
  }

  public struct Sounds
  {
    public static SoundObject? MenuMusic;
    public static SoundObject? SetSound;
  }
  
  public struct Objects
  {
    public static SelectableImage? MenuWhiteBlinko;
    
    public static SelectableImage? MenuBackground;
    public static SimpleAnimation? MenuStatic;
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
    public static SimpleImage? CreditsRaylibLogo;
    public static SimpleText? CreditsRaylibText;
    public static SimpleImage? CreditsMyLogo;
    public static SimpleText? CreditsMyText;
    public static SimpleImage? CreditsScottLogo;
    public static SimpleText? CreditsScottText;
    
    public static SimpleText? CustomNightTitle;

    public static SimpleText? LoadingNightText;
    public static SimpleText? LoadingAmText;
    public static SimpleImage? LoadingClockThingo;

    public static SimpleImage? GameNewspapers;
    public static SelectableImage? GameOffice;
    public static SimpleBox? GameBlackoutRectangle;
    public static SimpleAnimation? GameOfficeTable;
    public static DebugBox? GameCentralScroller;
    public static SelectableHitboxImage? GameLeftLightSwitch;
    public static SelectableHitboxImage? GameRightLightSwitch;
    public static SimpleImage? GameOfficeToyFreddy;
    public static SimpleImage? GameOfficeToyBonnie;
    public static SimpleImage? GameOfficeToyChica;
    public static SimpleImage? GameOfficeMangle;
    public static SimpleImage? GameOfficeBalloonBoy;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.MenuMusicResource = registry.RegisterMaterial("MenuMusicResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameNewspaper], new SoundResource(Config.ResPath + "Sound/MenuMusic.wav"));
    Materials.SetSoundResource = registry.RegisterMaterial("SetSoundResource", [Config.AllScenesShortcut], new SoundResource(Config.ResPath + "Sound/Set.wav"));
    
    Materials.GlobalFont = registry.RegisterMaterial("GlobalFont", [Config.AllScenesShortcut], new FontResource(Config.ResPath + "Font/consolas.ttf", 128));
    Materials.MenuFont = registry.RegisterMaterial("MenuFont", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameLoading], new FontResource(Config.ResPath + "Font/regular.ttf", 128));
    
    Materials.MenuBackgroundStackResource = registry.RegisterMaterial("MenuBackgroundStackResource", [Config.Scenes.MenuMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/Background", 4)));
    Materials.MenuStaticStackResource = registry.RegisterMaterial("MenuStaticStackResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/Static", 8)));
    
    Materials.CreditsRaylibResource = registry.RegisterMaterial("CreditsRaylibResource", [Config.Scenes.MenuCredits], new ImageResource(Config.ResPath + "Credits/Raylib.png"));
    Materials.CreditsMyResource = registry.RegisterMaterial("CreditsMyResource", [Config.Scenes.MenuCredits], new ImageResource(Config.ResPath + "Credits/Me.png"));
    Materials.CreditsScottResource = registry.RegisterMaterial("CreditsScottResource", [Config.Scenes.MenuCredits], new ImageResource(Config.ResPath + "Credits/Scott.png"));
    
    Materials.LoadingClockResource = registry.RegisterMaterial("LoadingClockResource", [Config.Scenes.GameLoading], new ImageResource(Config.ResPath + "Game/Etc/Clock.png"));
    
    Materials.GameNewspaperYooo = registry.RegisterMaterial("GameNewspaperYooo", [Config.Scenes.GameNewspaper], new ImageResource(Config.ResPath + "Game/Etc/Newspaper.png"));
    Materials.GameNewspaperYooo.SetFilter(TextureFilter.Bilinear);
    
    Materials.GamePerspectiveShaderResource = registry.RegisterMaterial("GamePerspectiveShaderResource", [Config.Scenes.GameMain], new ShaderResource(Config.ResPath + "Shaders/perspective.vs", Config.ResPath + "Shaders/perspective.fs"));
    Materials.GameOfficeResource = registry.RegisterMaterial("GameOfficeResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office", 22)));
    Materials.GameOfficeTableResource = registry.RegisterMaterial("GameOfficeTableResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office/Table", 4)));
    Materials.GameLeftLightResource = registry.RegisterMaterial("GameLeftLightResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office/LightButtons", 2)));
    Materials.GameRightLightResource = registry.RegisterMaterial("GameRightLightResource", [Config.Scenes.GameMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Game/Main/Office/LightButtons", 2, 2)));
    Materials.GameOfficeToyFreddyResource = registry.RegisterMaterial("GameOfficeToyFreddyResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/ToyFreddy.png"));
    Materials.GameOfficeToyBonnieResource = registry.RegisterMaterial("GameOfficeToyBonnieResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/ToyBonnie.png"));
    Materials.GameOfficeToyChicaResource = registry.RegisterMaterial("GameOfficeToyChicaResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/ToyChica.png"));
    Materials.GameOfficeMangleResource = registry.RegisterMaterial("GameOfficeMangleResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/Mango.png"));
    Materials.GameOfficeBalloonBoyResource = registry.RegisterMaterial("GameOfficeBalloonBoyResource", [Config.Scenes.GameMain], new ImageResource(Config.ResPath + "Game/Main/Office/Insiders/BB.png"));
    
    Materials.MenuWhiteBlinkoStackResource = registry.RegisterMaterial("GlobalWhiteBlinkoStackResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameLoading], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/WhiteBlinko", 6)));
  }
  
  public static void SoundsInitialisation(Registry registry)
  {
    Sounds.MenuMusic = registry.RegisterSound("MenuMusic", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameNewspaper], new SoundObject(Materials.MenuMusicResource!, true, true, true));
    Sounds.SetSound = registry.RegisterSound("SetSound", [Config.AllScenesShortcut], new SoundObject(Materials.SetSoundResource!, false, false, true, true));
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.MenuWhiteBlinko = registry.RegisterObject("GlobalWhiteBlinko", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameLoading], [9], new SelectableImage(Vector2.Zero, Materials.MenuWhiteBlinkoStackResource!, Color.White));
    Objects.MenuWhiteBlinko.AssignObjectScript(new WhiteBlinkoScript(Objects.MenuWhiteBlinko));
    
    Objects.MenuBackground = registry.RegisterObject("MenuBackground", [Config.Scenes.MenuMain], [0], new SelectableImage(Vector2.Zero, Materials.MenuBackgroundStackResource!, Color.White));
    Objects.MenuStatic = registry.RegisterObject("MenuStatic", [Config.Scenes.MenuMain, "Menu/Settings", Config.Scenes.MenuExtras, "Menu/Credits", "Menu/CustomNight"], [0], new SimpleAnimation(Vector2.Zero, 24, new Color(255, 255, 255, 100), AnimationPlayMode.Replacement, Materials.MenuStaticStackResource!));
    Objects.MenuGameName = registry.RegisterObject("MenuGameName", [Config.Scenes.MenuMain], [1], new SimpleText(new Vector2(92, 16), Vector2.Zero, 62, "Five\n\n\n\nNights\n\n\n\nAt\n\n\n\nFreddy's\n\n\n\n2", Color.White, Materials.MenuFont!));
    
    // each new line = 65px
    Objects.MenuSet = registry.RegisterObject("MenuSet", [Config.Scenes.MenuMain, Config.Scenes.MenuExtras], [1], new SimpleText(new Vector2(20, 428), Vector2.Zero, 48, ">>", Color.White, Materials.MenuFont!));
    Objects.MenuNewGame = registry.RegisterObject("MenuNewGame", [Config.Scenes.MenuMain], [1], new HitboxText(new Vector2(92, 428), new Vector2(250, 65), 48, "New Game", Color.White, Materials.MenuFont!));
    Objects.MenuContinue = registry.RegisterObject("MenuContinue", [Config.Scenes.MenuMain], [1], new HitboxText(new Vector2(92, 493), new Vector2(250, 65), 48, "Continue", Color.White, Materials.MenuFont!));
    Objects.MenuExtras = registry.RegisterObject("MenuExtras", [Config.Scenes.MenuMain], [1], new HitboxText(new Vector2(92, 558), new Vector2(190, 65), 48, "Extras", Color.White, Materials.MenuFont!));
    Objects.MenuContinueNightText = registry.RegisterObject("MenuContinueNightText", [Config.Scenes.MenuMain], [1], new SimpleText(new Vector2(326, 518), Vector2.Zero, 18, "Night #", Color.Blank, Materials.MenuFont!));
    
    Objects.ExtrasTitle = registry.RegisterObject("ExtrasTitle", [Config.Scenes.MenuExtras], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Extras", Color.White, Materials.MenuFont!, true, true));
    Objects.ExtrasBackToPage = registry.RegisterObject("ExtrasBackToPage", [Config.Scenes.MenuSettings, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight], [1], new HitboxText(new Vector2(50, 30), new Vector2(80, 60), 48, "<<", Color.White, Materials.MenuFont!, true));
    Objects.ExtrasBackToPage.AssignObjectScript(new BackToExtrasScript(Objects.ExtrasBackToPage));
    Objects.ExtrasProjectLinkGithub = registry.RegisterObject("ExtrasProjectLinkGithub", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 120), new Vector2(500, 65), 48, "Project's Github", new Color { R = 210, G = 210, B = 255, A = 255 }, Materials.MenuFont!, true));
    Objects.ExtrasAuthorLinkGithub = registry.RegisterObject("ExtrasAuthorLinkGithub", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 185), new Vector2(500, 65), 48, "Author's Github", new Color { R = 210, G = 210, B = 255, A = 255 }, Materials.MenuFont!, true));
    Objects.ExtrasCustomNight = registry.RegisterObject("ExtrasCustomNight", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 250), new Vector2(500, 65), 48, "Custom Night", Color.White, Materials.MenuFont!, true));
    Objects.ExtrasSettings = registry.RegisterObject("ExtrasSettings", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 315), new Vector2(500, 65), 48, "Settings", Color.White, Materials.MenuFont!, true));
    Objects.ExtrasCredits = registry.RegisterObject("ExtrasCredits", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 380), new Vector2(500, 65), 48, "Credits", Color.White, Materials.MenuFont!, true));
    Objects.ExtrasBack = registry.RegisterObject("ExtrasBack", [Config.Scenes.MenuExtras], [1], new HitboxText(new Vector2(92, 445), new Vector2(500, 65), 48, "Back", Color.White, Materials.MenuFont!, true));
    
    Objects.SettingsTitle = registry.RegisterObject("SettingsTitle", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Settings", Color.White, Materials.MenuFont!, true, true));
    Objects.SettingsFullscreenText = registry.RegisterObject("SettingsFullscreenText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 175), new Vector2(500, 65), 48, "Fullscreen", Color.White, Materials.MenuFont!, true));
    Objects.SettingsFullscreenCheckbox = registry.RegisterObject("SettingsFullscreenCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 182), 50, Color.White));

    Objects.SettingsVsyncText = registry.RegisterObject("SettingsVsyncText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 255), new Vector2(500, 65), 48, "Vsync", Color.White, Materials.MenuFont!, true));
    Objects.SettingsFpsShowText = registry.RegisterObject("SettingsFpsShowText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(670, 255), new Vector2(160, 65), 24, "FPS: ", Color.Blank, Materials.MenuFont!, true));
    Objects.SettingsVsyncCheckbox = registry.RegisterObject("SettingsVsyncCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 262), 50, Color.White));

    Objects.SettingsVolumeText = registry.RegisterObject("SettingsVolumeText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 335), new Vector2(500, 65), 48, "Volume", Color.White, Materials.MenuFont!, true));
    Objects.SettingsVolumeSlider = registry.RegisterObject("SettingsVolumeSlider", [Config.Scenes.MenuSettings], [1], new SimpleSlider(new Vector2(698, 357), new Vector2(178, 20), Color.White, 3, .5f));

    Objects.SettingsFunModeText = registry.RegisterObject("SettingsFunModeText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 415), new Vector2(500, 65), 48, "Fun Mode", Color.White, Materials.MenuFont!, true));
    Objects.SettingsFunModeCheckbox = registry.RegisterObject("SettingsFunModeCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 422), 50, Color.White));

    Objects.SettingsDebugModeText = registry.RegisterObject("SettingsDebugModeText", [Config.Scenes.MenuSettings], [1], new SimpleText(new Vector2(112, 575), new Vector2(500, 65), 48, "Debug Mode", Color.White, Materials.MenuFont!, true));
    Objects.SettingsDebugModeCheckbox = registry.RegisterObject("SettingsDebugModeCheckbox", [Config.Scenes.MenuSettings], [1], new SimpleCheckbox(new Vector2(832, 582), 50, Color.White));
    
    Objects.CreditsTitle = registry.RegisterObject("CreditsTitle", [Config.Scenes.MenuCredits], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Credits", Color.White, Materials.MenuFont!, true, true));
    Objects.CreditsRaylibLogo = registry.RegisterObject("CreditsRaylibLogo", [Config.Scenes.MenuCredits], [1], new SimpleImage(new Vector2(Config.WindowWidth/2 - 275, 352), Materials.CreditsRaylibResource!, Color.White, new Vector2(150, 150)));
    Objects.CreditsRaylibText = registry.RegisterObject("CreditsRaylibText", [Config.Scenes.MenuCredits], [1], new SimpleText(Objects.CreditsRaylibLogo.GetPosition() + new Vector2(0, -90), new Vector2(150, 90), 28, "Powered\n\nwith:", Color.White, Materials.MenuFont!, false, true));
    Objects.CreditsScottLogo = registry.RegisterObject("CreditsScottLogo", [Config.Scenes.MenuCredits], [1], new SimpleImage(new Vector2(Config.WindowWidth/2 - 70, 352), Materials.CreditsScottResource!, Color.White, new Vector2(150, 150)));
    Objects.CreditsScottText = registry.RegisterObject("CreditsScottText", [Config.Scenes.MenuCredits], [1], new SimpleText(Objects.CreditsScottLogo.GetPosition() + new Vector2(0, -90), new Vector2(150, 90), 28, "Original\n\nDev:", Color.White, Materials.MenuFont!, false, true));
    Objects.CreditsMyLogo = registry.RegisterObject("CreditsMyLogo", [Config.Scenes.MenuCredits], [1], new SimpleImage(new Vector2(Config.WindowWidth/2 + 125, 352), Materials.CreditsMyResource!, Color.White, new Vector2(150, 150)));
    Objects.CreditsMyText = registry.RegisterObject("CreditsMyText", [Config.Scenes.MenuCredits], [1], new SimpleText(Objects.CreditsMyLogo.GetPosition() + new Vector2(0, -90), new Vector2(150, 90), 28, "Remake\n\nDev:", Color.White, Materials.MenuFont!, false, true));
    
    Objects.CustomNightTitle = registry.RegisterObject("CustomNightTitle", [Config.Scenes.MenuCustomNight], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Custom Night", Color.White, Materials.MenuFont!, true, true));
    
    Objects.LoadingNightText = registry.RegisterObject("LoadingNightText", [Config.Scenes.GameLoading], [1], new SimpleText(new Vector2(0, -40), new Vector2(1024, 768), 48, "Night #", Color.White, Materials.MenuFont!, true, true));
    Objects.LoadingAmText = registry.RegisterObject("LoadingAmText", [Config.Scenes.GameLoading], [1], new SimpleText(new Vector2(0, 40), new Vector2(1024, 768), 48, "12 AM", Color.White, Materials.MenuFont!, true, true));
    Objects.LoadingClockThingo = registry.RegisterObject("LoadingClockThingo", [Config.Scenes.GameLoading], [1], new SimpleImage(new Vector2(Config.WindowWidth-Materials.LoadingClockResource!.GetSize().X-20, Config.WindowHeight-Materials.LoadingClockResource.GetSize().Y-20), Materials.LoadingClockResource, Color.Blank));
    
    Objects.GameNewspapers = registry.RegisterObject("GameNewspapers", [Config.Scenes.GameNewspaper], [0], new SimpleImage(Vector2.Zero, Materials.GameNewspaperYooo!, Color.White));
    
    Objects.GameOffice = registry.RegisterObject("GameOffice", [Config.Scenes.GameMain], [0], new SelectableImage(Vector2.Zero, Materials.GameOfficeResource!, Color.White));
    Objects.GameOfficeTable = registry.RegisterObject("GameOfficeTable", [Config.Scenes.GameMain], [6], new SimpleAnimation(Vector2.Zero, 18, Color.White, AnimationPlayMode.Replacement, Materials.GameOfficeTableResource!));
    Objects.GameBlackoutRectangle = registry.RegisterObject("GameBlackoutRectangle", [Config.Scenes.GameMain], [6], new SimpleBox(Vector2.Zero, new Vector2(Config.WindowWidth, Config.WindowHeight), Color.Blank));
    Objects.GameCentralScroller = registry.RegisterObject("GameCentralScroller", [Config.Scenes.GameMain], [99], new DebugBox(new Vector2(Config.WindowWidth/2 - 2, 0), new Vector2(4, Config.WindowHeight), Color.Gold));
    Objects.GameLeftLightSwitch = registry.RegisterObject("GameLeftLightSwitch", [Config.Scenes.GameMain], [0], new SelectableHitboxImage(Vector2.Zero, Materials.GameLeftLightResource!));
    Objects.GameRightLightSwitch = registry.RegisterObject("GameRightLightSwitch", [Config.Scenes.GameMain], [0], new SelectableHitboxImage(Vector2.Zero, Materials.GameRightLightResource!));
    Objects.GameOfficeToyFreddy = registry.RegisterObject("GameOfficeToyFreddy", [Config.Scenes.GameMain], [1], new SimpleImage(Vector2.Zero, Materials.GameOfficeToyFreddyResource!));
    Objects.GameOfficeToyBonnie = registry.RegisterObject("GameOfficeToyBonnie", [Config.Scenes.GameMain], [2], new SimpleImage(Vector2.Zero, Materials.GameOfficeToyBonnieResource!));
    Objects.GameOfficeToyChica = registry.RegisterObject("GameOfficeToyChica", [Config.Scenes.GameMain], [3], new SimpleImage(Vector2.Zero, Materials.GameOfficeToyChicaResource!));
    Objects.GameOfficeMangle = registry.RegisterObject("GameOfficeMangle", [Config.Scenes.GameMain], [4], new SimpleImage(Vector2.Zero, Materials.GameOfficeMangleResource!));
    Objects.GameOfficeBalloonBoy = registry.RegisterObject("GameOfficeBalloonBoy", [Config.Scenes.GameMain], [5], new SimpleImage(Vector2.Zero, Materials.GameOfficeBalloonBoyResource!));
  }
  
  

  public static void CustomInitialisation(Registry registry)
  {
    Scene gameScene = registry.GetSceneManager().GetScenes()[Config.Scenes.GameMain];
    
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.ToyFreddy, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam09, Location.OfficeFrontFar, 1f),
      new MovementOpportunity(Location.Cam09, Location.Cam09, .9f),
      new MovementOpportunity(Location.OfficeFrontFar, Location.OfficeFrontClose, .3f),
      new MovementOpportunity(Location.OfficeFrontFar, Location.OfficeInside, .1f),
      new MovementOpportunity(Location.OfficeFrontFar, Location.OfficeFrontFar, .6f),
      new MovementOpportunity(Location.OfficeFrontClose, Location.OfficeInside, .2f),
      new MovementOpportunity(Location.OfficeFrontClose, Location.OfficeFrontClose, .8f),
      new MovementOpportunity(Location.OfficeInside, Location.Cam09, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.ToyBonnie, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam09, Location.Cam07, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.ToyChica, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam09, Location.Cam07, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.Mangle, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam12, Location.Cam11, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.BalloonBoy, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam12, Location.Cam11, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.WitheredFreddy, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam08, Location.Cam07, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.WitheredBonnie, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam08, Location.Cam07, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.WitheredChica, 3f, AnimatronicType.AutoBlackouter, [
      new MovementOpportunity(Location.Cam08, Location.Cam07, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.WitheredFoxy, 3f, AnimatronicType.LightHater, [
      new MovementOpportunity(Location.Cam08, Location.Cam07, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.Marionette, 3f, AnimatronicType.TriggerWaiter, [
      new MovementOpportunity(Location.Cam12, Location.Cam11, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.GoldenFreddy, 3f, AnimatronicType.TriggerWaiter, [
      new MovementOpportunity(Location.Cam12, Location.Cam11, 1f)
    ]));
    registry.GetFNaF().GetAnimatronicManager().Add(new Animatronic(gameScene, Config.AnimatronicsNames.Endo, 3f, AnimatronicType.TriggerWaiter, [
      new MovementOpportunity(Location.Cam12, Location.Cam11, 1f)
    ]));
  }
  
  public static void RegistryInitialisation(Registry registry)
  {
    registry.AssignSceneScript("Debugger/Testing", new DebuggerTesting());
    
    registry.AssignSceneScript("Menu/Main", new MenuMain());
    registry.AssignSceneScript("Menu/Settings", new MenuSettings());
    registry.AssignSceneScript("Menu/Extras", new MenuExtras());
    registry.AssignSceneScript("Menu/Credits", new MenuCredits());
    registry.AssignSceneScript("Menu/CustomNight", new MenuCustomNight());
    
    registry.AssignSceneScript("Game/Main", new GameMain());
    registry.AssignSceneScript("Game/Loading", new GameLoading());
    registry.AssignSceneScript("Game/Newspaper", new GameNewspaper());
    
    registry.AssignGlobalScript(new GlobalOverlay());
    registry.AssignGuiScript(new ImGuiWindow());
    
    #if DEBUG
    registry.SwitchDebugMode();
    #endif
  }
}