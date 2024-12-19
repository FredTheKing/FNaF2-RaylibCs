using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.SceneManager;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes;

public class ImGuiWindow
{
  private const int DebuggerWidth = 400;
  
  public void Process(Registry registry)
  {
    rlImGui.Begin();
    ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarSize, 0);
    
    if (registry.GetMovableDebugger() ? ImGui.Begin("Debugger", ImGuiWindowFlags.NoCollapse) : ImGui.Begin("Debugger", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize))
    {
      if (!registry.GetMovableDebugger())
      {
        ImGui.SetWindowSize(new Vector2(DebuggerWidth, 768));
        ImGui.SetWindowPos(new Vector2(Raylib.GetScreenWidth() - DebuggerWidth, 0)); 
      }
      ImGui.SeparatorText("Info");
      ImGui.Text("Window size: " + Raylib.GetRenderWidth() + "/" + Raylib.GetRenderHeight());
      ImGui.Text("FPS: " + Raylib.GetFPS());
      ImGui.Text("MS: " + Raylib.GetFrameTime());
      ImGui.Text("Vsync: " + registry.GetSceneManager().GetVsync());
      ImGui.Text("Volume: " + Math.Round(registry.GetSceneManager().GetMasterVolume() * 100) + "%%");
      ImGui.Text("Scene: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 182);
      List<string> array = registry.GetSceneManager().GetScenesNamesList();
      int index = array.IndexOf(registry.GetSceneManager().GetCurrentScene().GetName());
      ImGui.SetNextItemWidth(174);
      if (ImGui.Combo("##Scene Selector", ref index, array.ToArray(), array.Count)) registry.GetSceneManager().ChangeScene(registry, array[index]);
      
      ImGui.Text("Show Hitboxes: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      bool hitboxes = registry.GetShowHitboxes();
      ImGui.Checkbox("##Show Hitboxes", ref hitboxes);
      registry.SetShowHitboxes(hitboxes);
      
      ImGui.Text("Show Bounds: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      bool bounds = registry.GetShowBounds();
      ImGui.Checkbox("##Show Bounds", ref bounds);
      registry.SetShowBounds(bounds);
      
      ImGui.Text("Show Fps Non Debug: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      bool fps = registry.GetShowFpsNonDebug();
      ImGui.Checkbox("##Show Fps Non Debug", ref fps);
      registry.SetShowFpsNonDebug(fps);
      
      ImGui.Text("Movable Debugger: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      bool movdebugger = registry.GetMovableDebugger();
      ImGui.Checkbox("##Movable Debugger", ref movdebugger);
      registry.SetMovableDebugger(movdebugger);
      
      var halfButtonSize = new Vector2(ImGui.GetWindowWidth() / 2 - 12, 19);
      if (ImGui.Button("Enable all", halfButtonSize))
      {
        registry.SetShowHitboxes(true);
        registry.SetShowBounds(true);
        registry.SetShowFpsNonDebug(true);
        registry.SetMovableDebugger(true);
      }
      ImGui.SameLine();
      if (ImGui.Button("Disable all", halfButtonSize))
      {
        registry.SetShowHitboxes(false);
        registry.SetShowBounds(false);
        registry.SetShowFpsNonDebug(false);
        registry.SetMovableDebugger(false);
      }
      
      String currentSceneName = registry.GetSceneManager().GetCurrentScene().GetName();
      Dictionary<String, Dictionary<String, Object>> objects = registry.GetObjects();
      Dictionary<String, Dictionary<String, SoundObject>> sounds = registry.GetSounds();
      Dictionary<String, Dictionary<String, Dictionary<String, Object>>> materials = registry.GetResourcesManager().GetStorage();
      
      // Current scene
      ImGui.SeparatorText("Resources");
      if (ImGui.TreeNode("Current Scene"))
      {
        if (objects.ContainsKey(currentSceneName))
        {
          if (ImGui.TreeNode("Objects"))
          {
            foreach (KeyValuePair<String, dynamic> pair in objects[currentSceneName])
            {
              if (ImGui.TreeNode(pair.Key))
              {
                pair.Value.CallDebuggerInfo(registry);
                ImGui.TreePop();
              }
            }
            ImGui.TreePop();
          }
        }
        if (sounds.ContainsKey(currentSceneName))
        {
          if (ImGui.TreeNode("Sounds"))
          {
            foreach (KeyValuePair<String, SoundObject> pair in sounds[currentSceneName])
            {
              if (ImGui.TreeNode(pair.Key))
              {
                pair.Value.CallDebuggerInfo(registry);
                ImGui.TreePop();
              }
            }
            ImGui.TreePop();
          }
        }
        if (materials.ContainsKey(currentSceneName))
        {
          if (ImGui.TreeNode("Materials"))
          {
            foreach (KeyValuePair<String, Dictionary<String, Object>> pair in materials[currentSceneName])
            {
              if (ImGui.TreeNode(pair.Key))
              {
                foreach (KeyValuePair<String, dynamic> mat in pair.Value)
                {
                  if (ImGui.TreeNode(mat.Key))
                  {
                    mat.Value.CallDebuggerInfo(registry);
                    ImGui.TreePop();
                  }
                }
                ImGui.TreePop();
              }
            }
            ImGui.TreePop();
          }
        }

        ImGui.TreePop();
      } 
      
      // Each other scene
      ImGui.Separator();
      foreach (String sceneName in array)
      {
        if (materials.ContainsKey(sceneName) || objects.ContainsKey(sceneName) || sounds.ContainsKey(sceneName))
        {
          if (ImGui.TreeNode(sceneName))
          {
            if (objects.ContainsKey(sceneName))
            {
              if (ImGui.TreeNode("Objects"))
              {
                foreach (KeyValuePair<String, dynamic> obj in objects[sceneName])
                {
                  if (ImGui.TreeNode(obj.Key))
                  {
                    obj.Value.CallDebuggerInfo(registry);
                    ImGui.TreePop();
                  }
                }
                ImGui.TreePop();
              }
            }
            if (sounds.ContainsKey(sceneName))
            {
              if (ImGui.TreeNode("Sounds"))
              {
                foreach (KeyValuePair<String, SoundObject> snd in sounds[sceneName])
                {
                  if (ImGui.TreeNode(snd.Key))
                  {
                    snd.Value.CallDebuggerInfo(registry);
                    ImGui.TreePop();
                  }
                }
                ImGui.TreePop();
              }
            }
            if (materials.ContainsKey(sceneName))
            {
              if (ImGui.TreeNode("Materials"))
              {
                foreach (KeyValuePair<String, Dictionary<String, Object>> type in materials[sceneName])
                {
                  if (ImGui.TreeNode(type.Key))
                  {
                    foreach (KeyValuePair<String, dynamic> mat in type.Value)
                    {
                      if (ImGui.TreeNode(mat.Key))
                      {
                        mat.Value.CallDebuggerInfo(registry);
                        ImGui.TreePop();
                      }
                    }
                    ImGui.TreePop();
                  }
                }
                ImGui.TreePop();
              }
            }
            ImGui.TreePop();
          }
        }
      }
      
      // Managers
      ImGui.SeparatorText("Managers");
      if (ImGui.TreeNode("Registry"))
      {
        registry.CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("SceneManager"))
      {
        registry.GetSceneManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("ShortcutManager"))
      {
        registry.GetShortcutManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("ResourcesManager"))
      {
        registry.GetResourcesManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("GuiManager"))
      {
        registry.GetGuiManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("FNaF"))
      {
        registry.GetFNaF().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
    }

    ImGui.End();
    rlImGui.End();
  }

  public void Draw(Registry registry)
  {
    if (registry.GetShowFpsNonDebug()) Raylib.DrawFPS(10, 10);
  }
}