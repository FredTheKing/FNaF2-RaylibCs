using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Text;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Objects;

public class BackToExtrasScript(HitboxText obj) : ScriptTemplate
{
  public override void Update(Registry registry)
  {
    if (obj.GetHitbox().GetMousePress(MouseButton.Left)) 
      registry.GetSceneManager().ChangeScene(registry, Config.Scenes.MenuExtras);
  }
}