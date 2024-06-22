using UnityEngine;
public class ButtonChannel : EventChannel<InputButton>
{
}
[CreateAssetMenu(menuName = "Events/Input/ButtonPressedChannel")]
public class ButtonPressedChannel : ButtonChannel
{
}
