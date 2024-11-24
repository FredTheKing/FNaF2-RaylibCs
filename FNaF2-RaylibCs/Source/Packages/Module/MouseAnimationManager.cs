using ImGuiNET;
using Raylib_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class MouseAnimationManager : CallDebuggerInfoTemplate
{
  private int _max_ibeam_count;
  private int _current_ibeam_count;
  
  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > IBeam Objects Count: {_max_ibeam_count}");
    ImGui.Text($" > IBeam Objects Hovered: {_max_ibeam_count - _current_ibeam_count}");
  }
  
  public void Activation(Registry registry)
  {
    _max_ibeam_count = 0;
    _current_ibeam_count = 0;
  }
  
  public void Update(Registry registry)
  {
    Raylib.SetMouseCursor(_current_ibeam_count != _max_ibeam_count ? MouseCursor.IBeam : MouseCursor.Default);
    _current_ibeam_count = 0;
  }

  public void AddIbeamItem() => _max_ibeam_count++;
  
  public void AddIbeamHover() => _current_ibeam_count++;
}