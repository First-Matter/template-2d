using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
  public SoundData soundData;
  public ScoreData scoreData;
  public HealthData playerHealth;
  public ManaData playerMana;
  public ExpData playerExp;
}
