using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Text;

public class SelectableText : SimpleText
{
  protected List<string> _text;
  protected int _text_index = 0;

  SelectableText(Vector2 position, Vector2 size, int font_size, List<string> texts, Color color, bool align_center_v = false,
    bool align_center_h = false) : base(position, size, font_size, color, align_center_v, align_center_h) => _text = texts;

  SelectableText(Vector2 position, Vector2 size, int font_size, List<string> texts, Color color, FontResource font,
    bool align_center_v = false, bool align_center_h = false) : base(position, size, font_size, color, font,
    align_center_v, align_center_h) => _text = texts;
  
  public void SetTexts(List<string> texts) => _text = texts;
  public void SetText(string text, int index) => _text[index] = text;
  
  public void SetTextIndex(int index) => _text_index = index;
  public void NextText() => _text_index = (_text_index + 1) % _text.Count;
  public void PreviousText() => _text_index = (_text_index - 1 + _text.Count) % _text.Count;

  protected override void PostDraw(Vector2 new_position) => Raylib.DrawTextEx(_font.GetMaterial(), _text[_text_index], new_position + _offset, _font_size, _font_spacing, _color);
}