namespace RaylibArteSonat.Source.Packages.Module;

public static class MainLooper
{
  public static void GlobalActivation(Registry registry)
  {
    if (!registry.GetSceneManager().IsChanged()) return;
    registry.GetSceneManager().ResetChanged();
    registry.GetMouseAnimationManager().Activation(registry);
    registry.GetSceneManager().GetCurrentScene().Activation(registry);
  }
  
  public static void GlobalUpdate(Registry registry)
  {
    registry.GetMouseAnimationManager().Update(registry);
    registry.GetSceneManager().GetCurrentScene().Update(registry);
  }
  
  public static void GlobalDraw(Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Draw(registry);
    if (registry.GetDebugMode()) registry.GetGuiManager().Process();
    if (!registry.GetDebugMode()) registry.GetGuiManager().Draw();
  }
}