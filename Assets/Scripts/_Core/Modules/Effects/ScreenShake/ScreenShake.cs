using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ScreenShake", menuName = "Data/Effects/ScreenShake")]
public class ScreenShakeData : BaseData
{
  [Subscribe][SerializeField] private CouroutineEventChannel couroutineEventChannel;
  IEnumerator Shake(float duration, float magnitude)
  {
    Vector3 originalPosition = Camera.main.transform.localPosition;

    float elapsed = 0.0f;

    while (elapsed < duration)
    {
      float x = Random.Range(-1f, 1f) * magnitude;
      float y = Random.Range(-1f, 1f) * magnitude;

      Camera.main.transform.localPosition = new Vector3(x, y, originalPosition.z);

      elapsed += Time.deltaTime;

      yield return null;
    }
    Camera.main.transform.localPosition = originalPosition;
  }
  public void StartShake(ScreenShakeEventData screenShakeEventData)
  {
    couroutineEventChannel.Invoke(Shake(screenShakeEventData.shakeLength, screenShakeEventData.magnitude));
  }
}
public class ScreenShakeEventData
{
  public float shakeLength = 2f;
  public float magnitude = 0.7f;
  public ScreenShakeEventData(float shakeLength, float magnitude)
  {
    this.shakeLength = shakeLength;
    this.magnitude = magnitude;
  }
}