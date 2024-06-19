using UnityEngine;

/// <summary>
/// Base class for MonoBehaviours with dependency injection.
/// Injects dependencies into fields marked with the <see cref="InjectAttribute">[Inject]</see> attribute.
/// </summary>
public class InjectableMonoBehaviour : MonoBehaviour
{
  /// <summary>
  /// Called when the script instance is being loaded.
  /// Injects dependencies into fields marked with the <see cref="InjectAttribute">[Inject]</see> attribute.
  /// </summary>
  protected virtual void Awake()
  {
    DependencyInjector.InjectDependencies(this);
  }
}