using UnityEngine;

public class InjectableMonoBehaviour : MonoBehaviour
{
  protected virtual void Awake()
  {
    DependencyInjector.InjectDependencies(this);
  }
}