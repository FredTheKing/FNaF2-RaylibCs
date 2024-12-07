using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module;

public class GuiManager : CallDebuggerInfoTemplate
{
  private dynamic _script_instance;

  public override void CallDebuggerInfo(Registry registry) =>
    ImGui.Text($" > Script: {_script_instance.GetType().Name + ".cs"}");

  public void AssignGuiScript(dynamic script_instance) => _script_instance = script_instance;
  
  public void Process(Registry registry) => _script_instance.Process(registry);
  
  public void Draw(Registry registry) => _script_instance.Draw(registry);
}