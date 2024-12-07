using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

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

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Resources Count: {_storage.Count}");
  }
  
  private void AddFolderAndStuff(String scene_name, String name, dynamic mat, String resource_directory)
  {
    if (!_storage.ContainsKey(scene_name)) _storage.Add(scene_name, new Dictionary<String, Dictionary<String, dynamic>>());
    if (!_storage[scene_name].ContainsKey(resource_directory)) _storage[scene_name].Add(resource_directory, new Dictionary<String, dynamic>());
    _storage[scene_name][resource_directory].Add(name, mat);
  }

  public void AddMaterial(String scene_name, String name, FontResource mat) => AddFolderAndStuff(scene_name, name, mat, "Font");
  public void AddMaterial(String scene_name, String name, ImageResource mat) => AddFolderAndStuff(scene_name, name, mat, "Image");
  public void AddMaterial(String scene_name, String name, ImageStackResource mat) => AddFolderAndStuff(scene_name, name, mat, "ImageStack");
}