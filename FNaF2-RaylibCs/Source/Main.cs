using FNaF2_RaylibCs.Source;
using FNaF2_RaylibCs.Source.Packages.Module;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.VSyncHint);
Raylib.InitWindow(1024, 768, "Five Nights at Freddy's 2 - Raylib Edition");
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(-1);
Raylib.SetWindowMinSize(1024, 768);
Registry registry = Registration.RegistryInitialisation();
Registration.MaterialsInitialisation(registry);
Registration.ObjectsInitialisation(registry);
Raylib.SetWindowIcon(Raylib.LoadImage("Resources/icon.png"));
while (!Raylib.WindowShouldClose())
{
  MainLooper.GlobalActivation(registry);
  MainLooper.GlobalUpdate(registry);
  MainLooper.GlobalDraw(registry);
}
registry.GetSceneManager().GetCurrentScene().Unload();
Raylib.CloseWindow();
Raylib.CloseAudioDevice();
rlImGui.Shutdown();