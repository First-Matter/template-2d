using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : BaseData
{
  [Data] public SoundData soundData;
  [Data] public ScoreData scoreData;
  [Data] public HealthData playerHealth;
  [Data] public ManaData playerMana;
  [Data] public ExpData playerExp;
  [Data] public LevelData levelUpData;
  [Data] public PauseController pauseController;
  public bool isSelectingUpgrade;
}
