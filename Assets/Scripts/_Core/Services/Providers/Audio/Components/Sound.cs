using UnityEngine;

[System.Serializable]
public class Sound
{
  public string name;
  public AudioClip clip;
  [Range(0f, 1f)]
  public float volume = 0.7f;
  public bool loop = false;
  public bool global = false;
  public bool playOnAwake = false;
  [HideInInspector]
  public AudioSource source;
}