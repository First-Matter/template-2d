using UnityEngine;

[CreateAssetMenu(fileName = "ExpData", menuName = "Data/Sets/ExpData")]
public class ExpData : BarData
{
  public float maxExpMultiplier = 1.2f;
  public GameData gameData;
  public void Initialize(int currentValue, int maxValue)
  {
    base.Initialize(currentValue, maxValue);
  }
  protected override void OnFull()
  {
    LevelUp();
  }

  private void LevelUp()
  {
    gameData.levelUpData.LevelUp();
    int newMaxExp = Mathf.FloorToInt(maxValue * gameData.levelUpData.maxExpMultiplier);
    Initialize(0, newMaxExp); // Example: increase max value for the next level
  }
}
