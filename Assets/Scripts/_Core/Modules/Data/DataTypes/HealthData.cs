using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Data/Sets/HealthData")]
public class HealthData : ScriptableObject
{
  public float health;
  public float maxHealth;
  [SerializeField] private HealthUpdateChannel healthUpdateChannel;

  public void ReduceHealth(float amount)
  {
    health = Mathf.Max(0, health - amount);
    healthUpdateChannel.Invoke(this);
    if (health == 0)
    {
      Dev.Log("RIP");
    }
  }

  public void Heal(float amount)
  {
    health = Mathf.Min(maxHealth, health + amount);
    healthUpdateChannel.Invoke(this);
  }
  public void Initialize(float health, float maxHealth)
  {
    this.health = health;
    this.maxHealth = maxHealth;
    healthUpdateChannel.Invoke(this);
  }
}
