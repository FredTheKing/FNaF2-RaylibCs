using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;
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

    public static ImageResource? GameNewspaperYooo;
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
    
    public static SimpleText? CustomNightTitle;

    public static SimpleText? LoadingNightText;
    public static SimpleText? LoadingAmText;

    public static SimpleImage? GameNewspapers;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.MenuMusicResource = registry.RegisterMaterial("MenuMusicResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameNewspaper], new SoundResource(Config.ResPath + "Sound/MenuMusic.wav"));
    Materials.SetSoundResource = registry.RegisterMaterial("SetSoundResource", [Config.AllScenesShortcut], new SoundResource(Config.ResPath + "Sound/Set.wav"));
    
    Materials.GlobalFont = registry.RegisterMaterial("GlobalFont", [Config.AllScenesShortcut], new FontResource(Config.ResPath + "Font/consolas.ttf", 128));
    Materials.MenuFont = registry.RegisterMaterial("MenuFont", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight, Config.Scenes.GameLoading], new FontResource(Config.ResPath + "Font/regular.ttf", 128));
    
    Materials.MenuBackgroundStackResource = registry.RegisterMaterial("MenuBackgroundStackResource", [Config.Scenes.MenuMain], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/Background", 4)));
    Materials.MenuStaticStackResource = registry.RegisterMaterial("MenuStaticStackResource", [Config.Scenes.MenuMain, Config.Scenes.MenuSettings, Config.Scenes.MenuExtras, Config.Scenes.MenuCredits, Config.Scenes.MenuCustomNight], new ImageStackResource(Loaders.LoadMultipleFilenames(Config.ResPath + "Menu/Static", 8)));
    
    Materials.GameNewspaperYooo = registry.RegisterMaterial("GameNewspaperYooo", [Config.Scenes.GameNewspaper], new ImageResource(Config.ResPath + "Game/Newspaper.png"));
    Materials.GameNewspaperYooo.SetFilter(TextureFilter.Bilinear);
    
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
    
    Objects.CustomNightTitle = registry.RegisterObject("CustomNightTitle", [Config.Scenes.MenuCustomNight], [1], new SimpleText(new Vector2(0, 30), new Vector2(Config.WindowWidth, 60), 48, "Custom Night", Color.White, Materials.MenuFont!, true, true));
    
    Objects.LoadingNightText = registry.RegisterObject("LoadingNightText", [Config.Scenes.GameLoading], [1], new SimpleText(new Vector2(0, -40), new Vector2(1024, 768), 48, "Night #", Color.White, Materials.MenuFont!, true, true));
    Objects.LoadingAmText = registry.RegisterObject("LoadingAmText", [Config.Scenes.GameLoading], [1], new SimpleText(new Vector2(0, 40), new Vector2(1024, 768), 48, "12 AM", Color.White, Materials.MenuFont!, true, true));
    
    Objects.GameNewspapers = registry.RegisterObject("GameNewspapers", [Config.Scenes.GameNewspaper], [0], new SimpleImage(Vector2.Zero, Materials.GameNewspaperYooo!, Color.White));
  }
  
  

  public static void CustomInitialisation(Registry registry)
  {
    registry.GetFNaF().GetAnimatronicManager().AddAnimatronic(new Animatronic());
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