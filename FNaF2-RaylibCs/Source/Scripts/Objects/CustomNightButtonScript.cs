using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Objects;

public class CustomNightButtonScript(HitboxTextBorderImage obj) : ScriptTemplate
{
  public override void Activation(Registry registry)
  {
    obj.Text.SetText("0");
  }

  public override void Update(Registry registry)
  {
    int difficulty = int.Parse(obj.Text.GetText());

    if (obj.Hitbox.GetMousePress(MouseButton.Left)) difficulty++;
    if (obj.Hitbox.GetMousePress(MouseButton.Right)) difficulty--;

    if (difficulty < 0) difficulty = 20;
    else if (difficulty > 20) difficulty = 0;

    obj.Text.SetText(difficulty.ToString());
  }
  
  
}