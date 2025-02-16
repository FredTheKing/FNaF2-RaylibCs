using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module;

public class KeybindsManager : CallDebuggerInfoTemplate
{
  public char GetCharPressed() => Convert.ToChar(Raylib.GetCharPressed());
  
  public bool IsKeyPressed(KeyboardKey key) => Raylib.IsKeyPressed(key);

  public bool IsKeyDown(KeyboardKey key) =>Raylib.IsKeyDown(key);
}