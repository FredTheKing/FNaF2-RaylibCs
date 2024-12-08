namespace FNaF2_RaylibCs.Source.Packages.Module;

public static class Loaders
{
  public static List<string> LoadMultipleFilenames(string dirpath, int framesCount) => Enumerable.Range(0, framesCount).Select(i => $"{dirpath}/{i}.png").ToList();
}