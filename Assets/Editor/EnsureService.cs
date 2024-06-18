using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoInstantiateServiceProviders
{
  private static readonly string ServiceProvidersPrefabPath = "Assets/Prefabs/Services/ServiceProvider.prefab";

  static AutoInstantiateServiceProviders()
  {
    EditorSceneManager.sceneOpened += OnSceneOpened;
  }

  private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
  {
    EnsureServiceProviderInScene(ServiceProvidersPrefabPath);
  }

  private static void EnsureServiceProviderInScene(string prefabPath)
  {
    if (GameObject.Find("ServiceProvider") == null)
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
