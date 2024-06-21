using UnityEngine;

public class ManageUI : EventDrivenBehaviour
{
  [Data] private GameData _gameData;
  [Header("Text Formatting")]
  [SerializeField] private string scoreTextFormat = "Score: {0}\nHigh Score: {1}";
  [SerializeField] private string levelTextFormat = "Level: {0}";
  [Header("Bar Settings")]
  [SerializeField] private Color healthBarColor = Color.red;
  [SerializeField] private Color manaBarColor = Color.blue;
  [SerializeField] private Color expBarColor = Color.green;

  private void Awake()
  {
    InitializeUISettings();
  }

  protected override void OnValidate()
  {
    base.OnValidate();
    InitializeUISettings();
    ValidateBarUI();
  }

  private void InitializeUISettings()
  {
    ManageScoreUI();
    ManageBarColors();
    ManageLevelUI();
  }

  private void ManageScoreUI()
  {
    _gameData.scoreData.ScoreTextFormat = scoreTextFormat;
  }

  private void ManageBarColors()
  {
    _gameData.playerHealth.updateChannel.BarColor = healthBarColor;
    _gameData.playerMana.updateChannel.BarColor = manaBarColor;
    _gameData.playerExp.updateChannel.BarColor = expBarColor;
  }

  private void ManageLevelUI()
  {
    _gameData.levelUpData.TextFormat = levelTextFormat;
  }

  private void ValidateBarUI()
  {
    BarUI[] barUIs = GetComponentsInChildren<BarUI>();
    foreach (var barUI in barUIs)
    {
      barUI.OnValidate();
    }
  }
}
