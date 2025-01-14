using FNaF2_RaylibCs.Source.Packages.Module;
using FNaF2_RaylibCs.Source.Packages.Module.Custom.Animatronics;
using FNaF2_RaylibCs.Source.Packages.Module.Templates;
using FNaF2_RaylibCs.Source.Packages.Objects.Image;
using Raylib_cs;

namespace FNaF2_RaylibCs.Source.Scripts.Scenes.Menu;

public class MenuCustomNight : ScriptTemplate
{
  private int[][] _difficultyList =
  [
    [20, 20, 20, 20, 0, 0, 0, 0, 0, 0], // 20/20/20/20
    [0, 0, 0, 0, 10, 10, 10, 10, 10, 0], // New & Shiny
    [0, 20, 0, 5, 0, 0, 20, 0, 0, 0], // Double Trouble
    [0, 0, 0, 0, 20, 0, 0, 0, 20, 10], // Night of Misfits
    [0, 0, 0, 20, 0, 0, 0, 0, 20, 0], // Foxy Foxy
    [0, 0, 20, 0, 0, 0, 0, 20, 20, 0], // Ladies' Night
    [20, 0, 0, 5, 10, 20, 0, 0, 0, 10], // Freddy's Circus
    [5, 5, 5, 5, 5, 5, 5, 5, 5, 5], // Cupcake Challenge
    [10, 10, 10, 10, 10, 10, 10, 10, 10, 10], // Fazbear Fever
    [20, 20, 20, 20, 20, 20, 20, 20, 20, 20] // Golden Freddy
  ];

  private HitboxTextBorderImage[] _animatronics;
  
  public override void Activation(Registry registry)
  {
    Registration.Objects.CustomNightStartText!.TextIndex = 1;
    Registration.Objects.WhiteBlinko!.GetScript()!.Play();
    _animatronics = 
    [
      Registration.Objects.CustomNightWitheredFreddy!,
      Registration.Objects.CustomNightWitheredBonnie!,
      Registration.Objects.CustomNightWitheredChica!,
      Registration.Objects.CustomNightWitheredFoxy!,
      Registration.Objects.CustomNightBalloonBoy!,
      Registration.Objects.CustomNightToyFreddy!,
      Registration.Objects.CustomNightToyBonnie!,
      Registration.Objects.CustomNightToyChica!,
      Registration.Objects.CustomNightMangle!,
      Registration.Objects.CustomNightGoldenFreddy!
    ];
  }

  public override void Update(Registry registry)
  {
    void UpdateDifficulties(Registry registry, Action action)
    {
      action();
      
      if (Registration.Objects.CustomNightStartText!.TextIndex is <=0 or 3) Registration.Objects.CustomNightStartText.TextIndex = 13;
      else if (Registration.Objects.CustomNightStartText.TextIndex is 2 or >=14) Registration.Objects.CustomNightStartText.TextIndex = 4;
      
      int index = Registration.Objects.CustomNightStartText.TextIndex;
      int[] selected = _difficultyList[index - 4];
      
      if (index == 1) return;

      for (int i = 0; i < _animatronics.Length; i++) 
        _animatronics[i].Text.SetText(selected[i].ToString());
    }
    
    if (Registration.Objects.CustomNightStartBox!.Hitbox.GetMousePress(MouseButton.Left) || Registration.Objects.CustomNightStartBox!.Hitbox.GetMousePress(MouseButton.Right)) Registration.Sounds.SetSound!.Play();
    Registration.Objects.CustomNightStartBox.SetFrame(Registration.Objects.CustomNightStartBox.Hitbox.GetMouseDrag(MouseButton.Left) || Registration.Objects.CustomNightStartBox.Hitbox.GetMouseDrag(MouseButton.Right) ? 1 : 0);
      
    if (Registration.Objects.CustomNightStartBox.Hitbox.GetMousePress(MouseButton.Left))
      UpdateDifficulties(registry, () => Registration.Objects.CustomNightStartText!.TextIndex += 1);
    if (Registration.Objects.CustomNightStartBox.Hitbox.GetMousePress(MouseButton.Right))
      UpdateDifficulties(registry, () => Registration.Objects.CustomNightStartText!.TextIndex -= 1);

    foreach (HitboxTextBorderImage anim in _animatronics)
    {
      if (anim.Hitbox.GetMousePress(MouseButton.Left) || anim.Hitbox.GetMousePress(MouseButton.Right))
      {
        Registration.Objects.CustomNightStartText!.TextIndex = 1;
        break;
      }
    }
    
    int[] animDiff = _animatronics.Select(a => int.Parse(a.Text.GetText())).ToArray();
    for (int i = 0; i < _difficultyList.Length; i++)
    {
      if (animDiff.SequenceEqual(_difficultyList[i]))
      {
        Registration.Objects.CustomNightStartText!.TextIndex = i + 4;
        break;
      }
    }
    
    if (Registration.Objects.CustomNightPresetBox!.Hitbox.GetMousePress(MouseButton.Left))
    {
      registry.fnaf.nightManager.current = 7;
      registry.scene.Change(registry, Config.Scenes.GameLoading);
      
      int index = Registration.Objects.CustomNightStartText!.TextIndex;
      for (int i = 0; i < _animatronics.Length; i++) 
        registry.fnaf.animatronicManager.all[i].Difficulty = _difficultyList[index][i];
    }
  }
}