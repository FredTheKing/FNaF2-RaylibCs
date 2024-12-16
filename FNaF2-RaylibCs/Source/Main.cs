using FNaF2_RaylibCs.Source;
using FNaF2_RaylibCs.Source.Packages.Module;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(Config.WindowConfigFlags);
Raylib.InitWindow(Config.WindowWidth, Config.WindowHeight, Config.WindowTitle);
Raylib.InitAudioDevice();
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
Registration.SoundsInitialisation(registry);
Console.WriteLine(Config.SeparatorLine);
Console.WriteLine("INFO: REGISTRY: Sounds initialised successfully");
Console.WriteLine("INFO: REGISTRY: Starting customs initialisation...");
Console.WriteLine(Config.SeparatorLine);
Registration.CustomInitialisation(registry);
Console.WriteLine(Config.SeparatorLine);
Console.WriteLine("INFO: REGISTRY: Customs initialised successfully");
Console.WriteLine("INFO: REGISTRY: Starting objects initialisation...");
Console.WriteLine(Config.SeparatorLine);
Registration.ObjectsInitialisation(registry);
registry.EndObjectsRegistration(registry, Config.StartSceneName);
Raylib.SetWindowIcon(Raylib.LoadImage(Config.WindowIconPath));
Raylib.SetTargetFPS(registry.GetSceneManager().GetVsync() ? Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()) : Config.WindowTargetFramerate);
while (!Raylib.WindowShouldClose())
{
  MainLooper.GlobalActivation(registry);
  MainLooper.GlobalUpdate(registry);
  MainLooper.GlobalDraw(registry);
}
Console.WriteLine(Config.SeparatorLine);
Console.WriteLine("INFO: REGISTRY: Closing game. Have a nice day!");
registry.GetSceneManager().GetCurrentScene().Unload();
Raylib.CloseWindow();
Raylib.CloseAudioDevice();
rlImGui.Shutdown();