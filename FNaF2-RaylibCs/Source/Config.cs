using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public struct Config
{
  public struct Scenes
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
  
  public struct AnimatronicsNames
  {
    public const string NoOne = "UNKNOWN_ANIMATRONIC";
    public const string ToyBonnie = "T Bonnie";
    public const string ToyFreddy = "T Freddy";
    public const string ToyChica = "T Chica";
    public const string Mangle = "Mango";
    public const string BalloonBoy = "BB";
    public const string Marionette = "Marionette";
    public const string WitheredBonnie = "W Bonnie";
    public const string WitheredFreddy = "W Freddy";
    public const string WitheredChica = "W Chica";
    public const string WitheredFoxy = "W Foxy";
    public const string GoldenFreddy = "G Freddy";
    public const string Endo = "Endo";
  }
  
  public const int WindowWidth = 1024, WindowHeight = 768;
  public const double WindowRatioWH = (double)WindowHeight / WindowWidth;
  public const string WindowTitle = "Five Nights at Freddy's 2 - Raylib Edition";
  public const string ResPath = "Resources/";
  public const string WindowIconPath = ResPath + "icon.png";
  public const int WindowTargetFramerate = -1;
  public const bool FullscreenMode = false, VsyncMode = false;
  public const float DefaultMasterVolume = 1f;

  public const int MaxAnimatronicsDifficulty = 20;
  #if DEBUG
  public const string StartSceneName = Scenes.GameMain;
  #else
  public const string StartSceneName = Scenes.MenuMain;
  #endif
  public const string AllShortcut = "*";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow; // concat with '|' sign
  public static readonly string SeparatorLine = new('-', 100);
}