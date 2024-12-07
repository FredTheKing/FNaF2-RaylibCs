using System.Numerics;

namespace FNaF2_RaylibCs.Source.Packages.Module.Templates.Raw;

public abstract class ObjectTemplate : ScriptTemplate
{
  protected Vector2 _position;
  protected Vector2 _size;
  protected dynamic? _script_instance = null;
  
  protected ObjectTemplate() { _position = Vector2.Zero; _size = Vector2.Zero; }
  protected ObjectTemplate(Vector2 position, Vector2 size) { _position = position; _size = size; }
  
  public virtual void Unload() { }
  public virtual void Load() { }
  
  public override void Activation(Registry registry) { if (_script_instance != null) _script_instance.Activation(registry); }
  public override void Update(Registry registry) { if (_script_instance != null) _script_instance.Update(registry); }
  public override void Draw(Registry registry) { if (_script_instance != null) _script_instance.Draw(registry); }
  public void AssignObjectScript(dynamic? script_instance) => _script_instance = script_instance;
  
  public virtual void SetPosition(Vector2 position) => _position = position;
  
  public virtual void SetX(float x) => _position.X = x;
  
  public virtual void SetY(float y) => _position.Y = y;
  
  public Vector2 GetPosition() => _position;
  
  public void SetSize(Vector2 size) => _size = size;
  
  public Vector2 GetSize() => _size;
}