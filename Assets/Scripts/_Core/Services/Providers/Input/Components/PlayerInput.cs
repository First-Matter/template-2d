using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum Button
{
  Jump, Fire
}
[System.Serializable]
public class PlayerInput : IPlayerInput
{
  [SerializeField]
  private InputBinding[] inputBindings;
  public bool IsButtonPressed(Button button)
  {
    foreach (InputBinding binding in inputBindings)
    {
      if (binding.button == button && binding.IsPressed())
      {
        return true;
      }
    }
    return false;
  }
  public bool IsButtonHeld(Button button)
  {
    foreach (InputBinding binding in inputBindings)
    {
      if (binding.button == button && binding.IsHeld())
      {
        return true;
      }
    }
    return false;
  }
  public float GetAxisHorizontal()
  {
    return Input.GetAxis("Horizontal");
  }
  public float GetVerticalAxis()
  {
    return Input.GetAxis("Vertical");
  }
  public void SetBinding(Button button, InputBinding binding)
  {
    for (int i = 0; i < inputBindings.Length; i++)
    {
      if (inputBindings[i].button == button)
      {
        inputBindings[i] = binding;
        return;
      }
    }
    InputBinding[] newBindings = new InputBinding[inputBindings.Length + 1];
    for (int i = 0; i < inputBindings.Length; i++)
    {
      newBindings[i] = inputBindings[i];
    }
    newBindings[inputBindings.Length] = binding;
    inputBindings = newBindings;
  }
}