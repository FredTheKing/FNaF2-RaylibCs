using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.SceneManager;

public class Scene(string name) : CallDebuggerInfoTemplate
{
  private string _name = name;
  private Dictionary<String, Dictionary<String, dynamic>> _resourcesDictionary = new();
  private Dictionary<int, List<Object>> _unsortedDictObjects = new();
  private List<dynamic> _sortedListObjects = [];
  private List<SoundObject> _listSounds = [];
  private dynamic? _scriptInstance;
  private dynamic? _globalScriptInstance;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Name: {_name}");
    ImGui.Text($" > Objects Count: {_sortedListObjects.Count}");
    ImGui.Text($" > Materials Count: {_resourcesDictionary.SelectMany(x => x.Value).Count()}");
    ImGui.Text($" > Sounds Count: {_listSounds.Count}");
  }
  
  public void AddObject(dynamic obj, int zLayer)
  { 
    if (!_unsortedDictObjects.ContainsKey(zLayer)) _unsortedDictObjects.Add(zLayer, new List<dynamic>());
    _unsortedDictObjects[zLayer].Add(obj);
  }
  
  public void AddSound(dynamic snd)
  { 
    _listSounds.Add(snd);
  }

  public void AssignResources(Dictionary<String, Dictionary<String, dynamic>> resourcesDictionary) => _resourcesDictionary = resourcesDictionary;

  public List<Object> GetObjectsList() => _sortedListObjects;
  
  public List<SoundObject> GetSoundsList() => _listSounds;

  public string GetName() => _name;

  public void AssignScriptInstance(dynamic scriptInstance) => _scriptInstance = scriptInstance;

  public void AssignGlobalScriptInstance(dynamic scriptInstance) => _globalScriptInstance = scriptInstance;

  public void SortLayers()
  {
    _sortedListObjects = _unsortedDictObjects.OrderBy(x => x.Key).SelectMany(x => x.Value).ToList();
    _unsortedDictObjects.Clear();
  }

  public void Unload(Scene? nextScene = null)
  { 
    foreach (KeyValuePair<string,Dictionary<string,dynamic>> typePair in _resourcesDictionary)
      foreach (KeyValuePair<string,dynamic> objectPair in typePair.Value)
        if (nextScene == null || !nextScene._resourcesDictionary.TryGetValue(typePair.Key, out var nextSceneTypePair) || !nextSceneTypePair.ContainsKey(objectPair.Key))
          objectPair.Value.Unload();
  }
  
  public void Load()
  {
    foreach (KeyValuePair<string,Dictionary<string,dynamic>> typePair in _resourcesDictionary)
      foreach (KeyValuePair<string,dynamic> objectPair in typePair.Value)
        if (!objectPair.Value.IsMaterialLoaded())
          objectPair.Value.Load();
  }
  
  public void Deactivation(Registry registry, string nextSceneName)
  {
    foreach (dynamic item in _sortedListObjects)
      item.Deactivation(registry, nextSceneName);
    foreach (SoundObject sound in _listSounds)
      sound.Deactivation(registry, nextSceneName);
    _scriptInstance!.Deactivation(registry, nextSceneName);
    _globalScriptInstance!.Deactivation(registry, nextSceneName);
  }
  
  public void Activation(Registry registry)
  {
    foreach (dynamic item in _sortedListObjects)
      item.Activation(registry);
    foreach (SoundObject sound in _listSounds)
      sound.Activation(registry);
    _scriptInstance!.Activation(registry);
    _globalScriptInstance!.Activation(registry);
  }
  
  public void Update(Registry registry)
  {
    foreach (dynamic item in _sortedListObjects)
      item.Update(registry);
    foreach (SoundObject sound in _listSounds)
      sound.Update(registry);
    _scriptInstance!.Update(registry);
    _globalScriptInstance!.Update(registry);
  }
  
  public void Draw(Registry registry)
  {
    foreach (dynamic item in _sortedListObjects)
      item.Draw(registry);
    _scriptInstance!.Draw(registry);
    _globalScriptInstance!.Draw(registry);
  }
}