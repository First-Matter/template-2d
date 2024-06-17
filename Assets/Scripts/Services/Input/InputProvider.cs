using UnityEngine;

public class InputProvider : MonoBehaviour, IProvider<IPlayerInput>
{
  private IPlayerInput _inputHandler;

  private void Awake()
  {
    _inputHandler = new PlayerInput();
    ServiceLocator.RegisterService(_inputHandler);
  }

  public IPlayerInput Get()
  {
    return _inputHandler;
  }
}