using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerController : EventDrivenBehaviour
{

  [SerializeField] private float moveSpeed = 5f;
  [Subscribe][SerializeField] private InputMediator input;
  [Subscribe][SerializeField] private SoundChannel playSoundChannel;
  [Data][SerializeField] private GameData data;

  private void OnEnable()
  {
    input.RegisterButtonPressedAction(Button.Jump, HandleJumpPressed);
    input.RegisterButtonPressedAction(Button.Fire, HandleFirePressed);
    input.RegisterButtonHeldAction(Button.Dash, HandleDashHeld);
    input.RegisterMoveAction(HandleMove);
    data.playerHealth.Initialize(100, 100);
    data.playerMana.Initialize(100, 100);
    data.playerExp.Initialize(0, 100);
  }

  private void OnDisable()
  {
    input.UnRegisterButtonPressedAction(Button.Jump, HandleJumpPressed);
    input.UnRegisterButtonPressedAction(Button.Fire, HandleFirePressed);
    input.UnRegisterButtonHeldAction(Button.Dash, HandleDashHeld);
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
    data.playerHealth.Reduce(10);
    data.pauseController.TogglePause();
  }
  private void HandleFirePressed()
  {
    PlaySound(RegisteredSound.Zap);
    data.scoreData.AddScore(10);
    data.playerMana.Reduce(5);
  }
  private void HandleDashHeld()
  {
    data.playerExp.Increase(1);
  }
  private void PlaySound(RegisteredSound soundName)
  {
    playSoundChannel.Invoke(soundName);
  }
}
