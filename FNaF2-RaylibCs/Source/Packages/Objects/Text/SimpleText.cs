using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using ImGuiNET;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Text;

public class SimpleText : ObjectTemplate
{
  public SimpleText(Vector2 position, Vector2 size, int fontSize, string text, Color color, bool alignCenterV = false, bool alignCenterH = false)
  {
    Position = position;
    Size = size;
    _text = text;
    FontSize = fontSize;
    Color = color;
    RememberColor = color;
    Font = new FontResource(Raylib.GetFontDefault());
    AlignCenterH = alignCenterH;
    AlignCenterV = alignCenterV;
  }
  
  public SimpleText(Vector2 position, Vector2 size, int fontSize, string text, Color color, FontResource font, bool alignCenterV = false, bool alignCenterH = false)
  {
    Position = position;
    Size = size;
    _text = text;
    FontSize = fontSize;
    Color = color;
    RememberColor = color;
    Font = font;
    AlignCenterH = alignCenterH;
    AlignCenterV = alignCenterV;
  }
  
  public SimpleText(Vector2 position, Vector2 size, int fontSize, Color color, bool alignCenterV = false, bool alignCenterH = false)
  {
    Position = position;
    Size = size;
    _text = "";
    FontSize = fontSize;
    Color = color;
    RememberColor = color;
    Font = new FontResource(Raylib.GetFontDefault());
    AlignCenterH = alignCenterH;
    AlignCenterV = alignCenterV;
  }
  
  public SimpleText(Vector2 position, Vector2 size, int fontSize, Color color, FontResource font, bool alignCenterV = false, bool alignCenterH = false)
  {
    Position = position;
    Size = size;
    _text = "";
    FontSize = fontSize;
    Color = color;
    RememberColor = color;
    Font = font;
    AlignCenterH = alignCenterH;
    AlignCenterV = alignCenterV;
  }
  
  private string _text;
  protected int FontSize;
  protected float FontSpacing = 1.0f;
  protected Color Color; 
  protected Color RememberColor;
  protected FontResource Font;
  protected Vector2 Offset = new(0, 0);
  protected bool AlignCenterH;
  protected bool AlignCenterV;
  protected bool HideText = false;

  protected string DebuggerName = "Text-" + new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4)
    .Select(s => s[new Random().Next(s.Length)]).ToArray());
  
  public override void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode(DebuggerName))
    {
      ImGui.Text($" > Position: {Position.X}, {Position.Y}");
      ImGui.Text($" > Offset: {Offset.X}, {Offset.Y}");
      ImGui.Text($" > Size: {Size.X}, {Size.Y}");
      ImGui.Text($" > Text: {GetText()}");
      ImGui.Text($" > Color: {Color.R}, {Color.G}, {Color.B}, {Color.A}");
      ImGui.Text($" > Font Size: {FontSize}");
      ImGui.Text($" > Font Spacing: {FontSpacing}");
      ImGui.TreePop();
    }
  }
  
  protected void DrawText()
  {
    Vector2 measuredText = Raylib.MeasureTextEx(Font.GetMaterial(), GetText(), FontSize, FontSpacing);
    float textPosX = AlignCenterH ? Position.X + Size.X/2 - measuredText.X/2 : Position.X + 10;
    float textPosY = AlignCenterV ? Position.Y + Size.Y/2 - measuredText.Y/2 : Position.Y + 10;
    
    Vector2 newPosition = new Vector2(textPosX, textPosY);
    
    PostDraw(newPosition);
  }

  protected virtual void PostDraw(Vector2 newPosition) => Raylib.DrawTextEx(Font.GetMaterial(), _text, newPosition + Offset, FontSize, FontSpacing, Color);
  
  public void SetCurrentFrameColor(Color color) => Color = color;
  
  public string GetText() => _text;
  
  public void SetText(string text) => _text = text;

  private void UndoColorChanges()
  {
    if (Raylib.ColorToInt(Color) == Raylib.ColorToInt(RememberColor)) return;
    Color = RememberColor;
  }
  
  public override void Draw(Registry registry)
  {
    if (!HideText) DrawText();
    DrawDebug(registry);
    base.Draw(registry);
    UndoColorChanges();
  }

  protected void DrawDebug(Registry registry)
  {
    if (registry.GetShowBounds() & registry.GetDebugMode()) Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size), 1, Color.Lime);
  }
}