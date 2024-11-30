namespace FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;

public abstract class MaterialTemplate : CallDebuggerInfoTemplate
{
  protected String _filename;
  protected dynamic? _material;

  public virtual bool IsMaterialLoaded()
  {
    Console.WriteLine("ERROR: TEMPLATE: Material's 'IsMaterialLoaded' method not overrided in new resource. Returning false");
    return false;
  }
  
  public void Unload() { }
  public void Load() { }
  public String GetFilename() => _filename;
  public dynamic GetMaterial() => _material;
}