namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public class AnimatronicManager
{
  private List<Animatronic> _animatronics = [];
  
  public void AddAnimatronic(Animatronic animatronic) => _animatronics.Add(animatronic);
  public List<Animatronic> GetAnimatronics() => _animatronics;
}