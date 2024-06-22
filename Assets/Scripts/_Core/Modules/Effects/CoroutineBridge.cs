using UnityEngine;
using System.Collections;
public class CoroutineBridge : EventDrivenBehaviour
{
  [Subscribe][SerializeField] private CouroutineEventChannel couroutineEventChannel;
  private void OnEnable()
  {
    couroutineEventChannel.RegisterEvent(StartCouroutine);
  }
  private void OnDisable()
  {
    couroutineEventChannel.UnRegisterEvent(StartCouroutine);
  }
  public void StartCouroutine(IEnumerator couroutine)
  {
    StartCoroutine(couroutine);
  }
}