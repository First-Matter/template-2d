using UnityEngine;

public class ManageUI : EventDrivenBehaviour
{
  [Data] private GameData _gameData;
  [SerializeField] private ScoreResetBehaviour scoreResetBehaviour = ScoreResetBehaviour.ResetForFirstScene;

  private void Awake()
  {
    ManageScore();
  }
  private void ManageScore()
  {
    _gameData.scoreData.sceneResetBehaviour = scoreResetBehaviour;
    _gameData.scoreData.ResetScore();
  }
}