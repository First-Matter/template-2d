using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputMediator))]
public class InputMediatorInspector : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    InputMediator inputMediator = (InputMediator)target;

    if (GUILayout.Button("Initialize Channels"))
    {
      InitializeChannels(inputMediator);
    }
  }

  private void InitializeChannels(InputMediator inputMediator)
  {
    if (inputMediator.inputAxisChannel == null)
    {
      inputMediator.inputAxisChannel = CreateChannel<AxisChannel>(Channel.AxisChannel);
    }
    if (inputMediator.buttonPressedChannel == null)
    {
      inputMediator.buttonPressedChannel = CreateChannel<ButtonChannel>(Channel.ButtonPressedChannel);
    }
    if (inputMediator.buttonHeldChannel == null)
    {
      inputMediator.buttonHeldChannel = CreateChannel<ButtonChannel>(Channel.ButtonHeldChannel);
    }
    if (inputMediator.buttonReleasedChannel == null)
    {
      inputMediator.buttonReleasedChannel = CreateChannel<ButtonChannel>(Channel.ButtonReleasedChannel);
    }

    EditorUtility.SetDirty(inputMediator);
  }

  private T CreateChannel<T>(string channelName) where T : ScriptableObject
  {
    T channel = ScriptableObject.CreateInstance<T>();
    AssetDatabase.CreateAsset(channel, $"Assets/Resources/EventChannels/{channelName}.asset");
    AssetDatabase.SaveAssets();
    return channel;
  }
}
