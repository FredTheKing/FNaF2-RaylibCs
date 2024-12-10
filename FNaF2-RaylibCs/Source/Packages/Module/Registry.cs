using FNaF2_RaylibCs.Source.Packages.Module.Custom;
using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;
using rlImGui_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module;

public class Registry(List<string> scenesNames) : CallDebuggerInfoTemplate
{
  private bool _debugMode;
  private bool _showHitboxes = true;
  private bool _showBounds = true;
  private bool _showFpsNonDebug;
  private bool _movableDebugger;
  
  private readonly ShortcutManager _shortcutManager = new();
  private readonly SceneManager.SceneManager _sceneManager = new(scenesNames);
  private readonly GuiManager _guiManager = new();
  private readonly ResourcesManager.ResourcesManager _resourcesManager = new();
  private readonly FNaFHost _fnafHost = new();
  
  private Dictionary<String, Dictionary<String, Object>> _container = new();

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Debug Mode: {(_debugMode ? 1 : 0)}");
    ImGui.Text($" > Show Hitboxes: {(_showHitboxes ? 1 : 0)}");
    ImGui.Text($" > Show Bounds: {(_showBounds ? 1 : 0)}");
    ImGui.Text($" > Show Fps Non Debug: {(_showFpsNonDebug ? 1 : 0)}");
    ImGui.Text($" > Movable Debugger: {(_movableDebugger ? 1 : 0)}");
    ImGui.Separator();
    ImGui.Text($" > Total Objects: {_container.SelectMany(x => x.Value).Count()}");
    ImGui.Text($" > Total Materials: {GetResourcesManager().GetStorage().SelectMany(x => x.Value).Count()}");
  }

  public dynamic RegisterObject(String name, String[] scenesNames, int[] zLayers, dynamic obj)
  {
    List<string> targetScenes = new();
    bool haveStar = scenesNames[0] == "*";
    
    if (haveStar) targetScenes.AddRange(_sceneManager.GetScenes().Keys);
    foreach (string sceneName in scenesNames)
    {
      if (sceneName == "*") continue;
      switch (haveStar)
      {
        case true:
          targetScenes.Remove(sceneName);
          break;
        case false:
          targetScenes.Add(sceneName);
          break;
      }
    }
    
    foreach (string sceneName in targetScenes)
    {
      if (!_container.ContainsKey(sceneName)) 
        _container.Add(sceneName, new Dictionary<String, Object>());
      
      Console.WriteLine("INFO: REGISTRY: Object '" + name + "' for scene '" + sceneName + "' loaded successfully");
      _container[sceneName].Add(name, obj);
    }
    
    for(int i = 0; i < targetScenes.Count; i++) 
      _sceneManager.LinkObject(obj, targetScenes[i], zLayers[i % zLayers.Length]);
    
    return obj;
  }
  
  public dynamic RegisterMaterial(String name, String[] scenesNames, dynamic mat)
  {
    List<string> targetScenes = new();
    bool haveStar = scenesNames[0] == "*";
    
    if (haveStar) targetScenes.AddRange(_sceneManager.GetScenes().Keys);
    foreach (string sceneName in scenesNames)
    {
      if (sceneName == "*") continue;
      switch (haveStar)
      {
        case true:
          targetScenes.Remove(sceneName);
          break;
        case false:
          targetScenes.Add(sceneName);
          break;
      }
    }
    
    foreach (string sceneName in targetScenes)
    {
      _resourcesManager.AddMaterial(sceneName, name, mat);
      Console.WriteLine("INFO: REGISTRY: Material '" + name + "' for scene '" + sceneName + "' loaded successfully");
    }

    return mat;
  }

  public Dictionary<String, Dictionary<String, Object>> GetContainer() => _container; 
  
  public dynamic Get(String name) => _container[name];

  public void SwitchDebugMode() => _debugMode = !_debugMode;

  public void SetShowHitboxes(bool boolean) => _showHitboxes = boolean;
  
  public bool GetShowHitboxes() => _showHitboxes;
  
  public void SetShowBounds(bool boolean) => _showBounds = boolean;
  
  public bool GetShowBounds() => _showBounds;
  
  public void SetShowFpsNonDebug(bool boolean) => _showFpsNonDebug = boolean;
  
  public bool GetShowFpsNonDebug() => _showFpsNonDebug;
  
  public bool GetDebugMode() => _debugMode;
  
  public void SetMovableDebugger(bool boolean) => _movableDebugger = boolean;
  
  public bool GetMovableDebugger() => _movableDebugger;

  public void EndMaterialsRegistration()
  {
    foreach (KeyValuePair<String, Scene> scenePair in GetSceneManager().GetScenes())
    {
      if (GetResourcesManager().GetStorage().ContainsKey(scenePair.Key)) 
        scenePair.Value.AssignResources(GetResourcesManager().GetStorage()[scenePair.Key]);
    }
  }
  
  public void EndObjectsRegistration(string startSceneName)
  {
    _sceneManager.SortObjectsLayers();
    _sceneManager.ChangeScene(startSceneName);
    rlImGui.Setup(true, true);
  }
  
  public SceneManager.SceneManager GetSceneManager() => _sceneManager;
  
  public GuiManager GetGuiManager() => _guiManager;

  public ShortcutManager GetShortcutManager() => _shortcutManager;
  
  public ResourcesManager.ResourcesManager GetResourcesManager() => _resourcesManager;
  
  public FNaFHost GetFNaF() => _fnafHost;
  
  public void AssignSceneScript(string sceneName, dynamic scriptInstance) => 
    _sceneManager.AssignScriptInstance(sceneName, scriptInstance);
  
  public void AssignGlobalScript(dynamic scriptInstance) =>
    _sceneManager.AssignGlobalScriptInstance(scriptInstance);
  
  public void AssignGuiScript(dynamic scriptInstance) =>
    _guiManager.AssignGuiScript(scriptInstance);
}