using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BarUI : MonoBehaviour
{
  [SerializeField] private Slider barSlider;
  [SerializeField] private TextMeshProUGUI barText;
  [SerializeField] private BarUpdateEventChannel barUpdateChannel;
  [SerializeField] private BarUpdateEventChannel lastBarUpdateChannel;
  [SerializeField] private Image FillImage;

  private void OnEnable()
  {
    if (barUpdateChannel != null)
    {
      Debug.Log("Registering");
      barUpdateChannel.RegisterEvent(UpdateBar);
    }
  }

  private void OnDisable()
  {
    if (barUpdateChannel != null)
    {
      barUpdateChannel.UnRegisterEvent(UpdateBar);
    }
  }

  public void OnValidate()
  {
    if (lastBarUpdateChannel != barUpdateChannel)
    {
      if (lastBarUpdateChannel != null)
      {
        lastBarUpdateChannel.UnRegisterEvent(UpdateBar);
      }

      if (barUpdateChannel != null)
      {
        barUpdateChannel.RegisterEvent(UpdateBar);
      }

      lastBarUpdateChannel = barUpdateChannel;
    }

    if (FillImage != null && barUpdateChannel != null && FillImage.color != barUpdateChannel.BarColor)
    {
      FillImage.color = barUpdateChannel.BarColor;
    }
  }

  public void UpdateBar(BarData data)
  {
    if (barSlider != null)
    {
      if (barSlider.maxValue != data.maxValue)
      {
        barSlider.maxValue = data.maxValue;
      }

      if (FillImage != null && data.updateChannel.BarColor != FillImage.color)
      {
        FillImage.color = data.updateChannel.BarColor;
      }

      if (barSlider.value != data.value)
      {
        barSlider.value = data.value;
      }
    }

    if (barText != null)
    {
      barText.text = $"{data.value} / {data.maxValue}";
    }
  }
}
