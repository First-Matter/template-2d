using UnityEngine;

public class ManageUI : EventDrivenBehaviour
{
  [Data] private GameData _gameData;
  [Header("Score Settings")]
  [SerializeField] private string scoreTextFormat = "Score: {0}\nHigh Score: {1}";
  [SerializeField] private ScoreResetBehaviour scoreResetBehaviour = ScoreResetBehaviour.ResetForFirstScene;
  [Header("Bar Settings")]
  [SerializeField] private Color healthBarColor = Color.red;
  [SerializeField] private Color manaBarColor = Color.blue;
  [SerializeField] private Color expBarColor = Color.green;

  private void Awake()
  {
    ManageScore();
    ManageBarColors();
  }
  protected override void OnValidate()
  {
    base.OnValidate();
    ManageBarColors();
    BarUI[] barUIs = GetComponentsInChildren<BarUI>();
    foreach (var barUI in barUIs)
    {
      barUI.OnValidate();
    }
  }
  private void ManageScore()
  {
    _gameData.scoreData.sceneResetBehaviour = scoreResetBehaviour;
    _gameData.scoreData.ScoreTextFormat = scoreTextFormat;
    _gameData.scoreData.ResetScore();
  }
  private void ManageBarColors()
  {
    _gameData.playerHealth.updateChannel.BarColor = healthBarColor;
    _gameData.playerMana.updateChannel.BarColor = manaBarColor;
    _gameData.playerExp.updateChannel.BarColor = expBarColor;
  }
}