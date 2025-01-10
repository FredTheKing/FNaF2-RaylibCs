using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class NightManager : CallDebuggerInfoTemplate
{
  private int _latestNight = 1;
  private int _upcomingNight;

  public void NewGameNight()
  {
    _latestNight = 1;
    ContinueNight();
  }
  
  public void ContinueNight() => _upcomingNight = _latestNight;
  
  public void AddLatestNight() => _latestNight++;
  
  public void SetLatestNight(int night) => _latestNight = night;
  
  public int GetLatestNight() => _latestNight;
  
  public int GetUpcomingNight() => _upcomingNight;
}