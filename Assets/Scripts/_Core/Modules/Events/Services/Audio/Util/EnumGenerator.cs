#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

public class EnumGenerator : Editor
{
  public static void GenerateSoundEnum()
  {
#if UNITY_EDITOR
    // Get sound names from the SoundRepository
    AudioEventHandler audioHandler = FindObjectOfType<AudioEventHandler>();

    // Start building the enum string
    StringBuilder enumString = new StringBuilder();
    enumString.AppendLine("public enum SoundNames");
    enumString.AppendLine("{");
    HashSet<string> soundNames = new HashSet<string>();

    // Add each unique sound name to the HashSet
    foreach (Sound sound in audioHandler.sounds)
    {
      soundNames.Add(sound.name);
    }

    // Add each sound name to the enum
    foreach (string soundName in soundNames)
    {
      enumString.AppendLine($"    {soundName},");
    }

    enumString.AppendLine("}");

    // Write the enum string to a file
    string path = Path.Combine(Application.dataPath, "Scripts", "_Core", "Modules", "Data", "Enums", "SoundNames.cs");
    File.WriteAllText(path, enumString.ToString());
    string relativePath = "Assets" + path.Substring(Application.dataPath.Length);

    // Replace backslashes with forward slashes
    string clickablePath = relativePath.Replace("\\", "/");
    Debug.Log("SoundNames enum generated successfully: \n" +
    $"<a href=\"{clickablePath}\" line=\"1\">{clickablePath}:1</a>");
#else
    Debug.LogError("This method should only be called in the Unity editor.");
#endif
  }
}