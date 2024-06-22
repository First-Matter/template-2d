using UnityEngine;

[CreateAssetMenu(fileName = "BarData", menuName = "Data/Sets/BarData")]
public class BarData : BaseData
{
  public float value;
  public float maxValue;
  public BarUpdateEventChannel updateChannel;

  public virtual void Reduce(float amount)
  {
    value = Mathf.Max(0, value - amount);
    updateChannel.Invoke(this);
    if (value == 0)
    {
      OnDeplete();
    }
  }

  public virtual void Increase(float amount)
  {
    value = Mathf.Min(maxValue, value + amount);
    updateChannel.Invoke(this);
    if (value == maxValue)
    {
      OnFull();
    }
  }

  public void Initialize(float value, float maxValue)
  {
    this.value = value;
    this.maxValue = maxValue;
    updateChannel.Invoke(this);
  }

  protected virtual void OnDeplete()
  {
    Dev.Log("Bar depleted");
  }

  protected virtual void OnFull()
  {
    Dev.Log("Bar full");
  }
}
