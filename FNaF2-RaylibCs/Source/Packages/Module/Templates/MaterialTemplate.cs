using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;

namespace FNaF2_RaylibCs.Source.Packages.Module.Templates;

public abstract class MaterialTemplate : CallDebuggerInfoTemplate
{
  protected String? Filename;
  protected dynamic? Material;

  public virtual bool IsMaterialLoaded()
  {
    Console.WriteLine("ERROR: TEMPLATE: Material's 'IsMaterialLoaded' method not overrided in new resource. Returning false");
    return false;
  }
  
  public virtual void Unload() { }
  public virtual void Load() { }
  public String GetFilename() => Filename!;
  public dynamic GetMaterial() => Material!;
}