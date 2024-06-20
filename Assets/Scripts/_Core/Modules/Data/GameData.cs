using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Audio/GameData")]
public class GameData : ScriptableObject
{
  public List<Sound> sounds;

  public Sound GetSound(RegisteredSound name)
  {
    string soundName = name.ToString();
    return sounds.Find(sound => sound.name == soundName);
  }
}
