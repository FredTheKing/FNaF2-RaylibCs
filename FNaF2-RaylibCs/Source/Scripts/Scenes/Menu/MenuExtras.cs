using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;
public class MenuExtras : ScriptTemplate
{
  private float _prevSetY;

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
        case "Project's Github":
          Raylib.OpenURL("https://github.com/FredTheKing/FNaF2-RaylibCs");
          break;
        case "Back":
          registry.scene.Change(registry, Config.Scenes.MenuMain);
          break;
        default:
          registry.scene.Change(registry, "Menu" + hitbox.GetText().Replace(" ", ""));
          break;
      }
  }

  public override void Activation(Registry registry)
  {
    Registration.Objects.WhiteBlinko!.GetScript()!.Play();
    float initialY = Registration.Objects.ExtrasProjectLinkGithub!.GetPosition().Y;
    Registration.Objects.MenuSet!.SetY(initialY);
    _prevSetY = initialY;
  }

  public override void Update(Registry registry)
  {
    List<HitboxText> hitboxes = [
      Registration.Objects.ExtrasProjectLinkGithub!, 
      Registration.Objects.ExtrasCustomNight!,
      Registration.Objects.ExtrasSettings!, 
      Registration.Objects.ExtrasCredits!, 
      Registration.Objects.ExtrasBack!
    ];
    
    TeleportSetToSelected(hitboxes);
    TeleportToScenes(hitboxes, registry);
  }
}