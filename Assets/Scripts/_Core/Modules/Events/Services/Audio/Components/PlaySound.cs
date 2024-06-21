using System;
using UnityEngine;

public class PlaySound : EventDrivenBehaviour
{
  [Subscribe(Channel.SoundChannel)][SerializeField] private SoundChannel _soundChannel;
  public RegisteredSound soundName;
  void Awake()
  {
    _soundChannel.Invoke(soundName);
  }
}