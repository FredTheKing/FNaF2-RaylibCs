using FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;

namespace FNaF2_RaylibCs.Source.Packages.Module.Templates;

public abstract class ScriptTemplate : CallDebuggerInfoTemplate
{
  public virtual void Deactivation(Registry registry, string nextSceneName) { }
  public virtual void Activation(Registry registry) { }
  public virtual void Update(Registry registry) { }
  public virtual void Draw(Registry registry) { }
}