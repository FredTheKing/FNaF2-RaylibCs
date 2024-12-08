using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public struct ProjectConfig
{
  public static readonly string[] ScenesNames = ["Debugger/Testing", "Menu/Main", "Menu/Settings", "Menu/Extras", "Menu/Credits", "Menu/CustomNight", "Game/Main", "Game/Loading", "Game/Newspaper"];
  public const string StartSceneName = "Debugger/Testing";
  public const ConfigFlags ConfigFlags = Raylib_cs.ConfigFlags.AlwaysRunWindow;
}