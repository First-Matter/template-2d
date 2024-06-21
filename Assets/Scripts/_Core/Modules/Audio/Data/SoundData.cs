using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SoundData", menuName = "Data/Sets/SoundData")]
public class SoundData : ScriptableObject
{
  public List<Sound> sounds;

  public Sound GetSound(RegisteredSound name)
  {
    string soundName = name.ToString();
    return sounds.Find(sound => sound.name == soundName);
  }
}