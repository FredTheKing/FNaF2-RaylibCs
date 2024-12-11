using FNaF2_RaylibCs.Source;
using FNaF2_RaylibCs.Source.Packages.Module;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(Config.WindowConfigFlags);
Raylib.InitWindow(Config.WindowWidth, Config.WindowHeight, Config.WindowTitle);
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(Config.WindowTargetFramerate);
Raylib.SetWindowMinSize(Config.WindowWidth, Config.WindowHeight);
Registry registry = new Registry(
  typeof(Config.Scenes)
  .GetFields()
  .Where(x => x.FieldType == typeof(string))
  .Select(x => (string)x.GetValue(null)!)
  .ToList()
);
Registration.RegistryInitialisation(registry);
Console.WriteLine(Config.SeparatorLine);
Console.WriteLine("INFO: REGISTRY: Registry initialised successfully");
Console.WriteLine(Config.SeparatorLine);
Registration.MaterialsInitialisation(registry);
registry.EndMaterialsRegistration();
Registration.ObjectsInitialisation(registry);
registry.EndObjectsRegistration(Config.StartSceneName);
Registration.CustomInitialisation(registry);
Raylib.SetWindowIcon(Raylib.LoadImage(Config.WindowIconPath));
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