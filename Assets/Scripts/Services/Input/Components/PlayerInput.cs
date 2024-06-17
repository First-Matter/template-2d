using UnityEngine;
using System.Collections;

public class PlayerInput : IPlayerInput
{
  readonly KeyCode jumpKey = KeyCode.Space;
  public bool IsJumpButtonPressed()
  {
    return Input.GetKeyDown(jumpKey);
  }
  public bool IsJumpButtonHeld()
  {
    return Input.GetKey(jumpKey);
  }
  public float GetAxisHorizontal()
  {
    return Input.GetAxis("Horizontal");
  }
  public float GetVerticalAxis()
  {
    return Input.GetAxis("Vertical");
  }
  public bool IsFireButtonPressed()
  {
    return Input.GetMouseButtonDown(0);
  }
  public bool IsFireButtonHeld()
  {
    return Input.GetMouseButton(0);
  }
}