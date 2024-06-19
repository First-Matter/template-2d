using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerInputEvents", menuName = "Scriptable Objects/PlayerInputEvents")]
public class PlayerInputEvents : ScriptableObject
{
  public event Action<Vector2> OnMove;
  private Dictionary<Button, Action> buttonToPressEvent = new Dictionary<Button, Action>();
  private Dictionary<Button, Action> buttonToHoldEvent = new Dictionary<Button, Action>();
  private Dictionary<Button, Action> buttonToReleaseEvent = new Dictionary<Button, Action>();

  public void RegisterMoveEvent(Action<Vector2> action)
  {
    OnMove += action;
  }
  public void UnRegisterMoveEvent(Action<Vector2> action)
  {
    OnMove -= action;
  }
  public void RegisterButtonPressEvent(Button button, Action action)
  {
    if (buttonToPressEvent.ContainsKey(button))
    {
      buttonToPressEvent[button] += action;
    }
    else
    {
      buttonToPressEvent[button] = action;
    }
  }
  public void UnRegisterButtonPressEvent(Button button, Action action)
  {
    if (buttonToPressEvent.ContainsKey(button))
    {
      buttonToPressEvent[button] -= action;
    }
  }

  public void RegisterButtonHoldEvent(Button button, Action action)
  {
    if (buttonToHoldEvent.ContainsKey(button))
    {
      buttonToHoldEvent[button] += action;
    }
    else
    {
      buttonToHoldEvent[button] = action;
    }
  }
  public void UnRegisterButtonHoldEvent(Button button, Action action)
  {
    if (buttonToHoldEvent.ContainsKey(button))
    {
      buttonToHoldEvent[button] -= action;
    }
  }

  public void RegisterButtonReleaseEvent(Button button, Action action)
  {
    if (buttonToReleaseEvent.ContainsKey(button))
    {
      buttonToReleaseEvent[button] += action;
    }
    else
    {
      buttonToReleaseEvent[button] = action;
    }
  }
  public void UnRegisterButtonReleaseEvent(Button button, Action action)
  {
    if (buttonToReleaseEvent.ContainsKey(button))
    {
      buttonToReleaseEvent[button] -= action;
    }
  }

  public void InvokeMove(Vector2 direction)
  {
    OnMove?.Invoke(direction);
  }

  public void InvokeButtonPress(Button button)
  {
    if (buttonToPressEvent.ContainsKey(button))
    {
      buttonToPressEvent[button]?.Invoke();
    }
  }

  public void InvokeButtonHold(Button button)
  {
    if (buttonToHoldEvent.ContainsKey(button))
    {
      buttonToHoldEvent[button]?.Invoke();
    }
  }

  public void InvokeButtonRelease(Button button)
  {
    if (buttonToReleaseEvent.ContainsKey(button))
    {
      buttonToReleaseEvent[button]?.Invoke();
    }
  }
}
