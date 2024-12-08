using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.SceneManager;

public class SceneManager(params String[] scenesNames) : CallDebuggerInfoTemplate
{ 
  private Dictionary<String, Scene> _scenes = InitScenes(scenesNames);
  private String[] _scenesNames = scenesNames;
  private Scene? _currentScene;
  private bool _changed = true;

  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode($"Current Scene: {_currentScene!.GetName()}"))
    {
      _currentScene.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
    ImGui.Text($" > Changed Scene: {(_changed ? 1 : 0)}");
    ImGui.Text($" > Scenes Count: {_scenes.Count}");
  }
  
  private static Dictionary<String, Scene> InitScenes(params String[] scenesNames) =>
    scenesNames.ToDictionary(sceneName => sceneName, sceneName => new Scene(sceneName));

  public void SortObjectsLayers()
  {
    foreach (Scene scene in _scenes.Values) 
      scene.SortLayers();
  }

  public void AssignScriptInstance(string name, dynamic scriptInstance) => _scenes[name].AssignScriptInstance(scriptInstance);

  public void AssignGlobalScriptInstance(dynamic scriptInstance)
  {
    foreach (Scene scene in _scenes.Values) 
      scene.AssignGlobalScriptInstance(scriptInstance);
  }

  public void LinkObject(Object obj, String sceneName, int zLayer) => _scenes[sceneName].AddObject(obj, zLayer);

  public Dictionary<String, Scene> GetScenes() => _scenes;

  public String[] GetScenesNamesList() => _scenesNames;
  
  public Scene GetCurrentScene() => _currentScene!;
  
  public bool IsChanged() => _changed;

  public void ResetChanged() => _changed = false;
  
  public void ChangeScene(String sceneName)
  {
    Console.WriteLine("-----------------------------------------");
    Scene newScene = _scenes[sceneName];
    _currentScene?.Unload(newScene);
    _currentScene = newScene;
    _changed = true;
    _currentScene.Load();
    Console.WriteLine("INFO: SCENE: Scene changed successfully");
  }

  public void NextScene() =>
    ChangeScene(_scenesNames[(_scenesNames.ToList().IndexOf(_currentScene!.GetName()) + 1) % _scenesNames.Length]);
  
  public void PreviousScene() =>
    ChangeScene(_scenesNames[(_scenesNames.ToList().IndexOf(_currentScene!.GetName()) + _scenesNames.Length - 1) % _scenesNames.Length]);
}