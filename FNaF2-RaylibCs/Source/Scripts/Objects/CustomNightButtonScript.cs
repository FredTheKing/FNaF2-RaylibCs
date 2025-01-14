using System.Drawing;
using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using Raylib_cs;
using Color = Raylib_cs.Color;

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

    if (obj.Hitbox.GetMousePress(MouseButton.Left))
    {
      Registration.Sounds.SetSound!.Play();
      difficulty++;
    }
    if (obj.Hitbox.GetMousePress(MouseButton.Right))
    {
      Registration.Sounds.SetSound!.Play();
      difficulty--;
    }

    if (difficulty == 0)
    {
      obj.Border.SetColor(Color.Gray);
      obj.Gray.SetPosition(obj.GetPosition());
      obj.Gray.SetSize(obj.Resource.GetSize());
      obj.Gray.Roundness = 0;
    }
    else
    {
      obj.Border.SetColor(Color.White);
      obj.Gray.SetPosition(obj.GetPosition() + new Vector2(obj.Resource.GetSize().X - 60, obj.Resource.GetSize().Y - 40));
      obj.Gray.SetSize(new Vector2(60, 40));
      obj.Gray.Roundness = 0.4f;
    }

    if (difficulty < 0) difficulty = 20;
    else if (difficulty > 20) difficulty = 0;

    obj.Text.SetText(difficulty.ToString());
  }
  
  
}