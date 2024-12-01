using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Objects.Animation;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using FNaF2_RaylibCs.Source.Packages.Objects.ImageSelector;
using FNaF2_RaylibCs.Source.Scenes;
using FNaF2_RaylibCs.Source.Scenes.Debugger;
using FNaF2_RaylibCs.Source.Scenes.Game;
using FNaF2_RaylibCs.Source.Scenes.Menu;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public static class Registration
{
  private static string[] scenes_names = ["Debugger/TestingOne", "Debugger/TestingTwo", "Debugger/TestingThree", "Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight", "Game/Main", "Game/Loading", "Game/Newspaper"];
  private static string start_scene_name = "Debugger/TestingOne";
  
  public struct Materials
  {
    public static FontResource GlobalFont;
    public static ImageStackResource TestingImageStack;
    public static ImageResource TestionImage;
    
    public static ImageStackResource MenuBackgroundStackResource;
  }
  
  public struct Objects
  {
    public static SimpleAnimation TestingAnimation;
    public static SimpleImage TestingImage;

    public static SelectableImage MenuBackground;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.GlobalFont = registry.RegisterMaterial("GlobalFont", ["*", "Debugger/TestingThree"], new FontResource("Resources/Font/consolas.ttf"));
    Materials.TestingImageStack = registry.RegisterMaterial("TestingAnimation", ["Debugger/TestingOne", "Debugger/TestingTwo"], new ImageStackResource(Loaders.LoadMultipleFilenames("Resources/TestingAnimation", 16)));
    Materials.TestionImage = registry.RegisterMaterial("TestImage", ["Debugger/TestingOne"], new ImageResource("Resources/TestingAnimation/avatar.png"));
    
    Materials.MenuBackgroundStackResource = registry.RegisterMaterial("MenuBackground", ["Menu/Main"], new ImageStackResource(Loaders.LoadMultipleFilenames("Resources/Menu/Background", 4)));
    
    registry.EndMaterialsRegistration();
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.TestingAnimation = registry.RegisterObject("TestingAnimationYooo", ["Debugger/TestingOne", "Debugger/TestingTwo"], [0], new SimpleAnimation(Vector2.Zero, 24, Color.White, AnimationPlayMode.Replacement, Materials.TestingImageStack));
    Objects.TestingImage = registry.RegisterObject("TestingImage", ["Debugger/TestingOne"], [-1], new SimpleImage(Vector2.Zero, Materials.TestionImage, Color.White, Materials.TestingImageStack.GetSize()));
    
    Objects.MenuBackground = registry.RegisterObject("MenuBackground", ["Menu/Main"], [0], new SelectableImage(Vector2.Zero, Materials.MenuBackgroundStackResource, Color.White));
    registry.EndObjectsRegistration(start_scene_name);
  }
  
  public static Registry RegistryInitialisation()
  {
    Registry registry = new Registry(scenes_names);
    
    registry.AssignSceneScript("Debugger/TestingOne", new DebuggerTestingOne(registry));
    registry.AssignSceneScript("Debugger/TestingTwo", new DebuggerTestingTwo(registry));
    registry.AssignSceneScript("Debugger/TestingThree", new DebuggerTestingThree(registry));
    
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