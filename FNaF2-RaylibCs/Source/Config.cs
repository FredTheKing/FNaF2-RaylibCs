using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public struct Config
{
  public record Scenes
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
  
  public const int WindowWidth = 1024, WindowHeight = 768;
  public const string WindowTitle = "Five Nights at Freddy's 2 - Raylib Edition";
  public const string ResPath = "Resources/";
  public const string WindowIconPath = ResPath + "icon.png";
  public const int WindowTargetFramerate = -1;
  
  public const string StartSceneName = Scenes.DebuggerTesting;
  public const string AllScenesShortcut = "*";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow; // concat with '|' sign
  public static readonly string SeparatorLine = new('-', 100);
}