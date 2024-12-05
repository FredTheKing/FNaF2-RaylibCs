using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;

namespace FNaF2_RaylibCs.Source.Scenes.Menu;
public class MenuMain(Registry registry) : SceneTemplate
{
  private RandomTimer twitch_timer = new(.07f, .144f, true);

  private void TwitchAnimatronics()
  {
    var twitch = Registration.Objects.MenuBackground;
    
    if (!twitch_timer.EndedTrigger()) return;
    double randomValue = new Random().NextDouble();
    var index = randomValue switch { < 0.85 => 0, < 0.9 => 1, < 0.95 => 2, _ => 3 };
    twitch.SetFrame(index);
  }
  
  public override void Activation()
  {
    twitch_timer.Activation(registry);
  }
    
  public override void Update()
  {
    twitch_timer.Update(registry);
    TwitchAnimatronics();
  }
}