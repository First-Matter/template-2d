using UnityEngine;
using TMPro;
using UnityEditor.Build.Player;

public class UIManager : EventDrivenBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [Data][SerializeField] private GameData gameData;
  [Subscribe(Channel.ScoreUpdateChannel)][SerializeField] private ScoreUpdateChannel scoreUpdateChannel;
  private void OnEnable()
  {
    scoreUpdateChannel.RegisterEvent(UpdateScoreText);
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
