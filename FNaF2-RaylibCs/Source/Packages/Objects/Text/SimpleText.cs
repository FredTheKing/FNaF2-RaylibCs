using System.Numerics;
using System.Text;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Text;

public class SimpleText : ObjectTemplate
{
  public SimpleText(Vector2 position, Vector2 size, int font_size, string text, Color color, bool align_center_v = false, bool align_center_h = false) : base()
  {
    _position = position;
    _size = size;
    _text = Encoding.UTF8.GetBytes(text).ToArray();
    _font_size = font_size;
    _color = color;
    _remember_color = color;
    _font = new FontResource(Raylib.GetFontDefault());
    _align_center_h = align_center_h;
    _align_center_v = align_center_v;
  }
  
  public SimpleText(Vector2 position, Vector2 size, int font_size, string text, Color color, FontResource font, bool align_center_v = false, bool align_center_h = false) : base()
  {
    _position = position;
    _size = size;
    _text = Encoding.UTF8.GetBytes(text).ToArray();
    _font_size = font_size;
    _color = color;
    _remember_color = color;
    _font = font;
    _align_center_h = align_center_h;
    _align_center_v = align_center_v;
  }
  
  public SimpleText(Vector2 position, Vector2 size, int font_size, Color color, bool align_center_v = false, bool align_center_h = false) : base()
  {
    _position = position;
    _size = size;
    _text = Encoding.UTF8.GetBytes("").ToArray();
    _font_size = font_size;
    _color = color;
    _remember_color = color;
    _font = new FontResource(Raylib.GetFontDefault());
    _align_center_h = align_center_h;
    _align_center_v = align_center_v;
  }
  
  public SimpleText(Vector2 position, Vector2 size, int font_size, Color color, FontResource font, bool align_center_v = false, bool align_center_h = false) : base()
  {
    _position = position;
    _size = size;
    _text = Encoding.UTF8.GetBytes("").ToArray();
    _font_size = font_size;
    _color = color;
    _remember_color = color;
    _font = font;
    _align_center_h = align_center_h;
    _align_center_v = align_center_v;
  }
  
  private byte[] _text;
  private int _font_size;
  private float _font_spacing = 1.0f;
  private Color _color; 
  private Color _remember_color;
  private FontResource _font;
  private Vector2 _offset = new(0, 0);
  private bool _align_center_h;
  private bool _align_center_v;

  protected string debugger_name = "Text-" + new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4)
    .Select(s => s[new Random().Next(s.Length)]).ToArray());
  
  public new void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode(debugger_name))
    {
      ImGui.Text($" > Position: {_position.X}, {_position.Y}");
      ImGui.Text($" > Offset: {_offset.X}, {_offset.Y}");
      ImGui.Text($" > Size: {_size.X}, {_size.Y}");
      ImGui.Text($" > Text: {GetText()}");
      ImGui.Text($" > Color: {_color.R}, {_color.G}, {_color.B}, {_color.A}");
      ImGui.Text($" > Font Size: {_font_size}");
      ImGui.Text($" > Font Spacing: {_font_spacing}");
      ImGui.TreePop();
    }
  }
  
  protected void DrawText()
  {
    string converted_text = Encoding.UTF8.GetString(_text);
    
    Vector2 measured_text = Raylib.MeasureTextEx(_font.GetMaterial(), GetText(), _font_size, _font_spacing);
    float text_pos_x = _align_center_h ? _position.X + _size.X/2 - measured_text.X/2 : _position.X + 10;
    float text_pos_y = _align_center_v ? _position.Y + _size.Y/2 - measured_text.Y/2 : _position.Y + 10;
    
    Vector2 new_position = new Vector2(text_pos_x, text_pos_y);
    
    Raylib.DrawTextEx(_font.GetMaterial(), converted_text, new_position + _offset, _font_size, _font_spacing, _color);
  }
  
  public void SetCurrentFrameColor(Color color) => _color = color;
  
  public string GetTextRaw() => BitConverter.ToString(_text).Replace("-", "");
  
  public string GetText() => Encoding.UTF8.GetString(_text);
  
  public void SetText(string text) => _text = Encoding.UTF8.GetBytes(text);

  private void UndoColorChanges()
  {
    if (Raylib.ColorToInt(_color) == Raylib.ColorToInt(_remember_color)) return;
    _color = _remember_color;
  }
  
  public new void Draw(Registry registry)
  {
    DrawText();
    DrawDebug(registry);
    base.Draw(registry);
    UndoColorChanges();
  }

  protected internal void DrawDebug(Registry registry)
  {
    if(registry.GetShowBounds() & registry.GetDebugMode()) Raylib.DrawRectangleLinesEx(new Rectangle(_position, _size), 1, Color.Lime);
  }
}