using UnityEngine;
using TMPro;

public class LevelUI : EventDrivenBehaviour
{
  [SerializeField] private LevelUpEventChannel levelUpEventChannel;
  [Data][SerializeField] private GameData gameData;
  [SerializeField] private TextMeshProUGUI levelText;

  private void OnEnable()
  {
    levelUpEventChannel.RegisterEvent(OnLevelUp);
  }

  private void OnDisable()
  {
    levelUpEventChannel.UnRegisterEvent(OnLevelUp);
  }

  private void OnLevelUp(int newLevel)
  {
    Dev.Log($"Player reached level {newLevel}");
    if (levelText != null)
    {
      levelText.text = string.Format(gameData.levelUpData.TextFormat, newLevel);
    }
  }
}
