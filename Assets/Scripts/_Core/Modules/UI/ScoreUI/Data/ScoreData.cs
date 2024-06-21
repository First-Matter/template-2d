using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Data/Sets/ScoreData")]
public class ScoreData : ScriptableObject
{

  [System.NonSerialized]
  public ScoreResetBehaviour sceneResetBehaviour = ScoreResetBehaviour.ResetForFirstScene;
  public int score;
  public int highScore;
  public ScoreUpdateChannel scoreUpdateChannel;
  private string _scoreTextFormat = "Score: {0}\nHigh Score: {1}";

  public string ScoreTextFormat
  {
    get => _scoreTextFormat;
    set
    {
      _scoreTextFormat = value;
      scoreUpdateChannel.Invoke(GetScore());
    }
  }
  public void ResetScore()
  {
    if (sceneResetBehaviour == ScoreResetBehaviour.ResetForAllScenes)
    {
      score = 0;
      highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    else if (sceneResetBehaviour == ScoreResetBehaviour.ResetForFirstScene)
    {
      if (SceneManager.GetActiveScene().buildIndex == 0)
      {
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
      }
    }
    scoreUpdateChannel.Invoke(GetScore());
  }

  public void AddScore(int points)
  {
    score += points;
    SetHighScore(score);
    scoreUpdateChannel.Invoke(GetScore());
  }

  public ScoreObject GetScore()
  {
    return new ScoreObject(score, highScore);
  }

  private void SetHighScore(int score)
  {
    if (score > highScore)
    {
      highScore = score;
      PlayerPrefs.SetInt("HighScore", highScore);
    }
  }
}

[System.Serializable]
public class ScoreObject
{
  public int score;
  public int highScore;

  public ScoreObject(int score, int highScore)
  {
    this.score = score;
    this.highScore = highScore;
  }
}
public enum ScoreResetBehaviour
{
  ResetForFirstScene,
  ResetForAllScenes
}