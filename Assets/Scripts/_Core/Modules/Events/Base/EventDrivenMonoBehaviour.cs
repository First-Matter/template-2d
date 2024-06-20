using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Base class for MonoBehaviours with event channel and data ScriptableObject injection.
/// Automatically assigns event channels to fields marked with the <see cref="ListenAttribute">[Listen]</see> attribute
/// and data ScriptableObjects to fields marked with the <see cref="DataAttribute">[Data]</see> attribute.
/// </summary>
public class EventDrivenBehaviour : MonoBehaviour
{
  /// <summary>
  /// Called when the script is loaded or a value is changed in the inspector.
  /// Assigns event channels and data ScriptableObjects to fields marked with the respective attributes.
  /// </summary>
  protected virtual void OnValidate()
  {
    AssignEventChannels();
    AssignDataObjects();
  }

  /// <summary>
  /// Assigns event channels to fields marked with the <see cref="ListenAttribute">[Listen]</see> attribute.
  /// </summary>
  private void AssignEventChannels()
  {
    var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
      .Where(field => Attribute.IsDefined(field, typeof(ListenAttribute)));

    foreach (var field in fields)
    {
      var attribute = (ListenAttribute)Attribute.GetCustomAttribute(field, typeof(ListenAttribute));
      if (attribute != null)
      {
        var currentFieldValue = field.GetValue(this);
        if (currentFieldValue == null)
        {
          var channel = Resources.Load($"EventChannels/{attribute.ChannelName}", field.FieldType);
          if (channel != null)
          {
            field.SetValue(this, channel);
          }
#if UNITY_EDITOR
          else
          {
            Debug.LogWarning($"Could not find a channel of type {field.FieldType} with name {attribute.ChannelName} in Resources. Creating a new one.");

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
            AssetDatabase.CreateAsset(newChannel, $"Assets/Resources/EventChannels/{attribute.ChannelName}.asset");
            AssetDatabase.SaveAssets();
            field.SetValue(this, newChannel);
          }
#endif
        }
      }
    }
  }

  // / <summary>
  // / Assigns data ScriptableObjects to fields marked with the <see cref="DataAttribute">[Data]</see> attribute.
  // / </summary>
  private void AssignDataObjects()
  {
    var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
      .Where(field => Attribute.IsDefined(field, typeof(DataAttribute)));

    foreach (var field in fields)
    {
      var attribute = (DataAttribute)Attribute.GetCustomAttribute(field, typeof(DataAttribute));
      if (attribute != null)
      {
        var currentFieldValue = field.GetValue(this);
        if (currentFieldValue == null)
        {
          var dataObject = Resources.Load($"DataObjects/{attribute.DataName}", field.FieldType);
          if (dataObject != null)
          {
            field.SetValue(this, dataObject);
          }
#if UNITY_EDITOR
          else
          {
            Debug.LogWarning($"Could not find a data object of type {field.FieldType} with name {attribute.DataName} in Resources. Creating a new one.");

            var newDataObject = ScriptableObject.CreateInstance(field.FieldType);
            string path = $"Assets/Resources/DataObjects/{attribute.DataName}.asset";
            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
              Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(newDataObject, path);
            AssetDatabase.SaveAssets();
            field.SetValue(this, newDataObject);
          }
#endif
        }
      }
    }
  }
}
