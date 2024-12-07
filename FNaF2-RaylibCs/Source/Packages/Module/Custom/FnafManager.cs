using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class FnafManager : CallDebuggerInfoTemplate
{
  private int _latest_night = 1;
  private int _upcoming_night = 1;

  public void NewGameNight()
  {
    _latest_night = 1;
    ContinueNight();
  }
  
  public void ContinueNight() => _upcoming_night = _latest_night;
  
  public int GetLatestNight() => _latest_night;
  
  public int GetUpcomingNight() => _upcoming_night;
}