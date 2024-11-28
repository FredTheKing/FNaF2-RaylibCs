using RaylibArteSonat.Source;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.VSyncHint);
Raylib.InitWindow(1920, 1080, "Five Nights at Freddy's 2 - Raylib Edition");
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(-1);
Raylib.SetWindowMinSize(700, 550);
Registry registry = Registration.RegistryInitialisation();
Registration.MaterialsInitialisation(registry);
Registration.ObjectsInitialisation(registry);
Raylib.SetWindowIcon(Raylib.LoadImage("Resources/icon.jpg"));
while (!Raylib.WindowShouldClose())
{
  MainLooper.GlobalActivation(registry);
  MainLooper.GlobalUpdate(registry);
  MainLooper.GlobalDraw(registry);
}
Raylib.CloseWindow();
Raylib.CloseAudioDevice();
rlImGui.Shutdown();