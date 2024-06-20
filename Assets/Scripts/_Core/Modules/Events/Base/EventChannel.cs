using UnityEngine;
using System;

public class EventChannel<T> : ScriptableObject, IListen<T>, IBroadcast<T>
{
  public event Action<T> OnEvent;

  public virtual void RegisterEvent(Action<T> action)
  {
    OnEvent += action;
  }
  public virtual void UnRegisterEvent(Action<T> action)
  {
    OnEvent -= action;
  }

  public virtual void Invoke(T value)
  {
    OnEvent?.Invoke(value);
  }
}
