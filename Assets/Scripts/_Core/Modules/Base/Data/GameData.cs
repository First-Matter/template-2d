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
  [Data] public EffectsData effects;
  public bool isSelectingUpgrade { get; set; }
  protected override void OnEnable()
  {
    base.OnEnable();
    isSelectingUpgrade = false;
  }
}
