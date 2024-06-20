using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioEventHandler : EventDrivenBehaviour
{
  [Listen(Channel.SoundChannel)][SerializeField] private EventChannel<Sound> playSoundChannel;
  private AudioSourcePool audioSourcePool = new AudioSourcePool();
  [Range(0f, 1f)] public float masterVolume = 1f;
  [Range(0f, 1f)] public float musicVolume = 0.7f;
  [Range(0f, 1f)] public float sfxVolume = 0.7f;
  public Sound[] sounds;
  [Data(Repository.SoundRepository)][SerializeField] private SoundRepository soundRepository;

  [SerializeField] private VolumeControl _volumeControl;

  private float lastMasterVolume;
  private float lastMusicVolume;
  private float lastSfxVolume;

  private Dictionary<AudioSource, Sound> activeAudioSources = new Dictionary<AudioSource, Sound>();

  public bool VolumeChanged => lastMasterVolume != masterVolume || lastMusicVolume != musicVolume || lastSfxVolume != sfxVolume;

  void Awake()
  {
    if (soundRepository != null)
    {
      soundRepository.sounds = new List<Sound>(sounds);
    }
    audioSourcePool.Initialize(transform);
    PlayAwakeSources();
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

  private void PlayAwakeSources()
  {
    foreach (Sound sound in sounds)
    {
      if (sound.playOnAwake)
      {
        PlaySound(sound);
      }
    }
  }

  private void AdjustVolume()
  {
    foreach (var entry in activeAudioSources)
    {
      AudioSource source = entry.Key;
      Sound sound = entry.Value;
      source.volume = sound.volume * (sound.type == SoundType.Music ? musicVolume : sfxVolume) * masterVolume;
    }
    lastMasterVolume = masterVolume;
    lastMusicVolume = musicVolume;
    lastSfxVolume = sfxVolume;
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
    AudioSource audioSource = audioSourcePool.GetAudioSource(transform);
    audioSource.gameObject.name = sound.name;
    audioSource.clip = sound.clip;
    audioSource.volume = sound.volume * (sound.type == SoundType.Music ? musicVolume : sfxVolume) * masterVolume;
    audioSource.loop = sound.loop;
    audioSource.Play();

    activeAudioSources[audioSource] = sound;

    if (!sound.loop)
    {
      StartCoroutine(ReturnToPoolAfterPlaying(audioSource, sound.clip.length));
    }
  }

  private System.Collections.IEnumerator ReturnToPoolAfterPlaying(AudioSource source, float duration)
  {
    yield return new WaitForSeconds(duration);
    activeAudioSources.Remove(source);
    source.gameObject.name = "AudioSource";
    audioSourcePool.ReturnAudioSource(source);
  }
}
