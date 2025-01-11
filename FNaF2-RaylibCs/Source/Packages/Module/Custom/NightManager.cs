using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;

namespace FNaF2_RaylibCs.Source.Packages.Module.Custom;

public class NightManager : ScriptTemplate
{
  public int latest { get; set; } = 1;
  public int current { get; set; }
  public int am { get; set; }

  public SimpleTimer timer { get; } = new(Config.NightSecondsLength, true, false);

  public void NewGameNight()
  {
    latest = 1;
    ContinueNight();
  }
  
  public void ContinueNight() => current = latest;
  
  public void AddLatestNight() => latest++;

  public override void Activation(Registry registry)
  {
    timer.Activation(registry);
  }

  public override void Deactivation(Registry registry, string nextSceneName)
  {
    timer.StopAndResetTimer();
  }

  public override void Update(Registry registry)
  {
    timer.Update(registry);
    am = (int)MathF.Floor((float)timer.GetTime() / (int)MathF.Floor(Config.NightSecondsLength / 6f));
  }
}