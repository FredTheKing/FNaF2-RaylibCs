namespace RaylibArteSonat.Source;
using RaylibArteSonat.Source.Packages.Objects.Animation;
using Raylib_cs;
using Packages.Module;
using Packages.Objects.Box;
using Packages.Objects.Text;
using Packages.Objects.Image;
using Scenes;
using System.Numerics;

public static class Registration
{
  private static string[] scenes_names = ["Debugger/Testing", "Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight", "Game/Main", "Game/Loading", "Game/Newspaper"];
  private static string start_scene_name = "Debugger/Testing";
  
  public static class Materials
  {
    public static FontResource GlobalFont;
    public static AnimationResource TestingAnimation;
  }
  
  public static class Objects
  {
    public static SimpleAnimation TestingAnimation;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.GlobalFont = registry.RegisterMaterial("GlobalFont", ["*"], new FontResource("Resources/Font/consolas.ttf"));
    Materials.TestingAnimation = registry.RegisterMaterial("TestingAnimation", ["Debugger/Testing"],
      new AnimationResource([
        "Resources/TestingAnimation/0.png",
        "Resources/TestingAnimation/1.png",
        "Resources/TestingAnimation/2.png",
        "Resources/TestingAnimation/3.png",
        "Resources/TestingAnimation/4.png",
        "Resources/TestingAnimation/5.png",
        "Resources/TestingAnimation/6.png",
        "Resources/TestingAnimation/7.png",
        "Resources/TestingAnimation/8.png",
        "Resources/TestingAnimation/9.png",
        "Resources/TestingAnimation/10.png",
        "Resources/TestingAnimation/11.png",
        "Resources/TestingAnimation/12.png",
        "Resources/TestingAnimation/13.png",
        "Resources/TestingAnimation/14.png",
        "Resources/TestingAnimation/15.png"
      ]));

    registry.EndMaterialsRegistration();
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.TestingAnimation = registry.RegisterObject("TestingAnimationYooo", ["Debugger/Testing"], [0], new SimpleAnimation(new Vector2(500, 100), 24, Color.White, AnimationPlayMode.Replacement, Materials.TestingAnimation));
    
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
    
    registry.SwitchDebugMode();
    
    return registry;
  }
}