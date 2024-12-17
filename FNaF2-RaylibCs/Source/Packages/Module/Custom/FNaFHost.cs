using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using ImGuiNET;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class FNaFHost : ScriptTemplate
{
  public bool FunMode = false;
  private NightManager _nightManager = new();
  private AnimatronicManager _animatronicManager = new();
  
  public NightManager GetNightManager() => _nightManager;
  public AnimatronicManager GetAnimatronicManager() => _animatronicManager;

  public override void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Fun Mode: {(FunMode ? 1 : 0)}");
    ImGui.Separator();
    _nightManager.CallDebuggerInfo(registry);
    _animatronicManager.CallDebuggerInfo(registry);
  }

  public override void Deactivation(Registry registry, string nextSceneName)
  {
    foreach (Animatronic animatronic in _animatronicManager.GetAnimatronics()) 
      animatronic.Deactivation(registry, nextSceneName);
  }

  public override void Activation(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronicManager.GetAnimatronics()) 
      animatronic.Activation(registry);
  }

  public override void Update(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronicManager.GetAnimatronics()) 
      animatronic.Update(registry);
  }

  public override void Draw(Registry registry)
  {
    foreach (Animatronic animatronic in _animatronicManager.GetAnimatronics()) 
      animatronic.Draw(registry);
  }
}