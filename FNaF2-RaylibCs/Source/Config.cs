using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public struct Config
{
  public const int WindowWidth = 1024, WindowHeight = 768;
  public const string WindowTitle = "Five Nights at Freddy's 2 - Raylib Edition";
  public const int WindowTargetFramerate = -1;
  
  public record ScenesNames
  {
    public const string DebuggerTesting = "Debugger/Testing";
    public const string MenuMain = "Menu/Main";
    public const string MenuSettings = "Menu/Settings";
    public const string MenuExtras = "Menu/Extras";
    public const string MenuCredits = "Menu/Credits";
    public const string MenuCustomNight = "Menu/CustomNight";
    public const string GameMain = "Game/Main";
    public const string GameLoading = "Game/Loading";
    public const string GameNewspaper = "Game/Newspaper";
  }
  
  public const string StartSceneName = ScenesNames.MenuExtras;
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow;
  public const string SeparatorLine = "---------------------------------------------------------";
}