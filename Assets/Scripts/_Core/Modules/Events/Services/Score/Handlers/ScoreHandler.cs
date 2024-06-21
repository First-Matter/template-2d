using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.EditorTools;

public class ScoreEventHandler : EventDrivenBehaviour
{
  [Listen(Channel.ScoreChannel)][SerializeField] private ScoreChannel _scoreChannel;
  [Data][SerializeField] private GameData gameData;
  private enum SceneLoadScoreBehavior { Reset, Keep };
  [Tooltip("When the next scene is loaded, should the score be reset or kept?")]
  [SerializeField] private SceneLoadScoreBehavior sceneLoadScoreBehavior = SceneLoadScoreBehavior.Reset;
  [SerializeField] private float scoreMultiplier = 1f;
  private void OnEnable()
  {
    if (sceneLoadScoreBehavior == SceneLoadScoreBehavior.Reset)
    {
      gameData.scoreData.ResetScore();
    }
    else
    {
      ResetScoreForFirstScene();
    }
    _scoreChannel.RegisterEvent(UpdateScore);
  }
  private void ResetScoreForFirstScene()
  {
    if (SceneManager.GetActiveScene().buildIndex == 0)
    {
      gameData.scoreData.ResetScore();
    }
  }
  private void OnDisable()
  {
    _scoreChannel.UnRegisterEvent(UpdateScore);
  }
  private void UpdateScore(int score)
  {
    score = (int)Mathf.Round(score * scoreMultiplier);
    gameData.scoreData.AddScore(score);
  }
}