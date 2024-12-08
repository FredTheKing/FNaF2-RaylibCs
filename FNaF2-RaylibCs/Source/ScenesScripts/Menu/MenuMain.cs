using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.ScenesScripts.Menu;
public class MenuMain : ScriptTemplate
{
  private RandomTimer _twitchTimer = new(.07f, .144f, true);

  private void TwitchAnimatronics(Registry registry)
  {
    _twitchTimer.Update(registry);
    var twitch = Registration.Objects.MenuBackground!;
    
    if (!_twitchTimer.EndedTrigger()) return;
    double randomValue = new Random().NextDouble();
    var index = randomValue switch { < 0.85 => 0, < 0.9 => 1, < 0.95 => 2, _ => 3 };
    twitch.SetFrame(index);
  }
  private void TeleportSetToSelected(List<HitboxText> hitboxes)
  {
    foreach (HitboxText hitbox in hitboxes)
      if (hitbox.GetHitbox().GetMouseHover())
        Registration.Objects.MenuSet!.SetY(hitbox.GetPosition().Y);
  }
  private void TeleportToScenes(List<HitboxText> hitboxes, Registry registry)
  {
    foreach (HitboxText hitbox in hitboxes)
      if (hitbox.GetHitbox().GetMousePressed(MouseButton.Left))
        switch (hitbox.GetText())
        {
          case "New Game":
            registry.GetFNaF().GetNightManager().NewGameNight();
            registry.GetSceneManager().ChangeScene("Game/Newspaper");
            break;
          case "Continue":
            registry.GetFNaF().GetNightManager().ContinueNight();
            registry.GetSceneManager().ChangeScene("Game/Loading");
            break;
          case "Extras":
            registry.GetSceneManager().ChangeScene("Menu/Extras");
            break;
        }
  }

  private void HighlightContinueNightText()
  {
    if (Math.Abs(Registration.Objects.MenuSet!.GetPosition().Y - 493f) < 0) 
      Registration.Objects.MenuContinueNightText!.SetCurrentFrameColor(Color.LightGray);
  }
  
  public override void Activation(Registry registry)
  {
    _twitchTimer.Activation(registry);
    Registration.Objects.MenuSet!.SetY(428f);
    
    int latestNight = registry.GetFNaF().GetNightManager().GetLatestNight();
    Registration.Objects.MenuContinueNightText!.SetText("Night " + (latestNight <= 5 ? latestNight : 5));
  }
    
  public override void Update(Registry registry)
  {
    List<HitboxText> hitboxes = [
      Registration.Objects.MenuNewGame!, 
      Registration.Objects.MenuContinue!, 
      Registration.Objects.MenuExtras!
    ];
    
    TwitchAnimatronics(registry);
    TeleportSetToSelected(hitboxes);
    TeleportToScenes(hitboxes, registry);
    HighlightContinueNightText();
  }
}