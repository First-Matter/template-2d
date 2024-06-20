using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class VolumeControl
{
  public Slider masterVolumeSlider;
  public Slider musicVolumeSlider;
  public Slider sfxVolumeSlider;
  public bool Initialized => masterVolumeSlider != null && musicVolumeSlider != null && sfxVolumeSlider != null;
}
