using UnityEngine;

[CreateAssetMenu(fileName = "ExpData", menuName = "Data/Sets/ExpData")]
public class ExpData : BarData
{
  protected override void OnFull()
  {
    Dev.Log("Level up!");
    value = 0;
  }
}
