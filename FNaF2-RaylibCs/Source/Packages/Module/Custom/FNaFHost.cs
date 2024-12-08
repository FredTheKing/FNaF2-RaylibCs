using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class FNaFHost : CallDebuggerInfoTemplate
{
  private NightManager _nightManager = new();
  private AnimatronicManager _animatronicManager = new();
  
  public NightManager GetNightManager() => _nightManager;
  public AnimatronicManager GetAnimatronicManager() => _animatronicManager;
}