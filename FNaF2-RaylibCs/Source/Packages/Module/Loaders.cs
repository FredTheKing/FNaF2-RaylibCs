namespace FNaF2_RaylibCs.Source.Packages.Module;

public static class Loaders
{
  public static List<string> LoadMultipleFilenames(string dirpath, int framesCount, int offset = 0, bool inverted = false)
  {
    var filenames = Enumerable.Range(0, framesCount).Select(i => $"{dirpath}/{i + offset}.png");
    return inverted ? filenames.Reverse().ToList() : filenames.ToList();
  }
  public static List<string> LoadSingleFilenameAsList(string filename) => [filename];
}