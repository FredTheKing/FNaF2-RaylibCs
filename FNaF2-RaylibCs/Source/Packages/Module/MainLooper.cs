using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module;

public static class MainLooper
{
  public static void GlobalActivation(Registry registry)
  {
    if (!registry.GetSceneManager().IsChanged()) return;
    registry.GetSceneManager().ResetChanged();
    registry.GetSceneManager().GetCurrentScene().Activation(registry);
    registry.GetFNaF().Activation(registry);
  }
  
  public static void GlobalUpdate(Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Update(registry);
    registry.GetFNaF().Update(registry);
  }
  
  public static void GlobalDraw(Registry registry)
  {
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    
    registry.GetSceneManager().GetCurrentScene().Draw(registry);
    if (registry.GetDebugMode()) registry.GetGuiManager().Process(registry);
    if (!registry.GetDebugMode()) registry.GetGuiManager().Draw(registry);
    registry.GetFNaF().Draw(registry);
    
    Raylib.EndDrawing();
  }
}