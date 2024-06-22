using UnityEngine;
public class BaseData : ScriptableObject
{
  protected virtual void OnEnable()
  {
    ScriptableObjectAssigner.AssignEventChannels(this);
    ScriptableObjectAssigner.AssignDataObjects(this);
  }
}