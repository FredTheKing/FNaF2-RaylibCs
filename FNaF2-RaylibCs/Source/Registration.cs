using System.Numerics;
using FNaF2_RaylibCs.Source.ObjectsScripts;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;
using FNaF2_RaylibCs.Source.Packages.Objects.Checkbox;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.Slider;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.ScenesScripts;
using FNaF2_RaylibCs.Source.ScenesScripts.Menu;
using FNaF2_RaylibCs.Source.ScenesScripts.Debugger;
using FNaF2_RaylibCs.Source.ScenesScripts.Game;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public static class Registration
{
  public struct Materials
  {
    public static FontResource? GlobalFont;
    public static FontResource? MenuFont;

    public static ImageStackResource? MenuBackgroundStackResource;
    public static ImageStackResource? MenuStaticStackResource;
    public static ImageStackResource? MenuWhiteBlinkoStackResource;
  }
  
  public struct Objects
  {
    public static SimpleSlider? TestingSlider;
    
    public static SelectableImage? MenuWhiteBlinko;
    
    public static SelectableImage? MenuBackground;
    public static SimpleAnimation? MenuStatic;
    public static SimpleText? MenuGameName;
    public static SimpleText? MenuSet;
    public static HitboxText? MenuNewGame;
    public static HitboxText? MenuContinue;
    public static SimpleText? MenuContinueNightText;
    public static HitboxText? MenuExtras;
    
    public static HitboxText? ExtrasAuthorLinkGithub;
    public static HitboxText? ExtrasProjectLinkGithub;
    public static HitboxText? ExtrasCustomNight;
    public static HitboxText? ExtrasSettings;
    public static HitboxText? ExtrasCredits;
    public static HitboxText? ExtrasBack;

    public static SimpleCheckbox SettingsFullscreen;
    public static SimpleCheckbox SettingsVsync;
    public static SimpleSlider SettingsVolume;
    public static SimpleCheckbox SettingsFunMode;
    public static SimpleCheckbox SettingsDebugMode;

    public static SimpleText? LoadingNightText;
    public static SimpleText? LoadingAmText;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.GlobalFont = registry.RegisterMaterial("GlobalFont", ["*"], new FontResource("Resources/Font/consolas.ttf", 128));
    Materials.MenuFont = registry.RegisterMaterial("MenuFont", ["Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight", "Game/Loading"], new FontResource("Resources/Font/regular.ttf", 128));
    
    Materials.MenuBackgroundStackResource = registry.RegisterMaterial("MenuBackground", ["Menu/Main"], new ImageStackResource(Loaders.LoadMultipleFilenames("Resources/Menu/Background", 4)));
    Materials.MenuStaticStackResource = registry.RegisterMaterial("MenuStaticStackResource", ["Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight"], new ImageStackResource(Loaders.LoadMultipleFilenames("Resources/Menu/Static", 8)));
    
    Materials.MenuWhiteBlinkoStackResource = registry.RegisterMaterial("GlobalWhiteBlinkoStackResource", ["Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight", "Game/Loading"], new ImageStackResource(Loaders.LoadMultipleFilenames("Resources/Menu/WhiteBlinko", 6)));
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.TestingSlider = registry.RegisterObject("TestingSlider", ["Debugger/Testing"], [0], new SimpleSlider(new Vector2(100, 100), new Vector2(250, 20), Color.White, 2));
    
    Objects.MenuWhiteBlinko = registry.RegisterObject("GlobalWhiteBlinko", ["Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight", "Game/Loading"], [9], new SelectableImage(Vector2.Zero, Materials.MenuWhiteBlinkoStackResource!, Color.White));
    Objects.MenuWhiteBlinko.AssignObjectScript(new WhiteBlinkoScript(Objects.MenuWhiteBlinko));
    
    Objects.MenuBackground = registry.RegisterObject("MenuBackground", ["Menu/Main"], [0], new SelectableImage(Vector2.Zero, Materials.MenuBackgroundStackResource!, Color.White));
    Objects.MenuStatic = registry.RegisterObject("MenuStatic", ["Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight"], [0], new SimpleAnimation(Vector2.Zero, 24, new Color(255, 255, 255, 100), AnimationPlayMode.Replacement, Materials.MenuStaticStackResource!));
    Objects.MenuGameName = registry.RegisterObject("MenuGameName", ["Menu/Main"], [1], new SimpleText(new Vector2(92, 16), Vector2.Zero, 62, "Five\n\n\n\nNights\n\n\n\nAt\n\n\n\nFreddy's\n\n\n\n2", Color.White, Materials.MenuFont!));
    
    // each new line = 65px
    Objects.MenuSet = registry.RegisterObject("MenuSet", ["Menu/Main", "Menu/Extras"], [1], new SimpleText(new Vector2(20, 428), Vector2.Zero, 48, ">>", Color.White, Materials.MenuFont!));
    Objects.MenuNewGame = registry.RegisterObject("MenuNewGame", ["Menu/Main"], [1], new HitboxText(new Vector2(92, 428), new Vector2(250, 65), 48, "New Game", Color.White, Materials.MenuFont!));
    Objects.MenuContinue = registry.RegisterObject("MenuContinue", ["Menu/Main"], [1], new HitboxText(new Vector2(92, 493), new Vector2(250, 65), 48, "Continue", Color.White, Materials.MenuFont!));
    Objects.MenuExtras = registry.RegisterObject("MenuExtras", ["Menu/Main"], [1], new HitboxText(new Vector2(92, 558), new Vector2(190, 65), 48, "Extras", Color.White, Materials.MenuFont!));
    Objects.MenuContinueNightText = registry.RegisterObject("MenuContinueNightText", ["Menu/Main"], [1], new SimpleText(new Vector2(326, 518), Vector2.Zero, 18, "Night #", Color.Blank, Materials.MenuFont!));
    
    Objects.ExtrasProjectLinkGithub = registry.RegisterObject("ExtrasProjectLinkGithub", ["Menu/Extras"], [1], new HitboxText(new Vector2(92, 50), new Vector2(500, 65), 48, "Project's Github", new Color { R = 210, G = 210, B = 255, A = 255 }, Materials.MenuFont!, true));
    Objects.ExtrasAuthorLinkGithub = registry.RegisterObject("ExtrasAuthorLinkGithub", ["Menu/Extras"], [1], new HitboxText(new Vector2(92, 115), new Vector2(500, 65), 48, "Author's Github", new Color { R = 210, G = 210, B = 255, A = 255 }, Materials.MenuFont!, true));
    Objects.ExtrasCustomNight = registry.RegisterObject("ExtrasCustomNight", ["Menu/Extras"], [1], new HitboxText(new Vector2(92, 180), new Vector2(500, 65), 48, "Custom Night", Color.White, Materials.MenuFont!, true));
    Objects.ExtrasSettings = registry.RegisterObject("ExtrasSettings", ["Menu/Extras"], [1], new HitboxText(new Vector2(92, 245), new Vector2(500, 65), 48, "Settings", Color.White, Materials.MenuFont!, true));
    Objects.ExtrasCredits = registry.RegisterObject("ExtrasCredits", ["Menu/Extras"], [1], new HitboxText(new Vector2(92, 310), new Vector2(500, 65), 48, "Credits", Color.White, Materials.MenuFont!, true));
    Objects.ExtrasBack = registry.RegisterObject("ExtrasBack", ["Menu/Extras"], [1], new HitboxText(new Vector2(92, 375), new Vector2(500, 65), 48, "Back", Color.White, Materials.MenuFont!, true));
    
    Objects.LoadingNightText = registry.RegisterObject("LoadingNightText", ["Game/Loading"], [1], new SimpleText(new Vector2(0, -40), new Vector2(1024, 768), 48, "Night #", Color.White, Materials.MenuFont!, true, true));
    Objects.LoadingAmText = registry.RegisterObject("LoadingAmText", ["Game/Loading"], [1], new SimpleText(new Vector2(0, 40), new Vector2(1024, 768), 48, "12 AM", Color.White, Materials.MenuFont!, true, true));
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