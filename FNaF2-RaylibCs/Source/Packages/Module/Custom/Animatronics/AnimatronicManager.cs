using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public class AnimatronicManager : CallDebuggerInfoTemplate
{
  private List<Animatronic> _animatronics = [];
  
  public void Add(Animatronic animatronic) => _animatronics.Add(animatronic);
  public List<Animatronic> GetAnimatronics() => _animatronics;
}