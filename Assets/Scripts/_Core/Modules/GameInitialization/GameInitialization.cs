using UnityEngine;

public class GameInitialization : EventDrivenBehaviour
{
  [Data] private GameData _gameData;
  [SerializeField] private ScoreResetBehaviour scoreResetBehaviour = ScoreResetBehaviour.ResetForFirstScene;
  [SerializeField] private LevelResetBehaviour levelResetBehaviour = LevelResetBehaviour.ResetForFirstScene;

  private void Awake()
  {
    InitializeGameSettings();
  }

  private void InitializeGameSettings()
  {
    InitializeScoreSettings();
    InitializeLevelSettings();
    _gameData.scoreData.ResetScore();
    _gameData.levelUpData.ResetLevel();
  }

  private void InitializeScoreSettings()
  {
    _gameData.scoreData.sceneResetBehaviour = scoreResetBehaviour;
  }

  private void InitializeLevelSettings()
  {
    _gameData.levelUpData.sceneResetBehaviour = levelResetBehaviour;
  }
}
