using System.ComponentModel;
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
  [Data][SerializeField] private GameData gameData;

  private void Update()
  {
    if (GetInputBinding(InputButton.Pause).IsPressed() && !gameData.isSelectingUpgrade)
    {
      gameData.pauseController.TogglePause();
    }
    if (PauseController.isPaused)
    {
      return;
    }
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
  InputBinding GetInputBinding(InputButton button)
  {
    foreach (var binding in inputBindings)
    {
      if (binding.button == button)
      {
        return binding;
      }
    }
    return null;
  }
}
