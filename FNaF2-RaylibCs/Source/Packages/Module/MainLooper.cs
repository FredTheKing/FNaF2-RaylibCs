using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module;

public static class MainLooper
{
  public static void GlobalActivation(Registry registry)
  {
    if (!registry.scene.Changed) return;
    registry.scene.Changed = false;
    registry.fnaf.Activation(registry);
  }
  
  public static void GlobalUpdate(Registry registry)
  {
    registry.scene.Current?.Update(registry);
    registry.fnaf.Update(registry);
  }
  
  public static void GlobalDraw(Registry registry)
  {
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    
    registry.scene.Current?.Draw(registry);
    if (registry.DebugMode) registry.gui.Process(registry);
    if (!registry.DebugMode) registry.gui.Draw(registry);
    registry.fnaf.Draw(registry);
    
    Raylib.EndDrawing();
  }
}