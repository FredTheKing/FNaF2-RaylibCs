using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class ResourcesManager : CallDebuggerInfoTemplate
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
  
  private void AddFolderAndStuff(String sceneName, String name, dynamic mat, String resourceDirectory)
  {
    if (!_storage.ContainsKey(sceneName)) _storage.Add(sceneName, new Dictionary<String, Dictionary<String, dynamic>>());
    if (!_storage[sceneName].ContainsKey(resourceDirectory)) _storage[sceneName].Add(resourceDirectory, new Dictionary<String, dynamic>());
    _storage[sceneName][resourceDirectory].Add(name, mat);
  }

  public void AddMaterial(String sceneName, String name, FontResource mat) => AddFolderAndStuff(sceneName, name, mat, "Font");
  public void AddMaterial(String sceneName, String name, ImageResource mat) => AddFolderAndStuff(sceneName, name, mat, "Image");
  public void AddMaterial(String sceneName, String name, ImageStackResource mat) => AddFolderAndStuff(sceneName, name, mat, "ImageStack");
  public void AddMaterial(String sceneName, String name, ImageDoubleStackResource mat) => AddFolderAndStuff(sceneName, name, mat, "ImageDoubleStack");
  public void AddMaterial(String sceneName, String name, SoundResource mat) => AddFolderAndStuff(sceneName, name, mat, "Sound");
  public void AddMaterial(String sceneName, String name, ShaderResource mat) => AddFolderAndStuff(sceneName, name, mat, "Shader");
}