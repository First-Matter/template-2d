using UnityEngine;

public class PlayerController : EventDrivenBehaviour
{

  [SerializeField] private float moveSpeed = 5f;
  [Listen("AxisChannel")][SerializeField] private EventChannel<Vector2> inputAxisChannel;
  [Listen("ButtonPressedChannel")][SerializeField] private EventChannel<Button> buttonPressedChannel;
  [Listen("ButtonHeldChannel")][SerializeField] private EventChannel<Button> buttonHeldChannel;

  private void OnEnable()
  {
    inputAxisChannel.RegisterEvent(HandleMove);
    buttonPressedChannel.RegisterEvent(HandleButtonPress);
    buttonHeldChannel.RegisterEvent(HandleButtonHold);
  }

  private void OnDisable()
  {
    inputAxisChannel.UnRegisterEvent(HandleMove);
    buttonPressedChannel.UnRegisterEvent(HandleButtonPress);
    buttonHeldChannel.UnRegisterEvent(HandleButtonHold);
  }

  private void HandleMove(Vector2 direction)
  {
    Vector3 move = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    transform.position += move;
  }

  private void HandleButtonPress(Button button)
  {
    switch (button)
    {
      case Button.Jump:
        Debug.Log("Player jumped!");
        break;
      case Button.Fire:
        Debug.Log("Player fired!");
        break;
    }
  }
  private void HandleButtonHold(Button button)
  {
    if (button == Button.Fire)
    {
      Debug.Log("Player is holding fire button!");
    }
  }
}
