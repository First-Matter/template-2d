using System;
using UnityEngine;
public interface IPlayerInput
{
  public void RegisterButtonPressEvent(Button button, Action action);
  public void RegisterButtonHoldEvent(Button button, Action action);
  public void RegisterButtonReleaseEvent(Button button, Action action);
  public void RegisterMoveEvent(Action<Vector2> action);
  public void UnRegisterButtonPressEvent(Button button, Action action);
  public void UnRegisterButtonHoldEvent(Button button, Action action);
  public void UnRegisterButtonReleaseEvent(Button button, Action action);
  public void UnRegisterMoveEvent(Action<Vector2> action);
}