using UnityEngine;

public class PlayerController : EventDrivenBehaviour
{

  [SerializeField] private float moveSpeed = 5f;
  [Listen(Channel.AxisChannel)][SerializeField] private IListen<Vector2> inputAxisChannel;
  [Listen(Channel.ButtonPressedChannel)][SerializeField] private IListen<Button> buttonPressedChannel;
  [Listen(Channel.ButtonHeldChannel)][SerializeField] private IListen<Button> buttonHeldChannel;
  [Listen(Channel.SoundChannel)][SerializeField] private IBroadcast<Sound> playSoundChannel;
  [Data(Repository.SoundRepository)][SerializeField] private SoundRepository SoundRepository;

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
        PlaySound("Grenade");
        break;
      case Button.Fire:
        PlaySound("Zap");
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
  private void PlaySound(string soundName)
  {
    Sound grenadeSound = SoundRepository.GetSound(soundName);
    playSoundChannel.Invoke(grenadeSound);
  }
}
