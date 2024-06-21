using UnityEngine;

[CreateAssetMenu(fileName = "BarData", menuName = "Data/Sets/BarData")]
public class BarData : ScriptableObject
{
  public float value;
  public float maxValue;
  [SerializeField] private EventChannel<BarData> updateChannel;

  public void Reduce(float amount)
  {
    value = Mathf.Max(0, value - amount);
    updateChannel.Invoke(this);
    if (value == 0)
    {
      Dev.Log("Bar depleted");
    }
  }

  public void Increase(float amount)
  {
    value = Mathf.Min(maxValue, value + amount);
    updateChannel.Invoke(this);
  }
  public void Initialize(float value, float maxValue)
  {
    this.value = value;
    this.maxValue = maxValue;
    updateChannel.Invoke(this);
  }
}