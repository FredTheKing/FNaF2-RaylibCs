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
    public const string TestAnimatronic = "Test Animatronic";
    public const string ToyBonnie = "Toy Bonnie";
    public const string ToyFreddy = "Toy Freddy";
    public const string ToyChica = "Toy Chica";
    public const string Mangle = "Mangle";
    public const string WitheredBonnie = "Withered Bonnie";
    public const string WitheredFreddy = "Withered Freddy";
    public const string WitheredChica = "Withered Chica";
    public const string WitheredFoxy = "Withered Foxy";
    public const string BalloonBoy = "Balloon Boy";
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
  public const string StartSceneName = Scenes.GameMain;
  public const string AllScenesShortcut = "*";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow; // concat with '|' sign
  public static readonly string SeparatorLine = new('-', 100);
}