using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Animations;

public class InputEventHandler : EventDrivenBehaviour
{
  [Subscribe][SerializeField] private ButtonPressedChannel buttonPressedChannel;
  [Subscribe][SerializeField] private ButtonHeldChannel buttonHeldChannel;
  [Subscribe][SerializeField] private ButtonReleasedChannel buttonReleasedChannel;
  [Subscribe][SerializeField] private AxisChannel inputAxisChannel;
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
