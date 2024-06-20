using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioEventHandler))]
public class AudioEventHandlerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    // Manually draw the Sounds field
    SerializedProperty sounds = serializedObject.FindProperty("sounds");
    EditorGUILayout.PropertyField(sounds, true);

    // Draw the button
    if (GUILayout.Button("Generate Sound Enum"))
    {
      EnumGenerator.GenerateSoundEnum();
    }

    // Draw the rest of the fields
    DrawPropertiesExcluding(serializedObject, "sounds");
    serializedObject.ApplyModifiedProperties();
  }
}