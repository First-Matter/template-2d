using UnityEngine;
public class ButtonChannel : EventChannel<Button>
{
}
[CreateAssetMenu(menuName = "Events/Input/ButtonPressedChannel")]
public class ButtonPressedChannel : ButtonChannel
{
}
