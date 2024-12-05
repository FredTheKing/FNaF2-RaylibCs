using System.Numerics;

namespace FNaF2_RaylibCs.Source.Packages.Module.Templates.RawTemplates;

public abstract class ObjectTemplate : CallDebuggerInfoTemplate
{
  protected Vector2 _position;
  protected Vector2 _size;
  protected dynamic? _script_instance = null;
  
  protected ObjectTemplate() { _position = Vector2.Zero; _size = Vector2.Zero; }
  protected ObjectTemplate(Vector2 position, Vector2 size) { _position = position; _size = size; }
  
  public virtual void Unload() { }
  public virtual void Load() { }
  
  public virtual void Activation(Registry registry) { if (_script_instance != null) _script_instance.Activation(registry); }
  public virtual void Update(Registry registry) { if (_script_instance != null) _script_instance.Update(registry); }
  public virtual void Draw(Registry registry) { if (_script_instance != null) _script_instance.Draw(registry); }
  public void AssignObjectScript(dynamic? script_instance) => _script_instance = script_instance;
  
  public void SetPosition(Vector2 position) => _position = position;
  
  public Vector2 GetPosition() => _position;
  
  public void SetSize(Vector2 size) => _size = size;
  
  public Vector2 GetSize() => _size;
}