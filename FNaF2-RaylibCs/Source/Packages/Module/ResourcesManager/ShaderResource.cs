using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;

public class ShaderResource : MaterialTemplate
{
  protected string? Filename2;
  public float Time;
  public float Intencity;
  
  public ShaderResource(string filename1, string filename2) { Filename = filename1; Filename2 = filename2; }
  public ShaderResource(Shader shader) { Material = shader; }
  
  public override bool IsMaterialLoaded()
  {
    if (Material is null) return false;
    return Raylib.IsShaderValid(Material);
  }
  
  public override void Unload()
  {
    if (Material is null) return;
    Raylib.UnloadShader(Material);
    Material = null;
  }
  
  public override void Load() => Material = Raylib.LoadShader(Filename, Filename2);
  
  public override void CallDebuggerInfo(Registry registry) => ImGui.Text($" > Loaded: {IsMaterialLoaded()}");
}