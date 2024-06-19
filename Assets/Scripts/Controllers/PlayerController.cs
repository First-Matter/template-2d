using UnityEngine;

public class PlayerController : InjectableMonoBehaviour
{
  [Inject] private IPlayerInput inputHandler;
  [SerializeField] private float moveSpeed = 5f;

  private void OnEnable()
  {
    inputHandler.RegisterMoveEvent(HandleMove);
    inputHandler.RegisterButtonPressEvent(Button.Jump, HandleJumpPress);
    inputHandler.RegisterButtonHoldEvent(Button.Fire, HandleFireHold);
  }

  private void OnDisable()
  {
    inputHandler.UnRegisterMoveEvent(HandleMove);
    inputHandler.UnRegisterButtonPressEvent(Button.Jump, HandleJumpPress);
    inputHandler.UnRegisterButtonHoldEvent(Button.Fire, HandleFireHold);
  }

  private void HandleMove(Vector2 direction)
  {
    Vector3 move = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    transform.position += move;
  }

  private void HandleJumpPress()
  {
    // Implement jump logic here
  }

  private void HandleFireHold()
  {
    // Implement fire logic here
  }

  void Start()
  {
    if (inputHandler == null)
    {
      Debug.LogError("InputHandler service not found.");
    }
  }
}
