using RaylibArteSonat.Source.Packages.Objects.Timer;
using static RaylibArteSonat.Source.Registration.Objects;

namespace RaylibArteSonat.Source;
using Raylib_cs;
using Packages.Module;
using Packages.Objects.Box;
using Packages.Objects.Text;
using Packages.Objects.Image;
using Scenes;
using System.Numerics;

public static class Registration
{
  private static string[] scenes_names = ["Debugger/Testing", "Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight"];
  private static string start_scene_name = "Debugger/Testing";
  
  public static class Materials
  {
    public static FontResource Global_Font;
  }
  
  public static class Objects
  {
    public static SimpleBox testing_box;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.Global_Font = registry.RegisterMaterial("GlobalFont", ["*"], new FontResource("Resources/Font/regular.ttf"));

    registry.EndMaterialsRegistration();
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.testing_box = registry.RegisterObject("TestingBoxYooo", ["Debugger/Testing"], [0], new SimpleBox(new Vector2(100, 100), new Vector2(50, 50), new Color(255, 255, 255, 255)));
    
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
    
    registry.AssignGlobalScript(new GlobalOverlay(registry));
    registry.AssignGuiScript(new DebuggerWindow(registry));
    
    registry.SwitchDebugMode();
    
    return registry;
  }
}