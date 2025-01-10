using Raylib_cs;

namespace FNaF2_RaylibCs.Source;

public struct Config
{
  public enum Scenes
  {
    DebuggerTesting,
    MenuMain,
    MenuSettings,
    MenuExtras,
    MenuCredits,
    MenuCustomNight,
    GameMain,
    GameLose,
    GameWin,
    GameLoading,
    GameNewspaper
  }
  
  public struct AnimatronicsNames
  {
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
  public const Scenes StartSceneName = Scenes.GameMain;
  #else
  public const Scenes StartSceneName = Scenes.MenuMain;
  #endif
  public const string AllShortcut = "*";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow; // concat with '|' sign
  public static readonly string SeparatorLine = new('-', 100);
}