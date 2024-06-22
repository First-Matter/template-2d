using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UpgradeScriptableObject;

public class UpgradeOption : EventDrivenBehaviour
{
  public UpgradeScriptableObject Upgrade;

  [SerializeField] private TextMeshProUGUI Title;
  [SerializeField] private TextMeshProUGUI Description;
  [SerializeField] private Image Icon;
  [SerializeField] private int minAmount;
  private int minMinAmount = 1;
  private int maxMinAmount = 3;
  private int minMaxAmount = 3;
  private int maxMaxAmount = 10;
  [SerializeField] private int maxAmount;
  public int amount = 0;
  [Data][SerializeField] private GameData data;
  [Subscribe][SerializeField] private UpgradeEventChannel upgradeEventChannel;

  // Start is called before the first frame update
  void Start()
  {
    if (amount == 0)
    {
      amount = GetAmount();
    }
    Button button = GetComponent<Button>();
    button.onClick.AddListener(UpgradeFunction);
  }
  void Update()
  {
    if (Upgrade == null)
    {
      return;
    }
    if (amount == 0)
    {
      amount = GetAmount();
    }
    bool isPlural = amount > 1 && Upgrade.DescriptionPlural != "";
    string formattedDescription = isPlural ? string.Format(Upgrade.DescriptionPlural, amount) : string.Format(Upgrade.Description, amount);

    Title.text = Upgrade.Title;
    Description.text = formattedDescription;
    Icon.sprite = Upgrade.Icon;
  }
  public int GetAmount()
  {
    minMinAmount = Upgrade.minMinAmount;
    maxMinAmount = Upgrade.maxMinAmount;
    minMaxAmount = Upgrade.minMaxAmount;
    maxMaxAmount = Upgrade.maxMaxAmount;
    // Set minAmount based on difficultyFactor
    float difficultyFactor = 0.5f;
    minAmount = Mathf.RoundToInt(Mathf.Lerp(minMinAmount, maxMinAmount, difficultyFactor));
    maxAmount = Mathf.RoundToInt(Mathf.Lerp(minMaxAmount, maxMaxAmount, difficultyFactor));
    return Random.Range(minAmount, maxAmount + 1);
  }
  void Clear()
  {
    amount = 0;
    Title.text = "";
    Description.text = "";
    Icon.sprite = null;
  }
  public void UpgradeFunction()
  {
    upgradeEventChannel.Invoke(new UpgradeEvent(Upgrade, amount));
  }
  void OnDisable()
  {
    Clear();
  }
}
