using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
  private static readonly Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();

  public static void RegisterService<T>(T service)
  {
    var type = typeof(T);
    if (!services.ContainsKey(type))
    {
      services[type] = service;
    }
  }
  public static object GetService(Type type)
  {
    if (services.ContainsKey(type))
    {
      return services[type];
    }
    Debug.LogError($"Service of type {type} not registered");
    return default;
  }
  public static T GetService<T>()
  {
    var type = typeof(T);
    if (services.TryGetValue(type, out var service))
    {
      return (T)service;
    }

    Debug.LogError($"Service of type {type} not found.");
    return default;
  }
}
