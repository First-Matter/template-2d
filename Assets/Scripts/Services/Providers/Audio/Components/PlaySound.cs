using UnityEngine;

public class PlaySound : InjectableMonoBehaviour
{
  [Inject] private IGameAudio _audioHandler;
  public string soundName;
  void Start()
  {
    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    _audioHandler.PlaySound(soundName, audioSource);
  }
}