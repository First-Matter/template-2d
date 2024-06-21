using UnityEngine;

[CreateAssetMenu(fileName = "ManaData", menuName = "Data/Sets/ManaData")]
public class ManaData : BarData
{
  protected override void OnDeplete()
  {
    Dev.Log("No mana!");
  }
}
