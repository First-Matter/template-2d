using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerController : EventDrivenBehaviour
{

  [SerializeField] private float moveSpeed = 5f;
  [Listen(Mediator.Input)][SerializeField] private InputMediator input;
  [Listen(Channel.SoundChannel)][SerializeField] private IBroadcast<Sound> playSoundChannel;
  [Data(Repository.SoundRepository)][SerializeField] private SoundRepository SoundRepository;

  private void OnEnable()
  {
    input.RegisterButtonPressedAction(Button.Jump, HandleJumpPressed);
    input.RegisterButtonPressedAction(Button.Fire, HandleFirePressed);
    input.RegisterMoveAction(HandleMove);
  }

  private void OnDisable()
  {
    input.UnRegisterButtonPressedAction(Button.Jump, HandleJumpPressed);
    input.UnRegisterButtonPressedAction(Button.Fire, HandleFirePressed);
    input.UnRegisterMoveAction(HandleMove);
  }

  private void HandleMove(Vector2 direction)
  {
    Vector3 move = new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    transform.position += move;
  }
  private void HandleJumpPressed()
  {
    PlaySound("Grenade");
  }
  private void HandleFirePressed()
  {
    PlaySound("Zap");
  }
  private void PlaySound(string soundName)
  {
    Sound grenadeSound = SoundRepository.GetSound(soundName);
    playSoundChannel.Invoke(grenadeSound);
  }
}
