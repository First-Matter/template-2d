using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioEventHandler : EventDrivenBehaviour
{
  [Listen("SoundChannel")][SerializeField] private EventChannel<Sound> playSoundChannel;
  [Range(0f, 1f)] public float masterVolume = 1f;
  [Range(0f, 1f)] public float musicVolume = 0.7f;
  [Range(0f, 1f)] public float sfxVolume = 0.7f;
  public Sound[] sounds;

  [SerializeField] private VolumeControl _volumeControl;
  [SerializeField] private AudioSource globalMusicSource;
  [SerializeField] private AudioSource globalSfxSource;

  private List<AudioSource> musicSources = new List<AudioSource>();
  private List<AudioSource> sfxSources = new List<AudioSource>();

  private float lastMasterVolume;
  private float lastMusicVolume;
  private float lastSfxVolume;

  public bool VolumeChanged => lastMasterVolume != masterVolume || lastMusicVolume != musicVolume || lastSfxVolume != sfxVolume;

  void Awake()
  {
    CreateAudioSources();
    if (!_volumeControl.Initialized)
    {
      Debug.LogWarning("Volume control sliders are not initialized.");
      return;
    }

    _volumeControl.masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
    _volumeControl.masterVolumeSlider.value = masterVolume;
    _volumeControl.musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    _volumeControl.musicVolumeSlider.value = musicVolume;
    _volumeControl.sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    _volumeControl.sfxVolumeSlider.value = sfxVolume;
  }

  private void OnEnable()
  {
    playSoundChannel.RegisterEvent(PlaySound);
  }

  private void OnDisable()
  {
    playSoundChannel.UnRegisterEvent(PlaySound);
  }

  private void Update()
  {
    if (VolumeChanged)
    {
      AdjustVolume();
    }
  }

  private void CreateAudioSources()
  {
    foreach (Sound sound in sounds)
    {
      if (sound.global)
      {
        GameObject audioSource = new GameObject(sound.name);
        audioSource.transform.SetParent(transform);
        sound.source = audioSource.AddComponent<AudioSource>();
        if (sound.playOnAwake)
        {
          PlaySound(sound);
        }
      }
    }
  }

  private void AddSource(AudioSource source, SoundType type)
  {
    if (type == SoundType.Music)
    {
      if (!musicSources.Contains(source))
      {
        musicSources.Add(source);
      }
    }
    else if (type == SoundType.SFX)
    {
      if (!sfxSources.Contains(source))
      {
        sfxSources.Add(source);
      }
    }
  }

  private void AdjustVolume()
  {
    AdjustMusicVolume();
    AdjustSFXVolume();
    lastMasterVolume = masterVolume;
    lastMusicVolume = musicVolume;
    lastSfxVolume = sfxVolume;
  }

  private void AdjustMusicVolume()
  {
    foreach (var source in musicSources)
    {
      if (source != null)
      {
        source.volume = musicVolume * masterVolume;
      }
    }
  }

  private void AdjustSFXVolume()
  {
    foreach (var source in sfxSources)
    {
      if (source != null)
      {
        source.volume = sfxVolume * masterVolume;
      }
    }
  }

  public void SetMasterVolume(float volume)
  {
    masterVolume = volume;
    AdjustVolume();
  }

  public void SetMusicVolume(float volume)
  {
    musicVolume = volume;
    AdjustVolume();
  }

  public void SetSFXVolume(float volume)
  {
    sfxVolume = volume;
    AdjustVolume();
  }

  private void PlaySound(Sound sound)
  {
    AudioSource audioSource = sound.source ?? (sound.type == SoundType.Music ? globalMusicSource : globalSfxSource);

    if (audioSource == null)
    {
      Debug.LogWarning($"No audio source found for sound: {sound.name}");
      return;
    }

    AddSource(audioSource, sound.type);
    audioSource.clip = sound.clip;
    audioSource.volume = sound.volume * (sound.type == SoundType.Music ? musicVolume : sfxVolume) * masterVolume;
    audioSource.loop = sound.loop;
    audioSource.Play();
  }
}
