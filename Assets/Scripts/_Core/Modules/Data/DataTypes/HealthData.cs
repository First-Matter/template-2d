using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Data/Sets/HealthData")]
public class HealthData : ScriptableObject
{
  public float health;
  public float maxHealth;

  // public void ReduceHealth(float amount, EventChannel<bool> deathChannel)
  // {
  //   health = Mathf.Max(0, health - amount);
  //   if (health == 0)
  //   {
  //     deathChannel.Invoke(true);
  //   }
  // }
  public void ReduceHealth(float amount)
  {
    health = Mathf.Max(0, health - amount);
    if (health == 0)
    {
      Dev.Log("RIP");
    }
  }

  public void Heal(float amount)
  {
    health = Mathf.Min(maxHealth, health + amount);
  }
}
