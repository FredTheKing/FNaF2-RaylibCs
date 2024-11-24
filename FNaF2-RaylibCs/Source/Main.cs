using RaylibArteSonat.Source;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow);
Raylib.InitWindow(1024, 760, "Window!");
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(-1);
Raylib.SetWindowMinSize(700, 550);
Registry registry = Registration.RegistryInitialisation();
Registration.MaterialsInitialisation(registry);
Registration.ObjectsInitialisation(registry);
Raylib.SetWindowIcon(Raylib.LoadImage("Resources/app_icon.png"));
while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Black);
  
  MainLooper.GlobalActivation(registry);
  MainLooper.GlobalUpdate(registry);
  MainLooper.GlobalDraw(registry);
  
  Raylib.EndDrawing();
}
Raylib.CloseWindow();
Raylib.CloseAudioDevice();
rlImGui.Shutdown();