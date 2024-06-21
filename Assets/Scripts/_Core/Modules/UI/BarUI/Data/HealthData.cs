using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Data/Sets/HealthData")]
public class HealthData : BarData
{
  protected override void OnDeplete()
  {
    Dev.Log("RIP");
  }
}
