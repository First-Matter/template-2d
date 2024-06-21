using UnityEngine;
using TMPro;
using UnityEditor.Build.Player;
using UnityEngine.UI;

public class UIManager : EventDrivenBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI healthText;
  [SerializeField] private Slider healthSlider;
  [Data][SerializeField] private GameData gameData;
  [Subscribe][SerializeField] private ScoreUpdateChannel scoreUpdateChannel;
  [Subscribe][SerializeField] private HealthUpdateChannel healthChannel;

  public enum ScoreResetBehaviour
  {
    ResetForFirstScene,
    ResetForAllScenes
  }
  public ScoreResetBehaviour sceneResetBehaviour = ScoreResetBehaviour.ResetForFirstScene;
  private void OnEnable()
  {
    if (healthSlider != null)
    {
      healthSlider.maxValue = gameData.playerHealth.maxHealth;
      healthSlider.value = gameData.playerHealth.health;
    }
    scoreUpdateChannel.RegisterEvent(UpdateScoreText);
    healthChannel.RegisterEvent(UpdateHealthUI);
    if (sceneResetBehaviour == ScoreResetBehaviour.ResetForFirstScene)
    {
      gameData.scoreData.ResetScore();
    }
  }
  private void OnDisable()
  {
    scoreUpdateChannel.UnRegisterEvent(UpdateScoreText);
    healthChannel.UnRegisterEvent(UpdateHealthUI);
  }

  private void UpdateScoreText(ScoreObject scores)
  {
    scoreText.text = "Score: " + scores.score + "\nHigh Score: " + scores.highScore;
  }
  private void UpdateHealthUI(HealthData health)
  {
    if (healthText != null)
    {
      healthText.text = health.health + " / " + health.maxHealth;
    }
    if (healthSlider != null)
    {
      healthSlider.value = health.health;
    }
  }
}
