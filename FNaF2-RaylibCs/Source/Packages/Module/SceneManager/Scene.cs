using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.SceneManager;

public class Scene(string name) : CallDebuggerInfoTemplate
{
  private string _name = name;
  private Dictionary<String, Dictionary<String, dynamic>> _resources_dictionary = new();
  private Dictionary<int, List<Object>> _unsorted_dict_objects = new();
  private List<Object> _sorted_list_objects = new();
  private dynamic _script_instance;
  private dynamic _global_script_instance;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Name: {_name}");
    ImGui.Text($" > Objects Count: {_sorted_list_objects.Count()}");
    ImGui.Text($" > Materials Count: {_resources_dictionary.SelectMany(x => x.Value).Count()}");
  }
  
  public void AddObject(Object obj, int z_layer)
  { 
    if (!_unsorted_dict_objects.ContainsKey(z_layer)) _unsorted_dict_objects.Add(z_layer, new List<object>());
    _unsorted_dict_objects[z_layer].Add(obj);
  }

  public void AssignResources(Dictionary<String, Dictionary<String, dynamic>> resources_dictionary) => _resources_dictionary = resources_dictionary;

  public List<Object> GetObjectsList() => _sorted_list_objects;

  public string GetName() => _name;

  public void AssignScriptInstance(dynamic script_instance) => _script_instance = script_instance;

  public void AssignGlobalScriptInstance(dynamic script_instance) => _global_script_instance = script_instance;

  public void SortLayers()
  {
    _sorted_list_objects = _unsorted_dict_objects.OrderBy(x => x.Key).SelectMany(x => x.Value).ToList();
    _unsorted_dict_objects.Clear();
  }

  public void Unload(Scene? next_scene = null)
  { 
    foreach (KeyValuePair<string,Dictionary<string,dynamic>> type_pair in _resources_dictionary)
      foreach (KeyValuePair<string,dynamic> object_pair in type_pair.Value)
        if (next_scene == null || (!next_scene._resources_dictionary.TryGetValue(type_pair.Key, out var next_scene_type_pair) || !next_scene_type_pair.ContainsKey(object_pair.Key)))
          object_pair.Value.Unload();

    foreach (dynamic item in _sorted_list_objects) 
      item.Unload();
  }
  
  public void Load()
  {
    foreach (KeyValuePair<string,Dictionary<string,dynamic>> type_pair in _resources_dictionary)
      foreach (KeyValuePair<string,dynamic> object_pair in type_pair.Value)
        if (!object_pair.Value.IsMaterialLoaded())
          object_pair.Value.Load();

    foreach (dynamic item in _sorted_list_objects)
      item.Load();
  }
  
  public void Activation(Registry registry)
  {
    foreach (dynamic item in _sorted_list_objects)
      item.Activation(registry);
    _script_instance.Activation(registry);
    _global_script_instance.Activation(registry);
  }
  
  public void Update(Registry registry)
  {
    foreach (dynamic item in _sorted_list_objects)
      item.Update(registry);
    _script_instance.Update(registry);
    _global_script_instance.Update(registry);
  }
  
  public void Draw(Registry registry)
  {
    foreach (dynamic item in _sorted_list_objects)
      item.Draw(registry);
    _script_instance.Draw(registry);
    _global_script_instance.Draw(registry);
  }
}