namespace FNaF2_RaylibCs.Source.Packages.Module;

public static class Loaders
{
  public static List<string> LoadMultipleFilenames(string path, int frames_count) => Enumerable.Range(0, frames_count).Select(i => $"{path}/{i}.png").ToList();
}