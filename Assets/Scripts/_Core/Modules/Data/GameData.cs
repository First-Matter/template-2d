using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Audio/GameData")]
public class GameData : ScriptableObject
{
  // Sound
  public List<Sound> sounds;
  public Sound GetSound(RegisteredSound name)
  {
    string soundName = name.ToString();
    return sounds.Find(sound => sound.name == soundName);
  }
  // Score
  public int score;
  public int highScore;
  public void ResetScore()
  {
    score = 0;
    highScore = PlayerPrefs.GetInt("HighScore", 0);
  }
  public void AddScore(int points)
  {
    score += points;
    SetHighScore(score);
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