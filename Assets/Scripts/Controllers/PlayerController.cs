using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerController : EventDrivenBehaviour
{

  [SerializeField] private float moveSpeed = 5f;
  [Subscribe(Mediator.Input)][SerializeField] private InputMediator input;
  [Subscribe(Channel.SoundChannel)][SerializeField] private SoundChannel playSoundChannel;
  [Subscribe(Channel.ScoreChannel)][SerializeField] private ScoreChannel scoreChannel;
  [Data][SerializeField] private GameData data;

  private void OnEnable()
  {
    input.RegisterButtonPressedAction(Button.Jump, HandleJumpPressed);
    input.RegisterButtonPressedAction(Button.Fire, HandleFirePressed);
    input.RegisterMoveAction(HandleMove);
    data.playerHealth.Initialize(100, 100);
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
    data.playerHealth.ReduceHealth(10);
  }
  private void PlaySound(RegisteredSound soundName)
  {
    playSoundChannel.Invoke(soundName);
  }
}
