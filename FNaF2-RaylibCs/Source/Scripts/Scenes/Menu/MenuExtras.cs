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
          System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://github.com/FredTheKing/FNaF2-RaylibCs") { UseShellExecute = true });
          break;
        case "Author's Github":
          System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://github.com/FredTheKing") { UseShellExecute = true });
          break;
        case "Back":
          registry.GetSceneManager().ChangeScene(registry, "Menu/Main");
          break;
        default:
          registry.GetSceneManager().ChangeScene(registry, "Menu/" + hitbox.GetText().Replace(" ", ""));
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
      Registration.Objects.ExtrasAuthorLinkGithub!, 
      Registration.Objects.ExtrasCustomNight!,
      Registration.Objects.ExtrasSettings!, 
      Registration.Objects.ExtrasCredits!, 
      Registration.Objects.ExtrasBack!
    ];
    
    TeleportSetToSelected(hitboxes);
    TeleportToScenes(hitboxes, registry);
  }
}