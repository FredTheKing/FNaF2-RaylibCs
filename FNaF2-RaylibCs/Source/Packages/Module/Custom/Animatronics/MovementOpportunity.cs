namespace FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;

public struct MovementOpportunity
{
  public Location From;
  public Location To;
  public float Chance;
  
  public MovementOpportunity(Location from, Location to, float chance)
  {
    From = from;
    To = to;
    Chance = chance;
  }
}