using UnityEngine;
using TMPro;
using UnityEditor.Build.Player;

public class UIManager : EventDrivenBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [Data][SerializeField] private GameData gameData;
  private int lastScore = -1;

  void Awake()
  {

  }

  void Update()
  {
    if (lastScore != gameData.scoreData.score)
    {
      lastScore = gameData.scoreData.score;
      UpdateScoreText(gameData.scoreData.GetScore());
    }
  }
  private void UpdateScoreText(ScoreObject scores)
  {
    scoreText.text = "Score: " + scores.score + "\nHigh Score: " + scores.highScore;
  }
}
