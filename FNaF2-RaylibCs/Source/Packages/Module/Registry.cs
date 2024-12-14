using FNaF2_RaylibCs.Source.Packages.Module.Custom;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
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
  
  private Dictionary<String, Dictionary<String, Object>> _objects = new();
  private Dictionary<String, Dictionary<String, SoundObject>> _sounds = new();

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Debug Mode: {(_debugMode ? 1 : 0)}");
    ImGui.Text($" > Show Hitboxes: {(_showHitboxes ? 1 : 0)}");
    ImGui.Text($" > Show Bounds: {(_showBounds ? 1 : 0)}");
    ImGui.Text($" > Show Fps Non Debug: {(_showFpsNonDebug ? 1 : 0)}");
    ImGui.Text($" > Movable Debugger: {(_movableDebugger ? 1 : 0)}");
    ImGui.Separator();
    ImGui.Text($" > Total Objects: {_objects.SelectMany(x => x.Value).Count()}");
    ImGui.Text($" > Total Materials: {GetResourcesManager().GetStorage().SelectMany(x => x.Value).Count()}");
  }

  public dynamic RegisterObject(string name, string[] scenesNames, int[] zLayers, dynamic obj)
  {
    List<string> targetScenes = new();
    bool haveStar = scenesNames[0] == Config.AllScenesShortcut;
    
    if (haveStar) targetScenes.AddRange(_sceneManager.GetScenes().Keys);
    foreach (string sceneName in scenesNames)
    {
      if (sceneName == Config.AllScenesShortcut) continue;
      
      if (haveStar) targetScenes.Remove(sceneName);
      else targetScenes.Add(sceneName);
    }
    
    foreach (string sceneName in targetScenes)
    {
      if (!_objects.ContainsKey(sceneName)) 
        _objects.Add(sceneName, new Dictionary<String, dynamic>());
      
      _objects[sceneName].Add(name, obj);
    }
    
    for(int i = 0; i < targetScenes.Count; i++)
    {
      _sceneManager.LinkObject(obj, targetScenes[i], zLayers[i % zLayers.Length]);
      Console.WriteLine("INFO: REGISTRY: Object '" + name + "' for scene '" + targetScenes[i] + "' instantiated!");
    }
    
    return obj;
  }
  
  public dynamic RegisterMaterial(string name, string[] scenesNames, dynamic mat)
  {
    List<string> targetScenes = new();
    bool haveStar = scenesNames[0] == Config.AllScenesShortcut;
    
    if (haveStar) targetScenes.AddRange(_sceneManager.GetScenes().Keys);
    foreach (string sceneName in scenesNames)
    {
      if (sceneName == Config.AllScenesShortcut) continue;
      
      if (haveStar) targetScenes.Remove(sceneName);
      else targetScenes.Add(sceneName);
    }
    
    foreach (string sceneName in targetScenes)
    {
      _resourcesManager.AddMaterial(sceneName, name, mat);
      Console.WriteLine("INFO: REGISTRY: Material '" + name + "' for scene '" + sceneName + "' instantiated!");
    }

    return mat;
  }
  
  public dynamic RegisterSound(String name, string[] scenesNames, SoundObject snd)
  {
    List<string> targetScenes = new();
    bool haveStar = scenesNames[0] == Config.AllScenesShortcut;
    
    if (haveStar) targetScenes.AddRange(_sceneManager.GetScenes().Keys);
    foreach (string sceneName in scenesNames)
    {
      if (sceneName == Config.AllScenesShortcut) continue;
      
      if (haveStar) targetScenes.Remove(sceneName);
      else targetScenes.Add(sceneName);
    }
    
    foreach (string sceneName in targetScenes)
    {
      if (!_sounds.ContainsKey(sceneName)) 
        _sounds.Add(sceneName, new Dictionary<String, SoundObject>());
      
      _sounds[sceneName].Add(name, snd);
    }
    
    foreach (var sceneName in targetScenes)
    {
      _sceneManager.LinkSound(snd, sceneName);
      Console.WriteLine("INFO: REGISTRY: Sound '" + name + "' for scene '" + sceneName + "' instantiated!");
    }

    return snd;
  }

  public Dictionary<String, Dictionary<String, dynamic>> GetObjects() => _objects; 
  public Dictionary<String, Dictionary<String, SoundObject>> GetSounds() => _sounds; 
  
  public dynamic Get(String name) => _objects[name];

  public void SwitchDebugMode() => _debugMode = !_debugMode;
  
  public void SetDebugMode(bool state) => _debugMode = state;

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
    Console.WriteLine(Config.SeparatorLine);
    Console.WriteLine("INFO: REGISTRY: Materials initialised successfully");
    Console.WriteLine("INFO: REGISTRY: Starting sounds initialisation...");
    Console.WriteLine(Config.SeparatorLine);
  }
  
  public void EndObjectsRegistration(Registry registry, string startSceneName)
  {
    _sceneManager.SortObjectsLayers();
    Console.WriteLine(Config.SeparatorLine);
    Console.WriteLine("INFO: REGISTRY: Objects initialised successfully");
    Console.WriteLine("INFO: REGISTRY: All initialisations are complete!");
    _sceneManager.ChangeScene(registry, startSceneName);
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