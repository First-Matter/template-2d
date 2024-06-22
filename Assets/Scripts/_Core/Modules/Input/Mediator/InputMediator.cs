using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InputMediator", menuName = "Events/Input/InputMediator")]
public class InputMediator : BaseData
{
  [Subscribe] public AxisChannel inputAxisChannel;
  [Subscribe] public ButtonPressedChannel buttonPressedChannel;
  [Subscribe] public ButtonHeldChannel buttonHeldChannel;
  [Subscribe] public ButtonReleasedChannel buttonReleasedChannel;

  private Dictionary<InputButton, Action> buttonPressedActions = new Dictionary<InputButton, Action>();
  private Dictionary<InputButton, Action> buttonHeldActions = new Dictionary<InputButton, Action>();
  private Dictionary<InputButton, Action> buttonReleasedActions = new Dictionary<InputButton, Action>();

  protected override void OnEnable()
  {
    base.OnEnable();
    RegisterActionsForChannels();
  }

  void OnDisable()
  {
    UnRegisterActionsForChannels();
  }
  public void RegisterMoveAction(Action<Vector2> action)
  {
    inputAxisChannel.RegisterEvent(action);
  }

  public void UnRegisterMoveAction(Action<Vector2> action)
  {
    inputAxisChannel.UnRegisterEvent(action);
  }

  private void RegisterActionsForChannels()
  {
    buttonPressedChannel.RegisterEvent(InvokeButtonPressed);
    buttonHeldChannel.RegisterEvent(InvokeButtonHeld);
    buttonReleasedChannel.RegisterEvent(InvokeButtonReleased);
  }

  private void UnRegisterActionsForChannels()
  {
    buttonPressedChannel.UnRegisterEvent(InvokeButtonPressed);
    buttonHeldChannel.UnRegisterEvent(InvokeButtonHeld);
    buttonReleasedChannel.UnRegisterEvent(InvokeButtonReleased);
  }

  public void RegisterButtonPressedAction(InputButton button, Action action)
  {
    if (!buttonPressedActions.ContainsKey(button))
    {
      buttonPressedActions[button] = action;
    }
    else
    {
      buttonPressedActions[button] += action;
    }
  }

  public void UnRegisterButtonPressedAction(InputButton button, Action action)
  {
    if (buttonPressedActions.ContainsKey(button))
    {
      buttonPressedActions[button] -= action;
      if (buttonPressedActions[button] == null)
      {
        buttonPressedActions.Remove(button);
      }
    }
  }

  public void RegisterButtonHeldAction(InputButton button, Action action)
  {
    if (!buttonHeldActions.ContainsKey(button))
    {
      buttonHeldActions[button] = action;
    }
    else
    {
      buttonHeldActions[button] += action;
    }
  }

  public void UnRegisterButtonHeldAction(InputButton button, Action action)
  {
    if (buttonHeldActions.ContainsKey(button))
    {
      buttonHeldActions[button] -= action;
      if (buttonHeldActions[button] == null)
      {
        buttonHeldActions.Remove(button);
      }
    }
  }

  public void RegisterButtonReleasedAction(InputButton button, Action action)
  {
    if (!buttonReleasedActions.ContainsKey(button))
    {
      buttonReleasedActions[button] = action;
    }
    else
    {
      buttonReleasedActions[button] += action;
    }
  }

  public void UnRegisterButtonReleasedAction(InputButton button, Action action)
  {
    if (buttonReleasedActions.ContainsKey(button))
    {
      buttonReleasedActions[button] -= action;
      if (buttonReleasedActions[button] == null)
      {
        buttonReleasedActions.Remove(button);
      }
    }
  }

  private void InvokeButtonPressed(InputButton button)
  {
    if (buttonPressedActions.ContainsKey(button))
    {
      buttonPressedActions[button]?.Invoke();
    }
  }

  private void InvokeButtonHeld(InputButton button)
  {
    if (buttonHeldActions.ContainsKey(button))
    {
      buttonHeldActions[button]?.Invoke();
    }
  }

  private void InvokeButtonReleased(InputButton button)
  {
    if (buttonReleasedActions.ContainsKey(button))
    {
      buttonReleasedActions[button]?.Invoke();
    }
  }
}
