using UnityEngine;
[CreateAssetMenu(menuName = "Events/Effects/EffectsData")]
public class EffectsData : BaseData
{
  [Data][SerializeField] private ScreenShakeData screenShake;
  public void ShakeScreen(float intensity, float duration)
  {
    screenShake.StartShake(new ScreenShakeEventData(intensity, duration));
  }
}