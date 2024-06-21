using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerController : EventDrivenBehaviour
{

  [SerializeField] private float moveSpeed = 5f;
  [Listen(Mediator.Input)][SerializeField] private InputMediator input;
  [Listen(Channel.SoundChannel)][SerializeField] private SoundChannel playSoundChannel;
  [Listen(Channel.ScoreChannel)][SerializeField] private ScoreChannel scoreChannel;
  [Data][SerializeField] private GameData GameData;

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
    PlaySound(RegisteredSound.Grenade);
  }
  private void HandleFirePressed()
  {
    PlaySound(RegisteredSound.Zap);
    scoreChannel.Invoke(10);
  }
  private void PlaySound(RegisteredSound soundName)
  {
    playSoundChannel.Invoke(soundName);
  }
}
