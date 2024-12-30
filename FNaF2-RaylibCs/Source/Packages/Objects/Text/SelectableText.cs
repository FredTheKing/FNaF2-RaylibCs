using System.Numerics;
using FNaF2_RaylibCs.Source.Packages.Module.ResourcesManager;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Packages.Objects.Text;

public class SelectableText : SimpleText
{
  protected List<string> Text;
  protected int TextIndex;

  public SelectableText(Vector2 position, Vector2 size, int fontSize, List<string> texts, Color color, bool alignCenterV = false,
    bool alignCenterH = false) : base(position, size, fontSize, color, alignCenterV, alignCenterH) => Text = texts;

  public SelectableText(Vector2 position, Vector2 size, int fontSize, List<string> texts, Color color, FontResource font,
    bool alignCenterV = false, bool alignCenterH = false) : base(position, size, fontSize, color, font,
    alignCenterV, alignCenterH) => Text = texts;
  
  public void SetTexts(List<string> texts) => Text = texts;
  public void SetText(string text, int index) => Text[index] = text;
  
  public void SetTextIndex(int index) => TextIndex = index;
  public void NextText() => TextIndex = (TextIndex + 1) % Text.Count;
  public void PreviousText() => TextIndex = (TextIndex - 1 + Text.Count) % Text.Count;

  protected override void PostDraw(Vector2 newPosition) => Raylib.DrawTextEx(Font.GetMaterial(), Text[TextIndex], newPosition + Offset, FontSize, FontSpacing, Color);
}