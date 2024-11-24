using ImGuiNET;

namespace RaylibArteSonat.Source.Packages.Module;

public class GuiManager : CallDebuggerInfoTemplate
{
  private dynamic _script_instance;
  
  public new void CallDebuggerInfo(Registry registry) => 
    ImGui.Text($" > Script: {_script_instance.GetType().Name + ".cs"}");
  
  public void AssignGuiScript(dynamic script_instance)
  {
    _script_instance = script_instance;
  }
  
  public void Process()
  {
    _script_instance.Process();
  }
  
  public void Draw()
  {
    _script_instance.Draw();
  }
}