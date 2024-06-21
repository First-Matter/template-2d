using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Data/Sets/ScoreData")]
public class ScoreData : ScriptableObject
{
  public int score;
  public int highScore;
  [SerializeField] private ScoreUpdateChannel scoreUpdateChannel;
  public enum SceneResetBehaviour
  {
    Reset,
    Keep
  }
  public void ResetScore()
  {
    score = 0;
    highScore = PlayerPrefs.GetInt("HighScore", 0);
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
