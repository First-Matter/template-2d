using UnityEngine;
using TMPro;
using UnityEditor.Build.Player;

public class UIManager : EventDrivenBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [Data][SerializeField] private GameData gameData;
  [Subscribe(Channel.ScoreUpdateChannel)][SerializeField] private ScoreUpdateChannel scoreUpdateChannel;
  [Subscribe(Channel.HealthUpdateChannel)][SerializeField] private HealthUpdateChannel healthChannel;
  public enum ScoreResetBehaviour
  {
    ResetForFirstScene,
    ResetForAllScenes
  }
  public ScoreResetBehaviour sceneResetBehaviour = ScoreResetBehaviour.ResetForFirstScene;
  private void OnEnable()
  {
    scoreUpdateChannel.RegisterEvent(UpdateScoreText);
    healthChannel.RegisterEvent(UpdateHealthText);
    if (sceneResetBehaviour == ScoreResetBehaviour.ResetForFirstScene)
    {
      gameData.scoreData.ResetScore();
    }
  }
  private void OnDisable()
  {
    scoreUpdateChannel.UnRegisterEvent(UpdateScoreText);
    healthChannel.UnRegisterEvent(UpdateHealthText);
  }

  private void UpdateScoreText(ScoreObject scores)
  {
    scoreText.text = "Score: " + scores.score + "\nHigh Score: " + scores.highScore;
  }
  private void UpdateHealthText(HealthData health)
  {
    Debug.Log("Health: " + health.health + " / " + health.maxHealth);
  }
}
