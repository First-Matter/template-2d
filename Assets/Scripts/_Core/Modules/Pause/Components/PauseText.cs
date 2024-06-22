using System.Collections;
using UnityEngine;
using TMPro;

public class PauseText : EventDrivenBehaviour
{
  [Data] private GameData gameData;
  private TextMeshProUGUI textComponent;

  void OnEnable()
  {
    textComponent = GetComponent<TextMeshProUGUI>();
    textComponent.text = gameData.pauseController.pauseText;
    Color initialColor = textComponent.color;
    initialColor.a = 1.0f;
    textComponent.color = initialColor;
    if (gameData.pauseController.textFlashSpeed > 0f)
    {
      StartCoroutine(FlashText());
    }
  }

  private IEnumerator FlashText()
  {
    bool isTextVisible = true;
    while (true)
    {
      yield return new WaitForSecondsRealtime(gameData.pauseController.textFlashSpeed);
      isTextVisible = !isTextVisible;
      Color currentColor = textComponent.color;
      currentColor.a = isTextVisible ? 1.0f : 0.0f;
      textComponent.color = currentColor;
    }
  }


  public void UpdateText(string newText)
  {
    StopAllCoroutines();
    textComponent.text = newText;
    if (gameData.pauseController.textFlashSpeed > 0f)
    {
      StartCoroutine(FlashText());
    }
  }
}
