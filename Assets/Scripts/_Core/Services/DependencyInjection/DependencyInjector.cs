using System;
using System.Reflection;
using UnityEngine;

public static class DependencyInjector
{
  public static void InjectDependencies(MonoBehaviour monoBehaviour)
  {
    var fields = monoBehaviour.GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

    bool injectAttributeFound = false;
    foreach (var field in fields)
    {
      if (field.GetCustomAttribute(typeof(InjectAttribute)) != null)
      {
        injectAttributeFound = true;
        var fieldType = field.FieldType;
        SubscribeToService(fieldType, service =>
        {
          field.SetValue(monoBehaviour, service);
        });
      }
    }
    if (!injectAttributeFound)
    {
      Debug.LogWarning($"No fields marked with [Inject] attribute found in {monoBehaviour.GetType().Name}");
    }
  }

  private static void SubscribeToService(Type serviceType, Action<object> onServiceAvailable)
  {
    var subscribeMethod = typeof(ServiceLocator).GetMethod("SubscribeToService")
        .MakeGenericMethod(serviceType);
    subscribeMethod.Invoke(null, new object[] { onServiceAvailable });
  }
}
