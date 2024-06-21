using UnityEngine;

[CreateAssetMenu(fileName = "BarUpdateChannel", menuName = "Events/Bar/BarUpdateChannel")]
public class BarUpdateEventChannel : EventChannel<BarData>
{
  public Color BarColor;
}