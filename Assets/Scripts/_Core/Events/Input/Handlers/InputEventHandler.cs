using System.Diagnostics.Tracing;
using UnityEngine;

public class InputEventHandler : EventDrivenBehaviour
{
  [Listen("ButtonPressedChannel")][SerializeField] private EventChannel<Button> buttonPressedChannel;
  [Listen("ButtonHeldChannel")][SerializeField] private EventChannel<Button> buttonHeldChannel;
  [Listen("ButtonReleasedChannel")][SerializeField] private EventChannel<Button> buttonReleasedChannel;
  [Listen("AxisChannel")][SerializeField] private EventChannel<Vector2> inputAxisChannel;
  [SerializeField] private InputBinding[] inputBindings;

  private void Update()
  {
    foreach (var binding in inputBindings)
    {
      if (binding.IsPressed())
      {
        buttonPressedChannel.Invoke(binding.button);
      }
      if (binding.IsHeld())
      {
        buttonHeldChannel.Invoke(binding.button);
      }
      else if (binding.IsReleased())
      {
        buttonReleasedChannel.Invoke(binding.button);
      }
    }

    Vector2 moveDirection = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    inputAxisChannel.Invoke(moveDirection);
  }
}
