using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;

public class MenuCredits : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    Registration.Objects.WhiteBlinko!.GetScript()!.Play();
  }

  public override void Update(Registry registry)
  {
    if (Registration.Objects.CreditsRaylibLogo!.Hitbox.GetMousePress(MouseButton.Left))
      Raylib.OpenURL("https://github.com/raylib-cs/raylib-cs");
    if (Registration.Objects.CreditsScottLogo!.Hitbox.GetMousePress(MouseButton.Left))
      Raylib.OpenURL("https://www.youtube.com/channel/UC2Xp5JeeO9sP6bhc-9yD1xA");
    if (Registration.Objects.CreditsMyLogo!.Hitbox.GetMousePress(MouseButton.Left))
      Raylib.OpenURL("https://github.com/FredTheKing");
  }
}