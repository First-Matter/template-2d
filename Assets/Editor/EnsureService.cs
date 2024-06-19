using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoInstantiateGameContext
{
  private static readonly string prefabName = "GameContext";
  private static readonly string prefabPath = $"Assets/Prefabs/{prefabName}/{prefabName}.prefab";

  static AutoInstantiateGameContext()
  {
    EditorSceneManager.sceneOpened += OnSceneOpened;
  }

  private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
  {
    EnsureServiceProviderInScene(prefabPath);
  }

  private static void EnsureServiceProviderInScene(string prefabPath)
  {
    if (GameObject.Find(prefabName) == null)
    {
      GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
      if (prefab != null)
      {
        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        if (instance != null)
        {
          instance.transform.SetSiblingIndex(0);
        }
        Debug.Log("ServiceProvider instantiated in the scene.");
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
      }
      else
      {
        Debug.LogError($"Prefab not found at path: {prefabPath}");
      }
    }
  }
}
