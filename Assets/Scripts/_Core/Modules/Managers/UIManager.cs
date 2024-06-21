using UnityEngine;
using TMPro;
using UnityEditor.Build.Player;

public class UIManager : EventDrivenBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [Data][SerializeField] private GameData gameData;
  private int lastScore = 0;

  void Awake()
  {
    UpdateScoreText(new(0, 0));
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
