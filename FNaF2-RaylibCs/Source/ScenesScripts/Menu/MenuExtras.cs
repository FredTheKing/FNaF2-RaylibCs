using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.ScenesScripts.Menu;
public class MenuExtras : ScriptTemplate
{
  private void TeleportSetToSelected(List<HitboxText> hitboxes)
  {
    foreach (var hitbox in hitboxes.Where(hitbox => hitbox.GetHitbox().GetMouseHover()))
      Registration.Objects.MenuSet!.SetY(hitbox.GetPosition().Y);
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
          registry.GetSceneManager().ChangeScene("Menu/Main");
          break;
        default:
          registry.GetSceneManager().ChangeScene("Menu/" + hitbox.GetText().Replace(" ", ""));
          break;
      }
  }

  public override void Activation(Registry registry) => 
    Registration.Objects.MenuSet!.SetY(Registration.Objects.ExtrasProjectLinkGithub!.GetPosition().Y);

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