using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class GameAudio : IGameAudio
{
  [Range(0f, 1f)]
  public float masterVolume = 1f;
  [Range(0f, 1f)]
  public float musicVolume = 0.7f;
  [Range(0f, 1f)]
  public float sfxVolume = 0.7f;
  public Sound[] music, sfx;

  private List<AudioSource> musicSources = new List<AudioSource>();
  private List<AudioSource> sfxSources = new List<AudioSource>();
  private float lastMasterVolume;
  private float lastMusicVolume;
  private float lastSfxVolume;
  public bool VolumeChanged => lastMasterVolume != masterVolume | lastMusicVolume != musicVolume | lastSfxVolume != sfxVolume;

  private void AddSource(AudioSource source, string type)
  {
    if (type == "music")
    {
      if (!musicSources.Contains(source))
      {
        musicSources.Add(source);
      }
    }
    else if (type == "sfx")
    {
      if (!sfxSources.Contains(source))
      {
        sfxSources.Add(source);
      }
    }
  }
  public void AdjustVolume()
  {
    AdjustMusicVolume();
    AdjustSFXVolume();
  }
  private void AdjustMusicVolume()
  {
    for (int i = musicSources.Count - 1; i >= 0; i--)
    {
      AudioSource source = musicSources[i];
      if (source != null)
      {
        source.volume = musicVolume * masterVolume;
      }
      else
      {
        musicSources.RemoveAt(i);
      }
    }
  }
  private void AdjustSFXVolume()
  {
    for (int i = sfxSources.Count - 1; i >= 0; i--)
    {
      AudioSource source = sfxSources[i];
      if (source != null)
      {
        source.volume = sfxVolume * masterVolume;
      }
      else
      {
        sfxSources.RemoveAt(i);
      }
    }
  }
  public void SetMasterVolume(float volume)
  {
    masterVolume = volume;
  }
  public void SetMusicVolume(float volume)
  {
    musicVolume = volume;
  }
  public void SetSFXVolume(float volume)
  {
    sfxVolume = volume;
  }
  public void PlaySound(string soundName, AudioSource source)
  {
    AddSource(source, "sfx");
    Sound sound = System.Array.Find(sfx, s => s.name == soundName);
    source.clip = sound.clip;
    source.volume = sound.volume * sfxVolume * masterVolume;
    source.loop = sound.loop;
    source.Play();
  }
  public void PlayMusic(string musicName, AudioSource source)
  {
    AddSource(source, "music");
    Sound sound = System.Array.Find(music, m => m.name == musicName);
    source.volume = sound.volume * musicVolume * masterVolume;
    source.loop = sound.loop;
    source.clip = sound.clip;
    source.Play();
  }
  public void PlaySound(string soundName)
  {
    Sound sound = System.Array.Find(sfx, s => s.name == soundName);
    AddSource(sound.source, "sfx");
    sound.source.clip = sound.clip;
    sound.source.volume = sound.volume * sfxVolume * masterVolume;
    sound.source.loop = sound.loop;
    sound.source.Play();
  }
  public void PlayMusic(string musicName)
  {
    Sound sound = System.Array.Find(music, m => m.name == musicName);
    AddSource(sound.source, "music");
    sound.source.volume = sound.volume * musicVolume * masterVolume;
    sound.source.loop = sound.loop;
    sound.source.Play();
  }
}