using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public enum Button
{
  Jump, Fire
}

[System.Serializable]
public class PlayerInput : IPlayerInput
{
  [SerializeField] private InputBinding[] inputBindings;
  [SerializeField] private PlayerInputEvents _playerData;

  public void UpdateInput()
  {
    foreach (var binding in inputBindings)
    {
      if (binding.IsPressed())
      {
        _playerData.InvokeButtonPress(binding.button);
      }
      if (binding.IsHeld())
      {
        _playerData.InvokeButtonHold(binding.button);
      }
      else
      {
        _playerData.InvokeButtonRelease(binding.button);
      }
    }

    Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    if (moveDirection != Vector2.zero)
    {
      _playerData.InvokeMove(moveDirection);
    }
  }

  public void RegisterButtonPressEvent(Button button, Action action)
  {
    _playerData.RegisterButtonPressEvent(button, action);
  }

  public void RegisterButtonHoldEvent(Button button, Action action)
  {
    _playerData.RegisterButtonHoldEvent(button, action);
  }

  public void RegisterButtonReleaseEvent(Button button, Action action)
  {
    _playerData.RegisterButtonReleaseEvent(button, action);
  }

  public void RegisterMoveEvent(Action<Vector2> action)
  {
    _playerData.RegisterMoveEvent(action);
  }

  public void UnRegisterButtonPressEvent(Button button, Action action)
  {
    _playerData.UnRegisterButtonPressEvent(button, action);
  }

  public void UnRegisterButtonHoldEvent(Button button, Action action)
  {
    _playerData.UnRegisterButtonHoldEvent(button, action);
  }

  public void UnRegisterButtonReleaseEvent(Button button, Action action)
  {
    _playerData.UnRegisterButtonReleaseEvent(button, action);
  }

  public void UnRegisterMoveEvent(Action<Vector2> action)
  {
    _playerData.UnRegisterMoveEvent(action);
  }
}
