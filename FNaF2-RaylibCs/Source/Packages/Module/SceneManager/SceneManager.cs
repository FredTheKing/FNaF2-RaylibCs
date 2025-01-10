using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.SceneManager;

public class SceneManager(List<string> names) : CallDebuggerInfoTemplate
{ 
  public Dictionary<String, Scene> All = names.ToDictionary(sceneName => sceneName, sceneName => new Scene(sceneName));
  public List<string> Names = names;
  public Scene? Current;
  public bool Changed = true;
  public float MasterVolume = Config.DefaultMasterVolume;
  public bool Fullscreen = Config.FullscreenMode;
  public bool Vsync = Config.VsyncMode;

  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode($"Current Scene: {Current!.GetName()}"))
    {
      Current.CallDebuggerInfo(registry);
      ImGui.TreePop();
    }
    ImGui.Text($" > Changed Scene: {(Changed ? 1 : 0)}");
    ImGui.Text($" > Scenes Count: {All.Count}");
    ImGui.Separator();
    ImGui.Text($" > Fullscreen: {Fullscreen}");
    ImGui.Text($" > Vsync: {Vsync}");
    ImGui.Text($" > Master Volume: {MasterVolume}");
  }

  public void SortObjectsLayers()
  {
    foreach (Scene scene in All.Values) 
      scene.SortLayers();
  }

  public void AssignScriptInstance(string name, dynamic scriptInstance) => All[name].AssignScriptInstance(scriptInstance);

  public void AssignGlobalScriptInstance(dynamic scriptInstance)
  {
    foreach (Scene scene in All.Values) 
      scene.AssignGlobalScriptInstance(scriptInstance);
  }

  public void LinkObject(dynamic obj, string sceneName, int zLayer) => All[sceneName].AddObject(obj, zLayer);
  
  public void LinkSound(SoundObject snd, string sceneName) => All[sceneName].AddSound(snd);

  private void ActualChange(Registry registry, string sceneName)
  {
    Current?.Deactivation(registry, sceneName);
    Console.WriteLine("INFO: SCENE: Changing scene from '" + Current?.GetName() + "' to '" + sceneName + "'...");
    Console.WriteLine(Config.SeparatorLine);
    Scene newScene = All[sceneName];
    Current?.Unload(newScene);
    Current = newScene;
    Changed = true;
    Current.Load();
    Console.WriteLine("INFO: SCENE: Scene changed successfully");
  }
  
  public void Change(Registry registry, Config.Scenes sceneEnum) => ActualChange(registry, sceneEnum.ToString());
  public void Change(Registry registry, int sceneId) => ActualChange(registry, ((Config.Scenes)sceneId).ToString());
  public void Change(Registry registry, string sceneName) => ActualChange(registry, sceneName);

  public void Next(Registry registry) =>
    Change(registry, Names[(Names.ToList().IndexOf(Current!.GetName()) + 1) % Names.Count]);
  
  public void Previous(Registry registry) =>
    Change(registry, Names[(Names.ToList().IndexOf(Current!.GetName()) + Names.Count - 1) % Names.Count]);
}