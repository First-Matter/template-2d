using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ScriptableObjectAssigner
{
  public static void AssignEventChannels<T>(T target)
  {
    var fields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
      .Where(field => Attribute.IsDefined(field, typeof(SubscribeAttribute)));

    foreach (var field in fields)
    {
      var attribute = (SubscribeAttribute)Attribute.GetCustomAttribute(field, typeof(SubscribeAttribute));
      if (attribute != null)
      {
        var currentFieldValue = field.GetValue(target);
        if (currentFieldValue == null)
        {
          var channel = Resources.Load($"EventChannels/{field.FieldType.Name}", field.FieldType);
          if (channel != null)
          {
            field.SetValue(target, channel);
          }
#if UNITY_EDITOR
          else
          {
            Debug.LogWarning($"Could not find a channel of type {field.FieldType.Name} in Resources. Creating a new one.");

            var newChannel = ScriptableObject.CreateInstance(field.FieldType);
            if (newChannel == null)
            {
              Debug.LogError($"Failed to create instance of type {field.FieldType}");
              return;
            }
            if (!Directory.Exists("Assets/Resources/EventChannels"))
            {
              Directory.CreateDirectory("Assets/Resources/EventChannels");
            }
            AssetDatabase.CreateAsset(newChannel, $"Assets/Resources/EventChannels/{field.FieldType.Name}.asset");
            AssetDatabase.SaveAssets();
            field.SetValue(target, newChannel);
          }
#endif
        }
      }
    }
  }

  public static void AssignDataObjects<T>(T target)
  {
    var fields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
      .Where(field => Attribute.IsDefined(field, typeof(DataAttribute)));

    foreach (var field in fields)
    {
      string path;
      if (field.FieldType == typeof(GameData))
      {
        path = "DataObjects/GameData";
      }
      else
      {
        path = $"DataObjects/DataSets/{field.FieldType.Name}";
      }
      var attribute = (DataAttribute)Attribute.GetCustomAttribute(field, typeof(DataAttribute));
      if (attribute != null)
      {
        var currentFieldValue = field.GetValue(target);
        if (currentFieldValue == null)
        {
          var dataObject = Resources.Load(path, field.FieldType);
          if (dataObject != null)
          {
            field.SetValue(target, dataObject);
          }
#if UNITY_EDITOR
          else
          {
            Debug.LogWarning($"Could not find a data object of type {field.FieldType.Name} with name GameData in Resources. Creating a new one.");

            var newDataObject = ScriptableObject.CreateInstance(field.FieldType);
            string assetPath = $"Assets/Resources/{path}.asset";
            string directory = Path.GetDirectoryName(assetPath);

            if (!Directory.Exists(directory))
            {
              Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(newDataObject, assetPath);
            AssetDatabase.SaveAssets();
            field.SetValue(target, newDataObject);
          }
#endif
        }
      }
    }
  }
}