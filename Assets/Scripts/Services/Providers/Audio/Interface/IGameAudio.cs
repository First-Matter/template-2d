using UnityEngine;
public interface IGameAudio
{
  void PlaySound(string soundName, AudioSource source);
  void PlaySound(string soundName);
  void PlayMusic(string soundName, AudioSource source);
  void PlayMusic(string soundName);
}