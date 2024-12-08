using FNaF2_RaylibCs.Source;
using FNaF2_RaylibCs.Source.Packages.Module;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(ProjectConfig.ConfigFlags);
Raylib.InitWindow(1024, 768, "Five Nights at Freddy's 2 - Raylib Edition");
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(-1);
Raylib.SetWindowMinSize(1024, 768);
Registry registry = new Registry(ProjectConfig.ScenesNames);
Registration.RegistryInitialisation(registry);
Registration.MaterialsInitialisation(registry);
registry.EndMaterialsRegistration();
Registration.ObjectsInitialisation(registry);
registry.EndObjectsRegistration(ProjectConfig.StartSceneName);
Registration.CustomInitialisation(registry);
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