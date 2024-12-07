using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scenes.Menu;
public class MenuMain : ScriptTemplate
{
  private RandomTimer _twitch_timer = new(.07f, .144f, true);

  private void TwitchAnimatronics(Registry registry)
  {
    _twitch_timer.Update(registry);
    var twitch = Registration.Objects.MenuBackground;
    
    if (!_twitch_timer.EndedTrigger()) return;
    double randomValue = new Random().NextDouble();
    var index = randomValue switch { < 0.85 => 0, < 0.9 => 1, < 0.95 => 2, _ => 3 };
    twitch.SetFrame(index);
  }
  private void TeleportSetToSelected(List<HitboxText> hitboxes)
  {
    foreach (HitboxText hitbox in hitboxes)
      if (hitbox.GetHitbox().GetMouseHover())
        Registration.Objects.MenuSet.SetY(hitbox.GetPosition().Y);
  }
  private void TeleportToScenes(List<HitboxText> hitboxes, Registry registry)
  {
    foreach (HitboxText hitbox in hitboxes)
      if (hitbox.GetHitbox().GetMousePressed(MouseButton.Left))
        switch (hitbox.GetText())
        {
          case "New Game":
            registry.GetFNaFManager().NewGameNight();
            registry.GetSceneManager().ChangeScene("Game/Newspaper");
            break;
          case "Continue":
            registry.GetFNaFManager().ContinueNight();
            registry.GetSceneManager().ChangeScene("Game/Loading");
            break;
          case "Extras":
            registry.GetSceneManager().ChangeScene("Menu/Extras");
            break;
        }
  }
  
  public override void Activation(Registry registry)
  {
    _twitch_timer.Activation(registry);
    Registration.Objects.MenuSet.SetY(428f);
  }
    
  public override void Update(Registry registry)
  {
    List<HitboxText> hitboxes = [Registration.Objects.MenuNewGame, Registration.Objects.MenuContinue, Registration.Objects.MenuExtras];
    
    TwitchAnimatronics(registry);
    TeleportSetToSelected(hitboxes);
    TeleportToScenes(hitboxes, registry);
  }
}