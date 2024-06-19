using UnityEngine;

public class InputProvider : MonoBehaviour, IProvider<IPlayerInput>
{
  [SerializeField] private PlayerInput _inputHandler;
  private void Awake()
  {
    IPlayerInput inputHandler = _inputHandler;
    ServiceLocator.RegisterService(inputHandler);
  }

  public IPlayerInput Get()
  {
    return _inputHandler;
  }
}