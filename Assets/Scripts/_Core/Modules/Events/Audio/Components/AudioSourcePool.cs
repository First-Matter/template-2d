using UnityEngine;
using System.Collections.Generic;

public class AudioSourcePool
{
  [SerializeField] private int initialPoolSize = 10;
  private Queue<AudioSource> availableSources = new Queue<AudioSource>();

  public void Initialize(Transform transform)
  {
    for (int i = 0; i < initialPoolSize; i++)
    {
      AddNewAudioSourceToPool(transform);
    }
  }

  private void AddNewAudioSourceToPool(Transform transform)
  {
    GameObject go = new GameObject("AudioSource");
    go.transform.SetParent(transform);
    AudioSource newSource = go.AddComponent<AudioSource>();
    newSource.gameObject.SetActive(false);
    availableSources.Enqueue(newSource);
  }

  public AudioSource GetAudioSource(Transform transform)
  {
    if (availableSources.Count == 0)
    {
      AddNewAudioSourceToPool(transform);
    }
    AudioSource source = availableSources.Dequeue();
    source.gameObject.SetActive(true);
    return source;
  }

  public void ReturnAudioSource(AudioSource source)
  {
    source.gameObject.SetActive(false);
    availableSources.Enqueue(source);
  }
}
