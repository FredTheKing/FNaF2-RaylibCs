using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.ImageSelector;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.Scenes;
using FNaF2_RaylibCs.Source.Scenes.Debugger;
using FNaF2_RaylibCs.Source.Scenes.Game;
using FNaF2_RaylibCs.Source.Scenes.Menu;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public static class Registration
{
  private static string[] scenes_names = ["Debugger/Testing", "Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight", "Game/Main", "Game/Loading", "Game/Newspaper"];
  private static string start_scene_name = "Menu/Main";
  
  public struct Materials
  {
    public static FontResource GlobalFont;
    public static FontResource MenuFont;

    public static ImageStackResource MenuBackgroundStackResource;
    public static ImageStackResource MenuStaticStackResource;
  }
  
  public struct Objects
  {
    public static SelectableImage MenuBackground;
    public static SimpleAnimation MenuStatic;
    public static SimpleText MenuGameName;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.GlobalFont = registry.RegisterMaterial("GlobalFont", ["*"], new FontResource("Resources/Font/consolas.ttf", 128));
    Materials.MenuFont = registry.RegisterMaterial("MenuFont", ["Menu/Main"], new FontResource("Resources/Font/regular.ttf", 128));
    
    Materials.MenuBackgroundStackResource = registry.RegisterMaterial("MenuBackground", ["Menu/Main"], new ImageStackResource(Loaders.LoadMultipleFilenames("Resources/Menu/Background", 4)));
    Materials.MenuStaticStackResource = registry.RegisterMaterial("MenuStaticStackResource", ["Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight"], new ImageStackResource(Loaders.LoadMultipleFilenames("Resources/Menu/Static", 8)));
    
    registry.EndMaterialsRegistration();
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.MenuBackground = registry.RegisterObject("MenuBackground", ["Menu/Main"], [0], new SelectableImage(Vector2.Zero, Materials.MenuBackgroundStackResource, Color.White));
    Objects.MenuStatic = registry.RegisterObject("MenuStatic", ["Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight"], [0], new SimpleAnimation(Vector2.Zero, 24, new Color(255, 255, 255, 100), AnimationPlayMode.Replacement, Materials.MenuStaticStackResource));
    Objects.MenuGameName = registry.RegisterObject("MenuGameName", ["Menu/Main"], [1], new SimpleText(new Vector2(92, 16), Vector2.Zero, 62, "Five\n\n\n\nNights\n\n\n\nAt\n\n\n\nFreddy's\n\n\n\n2", Color.White, Materials.MenuFont));
    
    registry.EndObjectsRegistration(start_scene_name);
  }
  
  public static Registry RegistryInitialisation()
  {
    Registry registry = new Registry(scenes_names);
    
    registry.AssignSceneScript("Debugger/Testing", new DebuggerTesting(registry));
    
    registry.AssignSceneScript("Menu/Main", new MenuMain(registry));
    registry.AssignSceneScript("Menu/Settings", new MenuSettings(registry));
    registry.AssignSceneScript("Menu/Extras", new MenuExtras(registry));
    registry.AssignSceneScript("Menu/Credits", new MenuCredits(registry));
    registry.AssignSceneScript("Menu/CustomNight", new MenuCustomNight(registry));
    
    registry.AssignSceneScript("Game/Main", new GameMain(registry));
    registry.AssignSceneScript("Game/Loading", new GameLoading(registry));
    registry.AssignSceneScript("Game/Newspaper", new GameNewspaper(registry));
    
    registry.AssignGlobalScript(new GlobalOverlay(registry));
    registry.AssignGuiScript(new ImGuiWindow(registry));
    
    #if DEBUG
    registry.SwitchDebugMode();
    #endif
    
    
    return registry;
  }
}