using UnityEngine;
using TMPro;
using UnityEditor.Build.Player;
using UnityEngine.UI;

public class ScoreUI : EventDrivenBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [Data][SerializeField] private GameData gameData;
  [Subscribe][SerializeField] private ScoreUpdateChannel scoreUpdateChannel;

  private void OnEnable()
  {
    scoreUpdateChannel.RegisterEvent(UpdateScoreText);
  }
  private void OnDisable()
  {
    scoreUpdateChannel.UnRegisterEvent(UpdateScoreText);
  }

  public void UpdateScoreText(ScoreObject scores)
  {
    scoreText.text = string.Format(gameData.scoreData.ScoreTextFormat, scores.score, scores.highScore);
  }
}
