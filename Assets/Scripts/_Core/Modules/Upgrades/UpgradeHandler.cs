using System.Collections.Generic;
using UnityEngine;
using static UpgradeScriptableObject;

public class UpgradeEventHandler : EventDrivenBehaviour
{
  [Data] public GameData gameData;
  [Subscribe][SerializeField] private LevelUpEventChannel levelUpEventChannel;
  [Subscribe][SerializeField] private UpgradeEventChannel upgradeEventChannel;

  public List<UpgradeScriptableObject> potentialUpgrades;
  private List<UpgradeScriptableObject> removedUpgrades = new List<UpgradeScriptableObject>();
  private int totalChance;
  public UpgradeOption upgradeOptionPrefab;
  private List<UpgradeOption> upgradeOptions;
  public int numberOfOptions = 3;
  [SerializeField] private Transform upgradeOptionParent;

  private void OnEnable()
  {
    levelUpEventChannel.RegisterEvent(OnLevelUp);
    upgradeEventChannel.RegisterEvent(ApplyUpgrade);
  }

  private void OnDisable()
  {
    levelUpEventChannel.UnRegisterEvent(OnLevelUp);
    upgradeEventChannel.UnRegisterEvent(ApplyUpgrade);
  }

  private void Awake()
  {
    totalChance = CalculateTotalChance();
    upgradeOptions = new List<UpgradeOption>(); // Initialize the upgradeOptions list
  }

  private void OnLevelUp(int newLevel)
  {
    if (newLevel < 2)
    {
      return;
    }
    upgradeOptionParent.gameObject.SetActive(true);
    gameData.pauseController.SetPause(true);
    GenerateUpgrades();
  }

  private int CalculateTotalChance()
  {
    int total = 0;
    foreach (UpgradeScriptableObject upgrade in potentialUpgrades)
    {
      total += upgrade.Chance;
    }
    return total;
  }

  public void RemoveUpgrade(UpgradeEnum upgrade)
  {
    List<UpgradeScriptableObject> upgradesToRemove = potentialUpgrades.FindAll(u => u.Upgrade == upgrade);
    foreach (UpgradeScriptableObject upgradeToRemove in upgradesToRemove)
    {
      potentialUpgrades.Remove(upgradeToRemove);
      removedUpgrades.AddRange(upgradesToRemove);
    }
  }

  public void Reset()
  {
    if (removedUpgrades.Count > 0)
    {
      potentialUpgrades.AddRange(removedUpgrades);
      removedUpgrades.Clear();
    }
  }

  public void ApplyUpgrade(UpgradeEvent upgradeEvent)
  {
    switch (upgradeEvent.upgrade.Upgrade)
    {
      case UpgradeEnum.IncreaseHealth:
        gameData.playerHealth.maxValue += upgradeEvent.amount;
        gameData.playerHealth.Initialize(gameData.playerHealth.value, gameData.playerHealth.maxValue);
        break;
      case UpgradeEnum.IncreaseMana:
        gameData.playerMana.maxValue += upgradeEvent.amount;
        gameData.playerMana.Initialize(gameData.playerMana.value, gameData.playerMana.maxValue);
        break;
    }
  }

  private void GenerateUpgrades()
  {
    Debug.Log("Generating upgrades...");
    List<UpgradeScriptableObject> availableUpgrades = new List<UpgradeScriptableObject>(potentialUpgrades);
    int availableTotalChance = totalChance;

    for (int i = 0; i < numberOfOptions; i++)
    {
      UpgradeOption upgradeOption;
      if (i < upgradeOptions.Count)
      {
        // Reuse an existing UpgradeOption
        upgradeOption = upgradeOptions[i];
        upgradeOption.gameObject.SetActive(true);
      }
      else
      {
        // Instantiate a new UpgradeOption and add it to the list
        upgradeOption = Instantiate(upgradeOptionPrefab, upgradeOptionParent);
        upgradeOptions.Add(upgradeOption);
      }

      int randomValue = Random.Range(0, availableTotalChance);
      int cumulativeChance = 0;
      for (int j = 0; j < availableUpgrades.Count; j++)
      {
        cumulativeChance += availableUpgrades[j].Chance;
        if (randomValue < cumulativeChance)
        {
          upgradeOption.Upgrade = availableUpgrades[j];
          availableTotalChance -= availableUpgrades[j].Chance;
          availableUpgrades.RemoveAt(j);
          break;
        }
      }
    }

    // Deactivate any remaining UpgradeOptions
    for (int i = numberOfOptions; i < upgradeOptions.Count; i++)
    {
      upgradeOptions[i].gameObject.SetActive(false);
    }
  }
}
