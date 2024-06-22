using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewUpgrade", menuName = "UpgradeObject")]

public class UpgradeScriptableObject : ScriptableObject
{

  [Header("Upgrade Information")]
  [Tooltip("The icon that will be displayed for the upgrade")]
  public Sprite Icon;
  [Tooltip("The title of the upgrade")]
  public string Title;
  [Tooltip("The singular description of the upgrade, use {0} to represent the amount of the upgrade")]
  public string Description;
  [Tooltip("The plural description of the upgrade, use {0} to represent the amount of the upgrade")]
  public string DescriptionPlural;
  [Tooltip("The minimum amount of the upgrade that can be found at the start of the game")]
  public int minMinAmount = 1;
  [Tooltip("The maximum amount of the upgrade that can be found at the start of the game")]
  public int minMaxAmount = 3;
  [Tooltip("The minimum amount of the upgrade that can be found at the end of the game")]
  public int maxMinAmount = 3;
  [Tooltip("The maximum amount of the upgrade that can be found at the end of the game")]
  public int maxMaxAmount = 10;
  public enum UpgradeEnum { IncreaseHealth, ReRoll, IncreaseMana };
  [Tooltip("The UpgradeManager method that will be called when the upgrade is selected")]
  public UpgradeEnum Upgrade;
  [Tooltip("The percentage chance of the upgrade option being presented to the player")]
  [Range(0, 100)] public int Chance;
}
public class UpgradeEvent
{
  public UpgradeScriptableObject upgrade;
  public int amount;
  public UpgradeEvent(UpgradeScriptableObject upgrade, int amount)
  {
    this.upgrade = upgrade;
    this.amount = amount;
  }
}