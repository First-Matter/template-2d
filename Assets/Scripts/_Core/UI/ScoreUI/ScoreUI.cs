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
    gameData.scoreData.ResetScore();
  }
  private void OnDisable()
  {
    scoreUpdateChannel.UnRegisterEvent(UpdateScoreText);
  }

  private void UpdateScoreText(ScoreObject scores)
  {
    scoreText.text = "Score: " + scores.score + "\nHigh Score: " + scores.highScore;
  }
}
