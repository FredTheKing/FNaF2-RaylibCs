using System.Numerics;

namespace FNaF2_RaylibCs.Source.Packages.Module.Templates;

public abstract class ObjectTemplate : ScriptTemplate
{
  protected Vector2 Position;
  protected Vector2 Size;
  protected dynamic? ScriptInstance;
  
  protected ObjectTemplate() { Position = Vector2.Zero; Size = Vector2.Zero; }
  protected ObjectTemplate(Vector2 position, Vector2 size) { Position = position; Size = size; }
  
  public override void Deactivation(Registry registry, string nextSceneName) { if (ScriptInstance != null) ScriptInstance.Deactivation(registry, nextSceneName); }
  public override void Activation(Registry registry) { if (ScriptInstance != null) ScriptInstance.Activation(registry); }
  public override void Update(Registry registry) { if (ScriptInstance != null) ScriptInstance.Update(registry); }
  public override void Draw(Registry registry) { if (ScriptInstance != null) ScriptInstance.Draw(registry); }
  public void AssignObjectScript(dynamic? scriptInstance) => ScriptInstance = scriptInstance;
  
  public virtual void SetPosition(Vector2 position) => Position = position;
  
  public virtual void SetX(float x) => Position.X = x;
  
  public virtual void SetY(float y) => Position.Y = y;
  
  public Vector2 GetPosition() => Position;
  
  public void SetSize(Vector2 size) => Size = size;
  
  public Vector2 GetSize() => Size;
}