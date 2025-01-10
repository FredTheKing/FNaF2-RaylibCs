using FNaF2_RaylibCs.Source.Packages.Module.Custom;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;
using rlImGui_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module;

public class Registry(List<string> scenesNames) : CallDebuggerInfoTemplate
{
  public bool DebugMode;
  public bool ShowHitboxes = true;
  public bool ShowBounds = true;
  public bool ShowFpsNonDebug;
  public bool MovableDebugger;
  
  public KeybindsManager keybinds { get; } = new();
  public SceneManager.SceneManager scene { get; } = new(scenesNames);
  public GuiManager gui { get; } = new();
  public ResourcesManager.ResourcesManager resources { get; } = new();
  public FNaFHost fnaf { get; } = new();

  public Dictionary<String, Dictionary<String, Object>> objects { get; } = new();
  public Dictionary<String, Dictionary<String, SoundObject>> sounds { get; } = new();

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Debug Mode: {(DebugMode ? 1 : 0)}");
    ImGui.Text($" > Show Hitboxes: {(ShowHitboxes ? 1 : 0)}");
    ImGui.Text($" > Show Bounds: {(ShowBounds ? 1 : 0)}");
    ImGui.Text($" > Show Fps Non Debug: {(ShowFpsNonDebug ? 1 : 0)}");
    ImGui.Text($" > Movable Debugger: {(MovableDebugger ? 1 : 0)}");
    ImGui.Separator();
    ImGui.Text($" > Total Objects: {objects.SelectMany(x => x.Value).Count()}");
    ImGui.Text($" > Total Materials: {resources.GetStorage().SelectMany(x => x.Value).Count()}");
  }

  private List<string> InitTargetScenes(Config.Scenes[] scenesNames)
  {
    List<string> targetScenes = [];
    bool haveStar = scenesNames[0] == Config.Scenes.All;
    
    if (haveStar) targetScenes.AddRange(scene.All.Keys);
    foreach (Config.Scenes sceneName in scenesNames)
    {
      if (sceneName == Config.Scenes.All) continue;
      
      if (haveStar) targetScenes.Remove(sceneName.ToString());
      else targetScenes.Add(sceneName.ToString());
    }

    return targetScenes;
  }

  public dynamic RegisterObject(string name, Config.Scenes[] scenesNames, int[] zLayers, dynamic obj)
  {
    List<string> targetScenes = InitTargetScenes(scenesNames);
    
    foreach (string sceneName in targetScenes)
    {
      if (!objects.ContainsKey(sceneName)) 
        objects.Add(sceneName, new Dictionary<String, dynamic>());
      
      objects[sceneName].Add(name, obj);
    }
    
    for(int i = 0; i < targetScenes.Count; i++)
    {
      scene.LinkObject(obj, targetScenes[i], zLayers[i % zLayers.Length]);
      Console.WriteLine("INFO: REGISTRY: Object '" + name + "' for scene '" + targetScenes[i] + "' instantiated!");
    }
    
    return obj;
  }
  
  public dynamic RegisterMaterial(string name, Config.Scenes[] scenesNames, dynamic mat)
  {
    List<string> targetScenes = InitTargetScenes(scenesNames);
    
    foreach (string sceneName in targetScenes)
    {
      resources.AddMaterial(sceneName, name, mat);
      Console.WriteLine("INFO: REGISTRY: Material '" + name + "' for scene '" + sceneName + "' instantiated!");
    }

    return mat;
  }
  
  public dynamic RegisterSound(String name, Config.Scenes[] scenesNames, SoundObject snd)
  {
    List<string> targetScenes = InitTargetScenes(scenesNames);
    
    foreach (string sceneName in targetScenes)
    {
      if (!sounds.ContainsKey(sceneName)) 
        sounds.Add(sceneName, new Dictionary<String, SoundObject>());
      
      sounds[sceneName].Add(name, snd);
    }
    
    foreach (var sceneName in targetScenes)
    {
      scene.LinkSound(snd, sceneName);
      Console.WriteLine("INFO: REGISTRY: Sound '" + name + "' for scene '" + sceneName + "' instantiated!");
    }

    return snd;
  }

  public void EndMaterialsRegistration()
  {
    foreach (KeyValuePair<String, Scene> scenePair in scene.All)
    {
      if (resources.GetStorage().ContainsKey(scenePair.Key)) 
        scenePair.Value.AssignResources(resources.GetStorage()[scenePair.Key]);
    }
    Console.WriteLine(Config.SeparatorLine);
    Console.WriteLine("INFO: REGISTRY: Materials initialised successfully");
    Console.WriteLine("INFO: REGISTRY: Starting sounds initialisation...");
    Console.WriteLine(Config.SeparatorLine);
  }
  
  public void EndObjectsRegistration(Registry registry, Config.Scenes startSceneName)
  {
    scene.SortObjectsLayers();
    Console.WriteLine(Config.SeparatorLine);
    Console.WriteLine("INFO: REGISTRY: Objects initialised successfully");
    Console.WriteLine("INFO: REGISTRY: All initialisations are complete!");
    scene.Change(registry, startSceneName);
    rlImGui.Setup(true, true);
  }
  
  public void AssignSceneScript(string sceneName, dynamic scriptInstance) => 
    scene.AssignScriptInstance(sceneName, scriptInstance);
  
  public void AssignGlobalScript(dynamic scriptInstance) =>
    scene.AssignGlobalScriptInstance(scriptInstance);
  
  public void AssignGuiScript(dynamic scriptInstance) =>
    gui.AssignGuiScript(scriptInstance);
}