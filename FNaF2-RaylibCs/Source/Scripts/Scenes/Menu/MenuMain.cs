using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using FNaF2_RaylibCs.Source.Packages.Objects.Timer;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;
public class MenuMain : ScriptTemplate
{
  private RandomTimer _twitchTimer = new(.07f, .144f, true);
  private float _prevSetY;

  private void TwitchAnimatronics(Registry registry)
  {
    _twitchTimer.Update(registry);
    var twitch = Registration.Objects.MenuBackground!;
    
    if (!_twitchTimer.TargetTrigger()) return;
    double randomValue = new Random().NextDouble();
    var index = randomValue switch { < 0.85 => 0, < 0.9 => 1, < 0.95 => 2, _ => 3 };
    twitch.SetFrame(index);
  }
  private void TeleportSetToSelected(List<HitboxText> hitboxes)
  {
    foreach (var hitbox in hitboxes.Where(hitbox => hitbox.GetHitbox().GetMouseHover()))
    {
      float newY = hitbox.GetPosition().Y;
      if (Math.Abs(newY - _prevSetY) > 1)
        Registration.Sounds.SetSound!.Play();
      Registration.Objects.MenuSet!.SetY(newY);
      _prevSetY = newY;
    }
  }
  private void TeleportToScenes(List<HitboxText> hitboxes, Registry registry)
  {
    foreach (var hitbox in hitboxes.Where(hitbox => hitbox.GetHitbox().GetMousePress(MouseButton.Left)))
      switch (hitbox.GetText())
      {
        case "New Game":
          registry.fnaf.nightManager.NewGameNight();
          registry.scene.Change(registry, Config.Scenes.GameNewspaper);
          break;
        case "Continue":
          registry.fnaf.nightManager.ContinueNight();
          registry.scene.Change(registry, Config.Scenes.GameLoading);
          break;
        case "Extras":
          registry.scene.Change(registry, Config.Scenes.MenuExtras);
          break;
      }
  }
  private void HighlightContinueNightText()
  {
    if (Math.Abs(Registration.Objects.MenuSet!.GetPosition().Y - 493) < 1) 
      Registration.Objects.MenuContinueNightText!.SetCurrentFrameColor(Color.LightGray);
  }
  
  public override void Activation(Registry registry)
  {
    _twitchTimer.Activation(registry);
    Registration.Objects.WhiteBlinko!.GetScript()!.Play();
    
    float initialY = Registration.Objects.MenuNewGame!.GetPosition().Y;
    Registration.Objects.MenuSet!.SetY(initialY);
    _prevSetY = initialY;
    
    int latestNight = registry.fnaf.nightManager.latest;
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