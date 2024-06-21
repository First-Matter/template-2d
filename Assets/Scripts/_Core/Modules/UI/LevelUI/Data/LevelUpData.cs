using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Sets/LevelData")]
public class LevelData : ScriptableObject
{
  public int currentLevel = 1;
  public float maxExpMultiplier = 1.2f;
  public LevelUpEventChannel levelUpEventChannel;
  public LevelResetBehaviour sceneResetBehaviour = LevelResetBehaviour.ResetForAllScenes;
  private string _textFormat = "Level: {0}";

  public string TextFormat
  {
    get => _textFormat;
    set
    {
      _textFormat = value;
      levelUpEventChannel.Invoke(GetLevel());
    }
  }

  public void LevelUp()
  {
    currentLevel++;
    levelUpEventChannel.Invoke(currentLevel);
    Dev.Log($"Leveled up to level {currentLevel}");
  }

  public int GetLevel() => currentLevel;

  public void ResetLevel()
  {
    if (sceneResetBehaviour == LevelResetBehaviour.ResetForAllScenes)
    {
      currentLevel = 1;
    }
    else if (sceneResetBehaviour == LevelResetBehaviour.ResetForFirstScene && SceneManager.GetActiveScene().buildIndex == 0)
    {
      currentLevel = 1;
    }
  }
}

public enum LevelResetBehaviour
{
  ResetForFirstScene,
  ResetForAllScenes
}
