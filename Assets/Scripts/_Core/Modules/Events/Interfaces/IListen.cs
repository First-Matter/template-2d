using System;
public interface IListen<T>
{
  void RegisterEvent(Action<T> action);
  void UnRegisterEvent(Action<T> action);
}