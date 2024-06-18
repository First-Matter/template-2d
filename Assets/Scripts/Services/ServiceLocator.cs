using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
  private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
  private static readonly Dictionary<Type, List<Action<object>>> subscribers = new Dictionary<Type, List<Action<object>>>();

  public static void RegisterService<T>(T service)
  {
    var type = typeof(T);
    services[type] = service;

    if (subscribers.ContainsKey(type))
    {
      foreach (var subscriber in subscribers[type])
      {
        subscriber.Invoke(service);
      }
      subscribers.Remove(type);
    }
  }
  public static void SubscribeToService<T>(Action<T> onServiceAvailable)
  {
    var type = typeof(T);
    if (services.ContainsKey(type))
    {
      onServiceAvailable.Invoke((T)services[type]);
    }
    else
    {
      if (!subscribers.ContainsKey(type))
      {
        subscribers[type] = new List<Action<object>>();
      }
      subscribers[type].Add(service => onServiceAvailable((T)service));
    }
  }

  public static void UnregisterService<T>()
  {
    var type = typeof(T);
    if (services.ContainsKey(type))
    {
      services.Remove(type);
    }
  }
}
