using System.Numerics;
using ImGuiNET;

namespace RaylibArteSonat.Source.Packages.Module;

public class SceneManager(params String[] scenes_names) : CallDebuggerInfoTemplate
{ 
  private Dictionary<String, Scene> _scenes = InitScenes(scenes_names);
  private String[] _scenes_names = scenes_names;
  private Scene _current_scene;
  private bool _changed = true;

  public new void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode($"Current Scene: {_current_scene.GetName()}"))
    {
      _current_scene.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
    ImGui.Text($" > Changed Scene: {(_changed ? 1 : 0)}");
    ImGui.Text($" > Scenes Count: {_scenes.Count}");
  }
  
  private static Dictionary<String, Scene> InitScenes(params String[] scenes_names)
  {
    var list = new Dictionary<String, Scene>();
    foreach (var scene_name in scenes_names)
    {
        list.Add(scene_name, new Scene(scene_name));
    }
    return list;
  }

  public void SortObjectsLayers()
  {
    foreach (Scene scene in _scenes.Values)
    {
      scene.SortLayers();
    }
  }

  public void AssignScriptInstance(string name, dynamic script_instance) => _scenes[name].AssignScriptInstance(script_instance);

  public void AssignGlobalScriptInstance(dynamic script_instance)
  {
    foreach (Scene scene in _scenes.Values)
    {
      scene.AssignGlobalScriptInstance(script_instance);
    }
  }

  public void LinkObject(Object obj, String scene_name, int z_layer) => _scenes[scene_name].AddObject(obj, z_layer);

  public Dictionary<String, Scene> GetScenes() => _scenes;

  public String[] GetScenesNamesList() => _scenes_names;
  
  public Scene GetCurrentScene() => _current_scene;
  
  public bool IsChanged() => _changed;

  public void ResetChanged() => _changed = false;
  
  public void ChangeScene(String scene_name)
  {
    if (_current_scene != null)
    {
      _current_scene.Unload();
    }
    _current_scene = _scenes[scene_name];
    _changed = true;
    _current_scene.Load();
    Console.WriteLine("INFO: SCENE: Scene changed successfully");
  }
}