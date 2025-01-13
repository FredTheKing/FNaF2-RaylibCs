using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;

public class MenuCustomNight : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.WhiteBlinko!.GetScript()!.Play();
  }

  public override void Update(Registry registry)
  {
    Registration.Objects.CustomNightStartBox!.SetFrame(Registration.Objects.CustomNightStartBox.Hitbox.GetMouseDrag(MouseButton.Left) || Registration.Objects.CustomNightStartBox.Hitbox.GetMouseDrag(MouseButton.Right) ? 1 : 0);
    Registration.Objects.CustomNightPresetBox!.SetFrame(Registration.Objects.CustomNightPresetBox.Hitbox.GetMouseDrag(MouseButton.Left) ? 1 : 0);
      
    if (Registration.Objects.CustomNightStartBox.Hitbox.GetMousePress(MouseButton.Left))
      Registration.Objects.CustomNightStartText!.NextText();
    if (Registration.Objects.CustomNightStartBox.Hitbox.GetMousePress(MouseButton.Right))
      Registration.Objects.CustomNightStartText!.PreviousText();

    if (Registration.Objects.CustomNightPresetBox.Hitbox.GetMousePress(MouseButton.Left))
    {
      registry.fnaf.nightManager.current = 7;
      registry.scene.Change(registry, Config.Scenes.GameLoading);
    }
  }
}