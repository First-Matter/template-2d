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

  private Dictionary<Button, Action> buttonPressedActions = new Dictionary<Button, Action>();
  private Dictionary<Button, Action> buttonHeldActions = new Dictionary<Button, Action>();
  private Dictionary<Button, Action> buttonReleasedActions = new Dictionary<Button, Action>();

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

  public void RegisterButtonPressedAction(Button button, Action action)
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

  public void UnRegisterButtonPressedAction(Button button, Action action)
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

  public void RegisterButtonHeldAction(Button button, Action action)
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

  public void UnRegisterButtonHeldAction(Button button, Action action)
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

  public void RegisterButtonReleasedAction(Button button, Action action)
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

  public void UnRegisterButtonReleasedAction(Button button, Action action)
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

  private void InvokeButtonPressed(Button button)
  {
    if (buttonPressedActions.ContainsKey(button))
    {
      buttonPressedActions[button]?.Invoke();
    }
  }

  private void InvokeButtonHeld(Button button)
  {
    if (buttonHeldActions.ContainsKey(button))
    {
      buttonHeldActions[button]?.Invoke();
    }
  }

  private void InvokeButtonReleased(Button button)
  {
    if (buttonReleasedActions.ContainsKey(button))
    {
      buttonReleasedActions[button]?.Invoke();
    }
  }
}
