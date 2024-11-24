using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class Registry(params String[] scenes_names) : CallDebuggerInfoTemplate
{
  private bool _debug_mode = false;
  private bool _show_hitboxes = true;
  private bool _show_bounds = true;
  private bool _show_fps_non_debug = false;
  
  private readonly ShortcutManager _shortcut_manager = new();
  private readonly SceneManager _scene_manager = new(scenes_names);
  private readonly GuiManager _gui_manager = new();
  private readonly ResourcesManager _resources_manager = new(scenes_names);
  private readonly MouseAnimationManager _mouse_animation_manager = new();
  
  private Dictionary<String, Dictionary<String, Object>> _container = new();

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Debug Mode: {(_debug_mode ? 1 : 0)}");
    ImGui.Text($" > Show Hitboxes: {(_show_hitboxes ? 1 : 0)}");
    ImGui.Text($" > Show Bounds: {(_show_bounds ? 1 : 0)}");
    ImGui.Text($" > Show Fps Non Debug: {(_show_fps_non_debug ? 1 : 0)}");
    ImGui.Separator();
    ImGui.Text($" > Total Objects: {_container.SelectMany(x => x.Value).Count()}");
    ImGui.Text($" > Total Materials: {GetResourcesManager().GetStorage().SelectMany(x => x.Value).Count()}");
  }
  
  public dynamic RegisterObject(String name, String[] scenes_names, int[] z_layers, dynamic obj)
  {
    List<string> target_scenes = new();
    
    foreach (string scene_name in scenes_names)
    {
      if (scene_name == "*")
      {
        target_scenes.AddRange(_scene_manager.GetScenes().Keys);
        break;
      }
      else
      {
        target_scenes.Add(scene_name);
      }
    }
    
    foreach (string scene_name in target_scenes)
    {
      if (!_container.ContainsKey(scene_name))
      {
        _container.Add(scene_name, new Dictionary<String, Object>());
      }
      
      Console.WriteLine("INFO: REGISTRY: Object '" + name + "' for scene '" + scene_name + "' loaded successfully");
      _container[scene_name].Add(name, obj);
    }
    
    for(int i = 0; i < target_scenes.Count; i++)
    {
      _scene_manager.LinkObject(obj, target_scenes[i], z_layers[i % z_layers.Length]);
    }
    return obj;
  }
  
  public dynamic RegisterMaterial(String name, String[] scenes_names, dynamic mat)
  {
    List<string> target_scenes = new();
    
    foreach (string scene_name in scenes_names)
    {
      if (scene_name == "*")
      {
        target_scenes.AddRange(_scene_manager.GetScenes().Keys);
        break;
      }
      else
      {
        target_scenes.Add(scene_name);
      }
    }
    
    foreach (string scene_name in target_scenes)
    {
      _resources_manager.AddMaterial(scene_name, name, mat);
      Console.WriteLine("INFO: REGISTRY: Material '" + name + "' for scene '" + scene_name + "' loaded successfully");
    }

    return mat;
  }

  public Dictionary<String, Dictionary<String, Object>> GetContainer() => _container; 
  
  public dynamic Get(String name) => _container[name];

  public void SwitchDebugMode() => _debug_mode = !_debug_mode;

  public void SetShowHitboxes(bool boolean) => _show_hitboxes = boolean;
  
  public bool GetShowHitboxes() => _show_hitboxes;
  
  public void SetShowBounds(bool boolean) => _show_bounds = boolean;
  
  public bool GetShowBounds() => _show_bounds;
  
  public void SetShowFpsNonDebug(bool boolean) => _show_fps_non_debug = boolean;
  
  public bool GetShowFpsNonDebug() => _show_fps_non_debug;
  
  public bool GetDebugMode() => _debug_mode;

  public void EndMaterialsRegistration()
  {
    foreach (KeyValuePair<String, Scene> scene_pair in GetSceneManager().GetScenes())
    {
      if (GetResourcesManager().GetStorage().ContainsKey(scene_pair.Key)) 
        scene_pair.Value.AssignResources(GetResourcesManager().GetStorage()[scene_pair.Key]);
    }
  }
  
  public void EndObjectsRegistration(string start_scene_name)
  {
    _scene_manager.SortObjectsLayers();
    _scene_manager.ChangeScene(start_scene_name);
    rlImGui.Setup(true, true);
  }
  
  public SceneManager GetSceneManager() => _scene_manager;
  
  public GuiManager GetGuiManager() => _gui_manager;

  public ShortcutManager GetShortcutManager() => _shortcut_manager;
  
  public ResourcesManager GetResourcesManager() => _resources_manager;
  
  public MouseAnimationManager GetMouseAnimationManager() => _mouse_animation_manager;
  
  public void AssignSceneScript(string scene_name, dynamic script_instance) => 
    _scene_manager.AssignScriptInstance(scene_name, script_instance);
  
  public void AssignGlobalScript(dynamic script_instance) =>
    _scene_manager.AssignGlobalScriptInstance(script_instance);
  
  public void AssignGuiScript(dynamic script_instance) =>
    _gui_manager.AssignGuiScript(script_instance);
}