// using UnityEngine;
// 
// public class PlayMusic : EventDrivenBehaviour
// {
//   // [Inject] private IGameAudio _audioHandler;
//   public string soundName;
//   private AudioSource audioSource;
//   protected override void Awake()
//   {
//     base.Awake();
//     audioSource = gameObject.AddComponent<AudioSource>();
//   }
//   void Start()
//   {
//     if (_audioHandler == null)
//     {
//       Debug.LogError("Audio service not found.");
//     }
//     _audioHandler.PlayMusic(soundName, audioSource);
//   }
// }