using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpdateChannel", menuName = "Events/HealthUpdateChannel")]
public class HealthUpdateChannel : EventChannel<HealthData>
{
}
