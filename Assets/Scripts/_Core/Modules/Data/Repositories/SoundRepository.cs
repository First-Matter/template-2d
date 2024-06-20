using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoundRepository", menuName = "Data/Audio/SoundRepository")]
public class SoundRepository : ScriptableObject
{
  public List<Sound> sounds;

  public Sound GetSound(string name)
  {
    return sounds.Find(sound => sound.name == name);
  }
}
