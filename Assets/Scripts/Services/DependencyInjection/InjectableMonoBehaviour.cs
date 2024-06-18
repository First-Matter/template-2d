using UnityEngine;

public class InjectableMonoBehaviour : MonoBehaviour
{
  protected virtual void Start()
  {
    DependencyInjector.InjectDependencies(this);
  }
}