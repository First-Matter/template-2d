using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
  [SerializeField] private Slider barSlider;
  [SerializeField] private TextMeshProUGUI barText;

  public void Initialize(float maxValue)
  {
    barSlider.maxValue = maxValue;
    UpdateBar(maxValue);
  }

  public void UpdateBar(float value)
  {
    if (barSlider != null)
      barSlider.value = value;
    if (barText != null)
      barText.text = $"{value} / {barSlider.maxValue}";
  }
}