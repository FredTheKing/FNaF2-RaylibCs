using ImGuiNET;
using Raylib_cs;
namespace RaylibArteSonat.Source.Packages.Module;

public class ResourcesManager(params String[] scenes_names) : CallDebuggerInfoTemplate
{
  // {
  //   scene_name:
  //   {
  //     material_type:
  //     {
  //       material_name: material_instance
  //     }
  //   }
  // }
  private Dictionary<String, Dictionary<String, Dictionary<String, dynamic>>> _storage = new();
  
  public Dictionary<String, Dictionary<String, Dictionary<String, dynamic>>> GetStorage() => _storage;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Resources Count: {_storage.Count}");
  }
  
  private void CheckSceneKey(String scene_name)
  {
    if (!_storage.ContainsKey(scene_name)) _storage.Add(scene_name, new Dictionary<String, Dictionary<String, dynamic>>());
  }

  public void AddMaterial(String scene_name, String name, FontResource mat)
  {
    CheckSceneKey(scene_name);
    if (!_storage[scene_name].ContainsKey("Font")) _storage[scene_name].Add("Font", new Dictionary<String, dynamic>());
    _storage[scene_name]["Font"].Add(name, mat);
  }
  
  public void AddMaterial(String scene_name, String name, ImageResource mat)
  {
    CheckSceneKey(scene_name);
    if (!_storage[scene_name].ContainsKey("Image")) _storage[scene_name].Add("Image", new Dictionary<String, dynamic>());
    _storage[scene_name]["Image"].Add(name, mat);
  }
}