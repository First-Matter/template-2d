using System;
using System.Reflection;
using UnityEngine;
[AttributeUsage(AttributeTargets.Field)]
public class InjectAttribute : Attribute
{
}
public static class DependencyInjector
{
  public static void InjectDependencies(MonoBehaviour monoBehaviour)
  {
    var fields = monoBehaviour.GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

    foreach (var field in fields)
    {
      if (field.GetCustomAttribute(typeof(InjectAttribute)) != null)
      {
        var service = ServiceLocator.GetService(field.FieldType);
        if (service != null)
        {
          field.SetValue(monoBehaviour, service);
        }
        else
        {
          Debug.LogError($"Service of type {field.FieldType} not found for injection in {monoBehaviour.GetType().Name}");
        }
      }
    }
  }
}