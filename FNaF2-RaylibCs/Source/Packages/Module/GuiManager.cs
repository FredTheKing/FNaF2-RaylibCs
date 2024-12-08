using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module;

public class GuiManager : CallDebuggerInfoTemplate
{
  private dynamic? _scriptInstance;

  public override void CallDebuggerInfo(Registry registry) =>
    ImGui.Text($" > Script: {_scriptInstance!.GetType().Name + ".cs"}");

  public void AssignGuiScript(dynamic scriptInstance) => _scriptInstance = scriptInstance;
  
  public void Process(Registry registry) => _scriptInstance!.Process(registry);
  
  public void Draw(Registry registry) => _scriptInstance!.Draw(registry);
}