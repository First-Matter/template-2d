using UnityEngine;

[System.Serializable]
public class InputBinding
{
  public Button button;
  public bool AnyKey;
  public KeyCode[] keys;
  public bool IsPressed()
  {
    if (AnyKey)
    {
      return Input.anyKeyDown;
    }
    foreach (KeyCode key in keys)
    {
      if (Input.GetKeyDown(key))
      {
        return true;
      }
    }
    return false;
  }
  public bool IsHeld()
  {
    if (AnyKey)
    {
      return Input.anyKey;
    }
    foreach (KeyCode key in keys)
    {
      if (Input.GetKey(key))
      {
        return true;
      }
    }
    return false;
  }
  public bool IsReleased()
  {
    if (AnyKey)
    {
      return Input.anyKey;
    }
    foreach (KeyCode key in keys)
    {
      if (Input.GetKeyUp(key))
      {
        return true;
      }
    }
    return false;
  }
}