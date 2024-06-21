using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
  [SerializeField] private Slider barSlider;
  [SerializeField] private TextMeshProUGUI barText;
  [SerializeField] private BarUpdateEventChannel barUpdateChannel;

  private void OnEnable()
  {
    barUpdateChannel.RegisterEvent(UpdateBar);
  }
  private void OnDisable()
  {
    barUpdateChannel.UnRegisterEvent(UpdateBar);
  }

  public void UpdateBar(BarData data)
  {
    if (barSlider != null)
    {
      if (barSlider.maxValue != data.maxValue)
        barSlider.maxValue = data.maxValue;
      barSlider.value = data.value;
    }
    if (barText != null)
      barText.text = $"{data.value} / {barSlider.maxValue}";
  }
}