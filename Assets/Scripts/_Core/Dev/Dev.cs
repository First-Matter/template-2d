using System.Collections.Generic;
using UnityEngine;

public class Dev : InjectableMonoBehaviour
{
  [Inject] private IGameAudio _audioHandler;
  [Inject] private IPlayerInput _inputHandler;
  public bool enableLogging = false;
  public float timeBeforeButtonHeldLog = 0.2f;
  private static Dev Instance;
  private List<string> _logs = new List<string>();

  public class ActionState
  {
    public bool WasPressed { get; set; }
    public bool IsPressed { get; set; }
    public bool WasHeld { get; set; }
    public float HoldTimer { get; set; }
  }

  private Dictionary<Button, ActionState> actionStates = new Dictionary<Button, ActionState>();
  private float horizontalAxis;
  private float lastHorizontalAxis;
  private float verticalAxis;
  private float lastVerticalAxis;

  protected override void Awake()
  {
    base.Awake();
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void Start()
  {
    if (_audioHandler == null)
    {
      Debug.LogError("Audio service not found.");
    }
    if (_inputHandler == null)
    {
      Debug.LogError("Input service not found.");
    }

    // Register events
    _inputHandler.RegisterButtonPressEvent(Button.Jump, () => HandleButtonPress(Button.Jump));
    _inputHandler.RegisterButtonPressEvent(Button.Fire, () => HandleButtonPress(Button.Fire));
    _inputHandler.RegisterButtonHoldEvent(Button.Jump, () => HandleButtonHold(Button.Jump));
    _inputHandler.RegisterButtonHoldEvent(Button.Fire, () => HandleButtonHold(Button.Fire));
    _inputHandler.RegisterButtonReleaseEvent(Button.Jump, () => HandleButtonRelease(Button.Jump));
    _inputHandler.RegisterButtonReleaseEvent(Button.Fire, () => HandleButtonRelease(Button.Fire));
    _inputHandler.RegisterMoveEvent(HandleMove);
  }
  public void OnDisable()
  {
    _inputHandler.UnRegisterButtonPressEvent(Button.Jump, () => HandleButtonPress(Button.Jump));
    _inputHandler.UnRegisterButtonPressEvent(Button.Fire, () => HandleButtonPress(Button.Fire));
    _inputHandler.UnRegisterButtonHoldEvent(Button.Jump, () => HandleButtonHold(Button.Jump));
    _inputHandler.UnRegisterButtonHoldEvent(Button.Fire, () => HandleButtonHold(Button.Fire));
    _inputHandler.UnRegisterButtonReleaseEvent(Button.Jump, () => HandleButtonRelease(Button.Jump));
    _inputHandler.UnRegisterButtonReleaseEvent(Button.Fire, () => HandleButtonRelease(Button.Fire));
    _inputHandler.UnRegisterMoveEvent(HandleMove);
  }

  private void HandleButtonPress(Button button)
  {
    Log($"{button} button pressed.");
    if (!actionStates.ContainsKey(button))
    {
      actionStates[button] = new ActionState();
    }
    actionStates[button].WasPressed = true;
  }

  private void HandleButtonHold(Button button)
  {
    var state = actionStates[button];
    if (state.WasPressed && !state.WasHeld)
    {
      state.HoldTimer += Time.deltaTime;
      if (state.HoldTimer >= timeBeforeButtonHeldLog)
      {
        Log($"{button} button held.");
        state.WasHeld = true;
      }
    }
  }

  private void HandleButtonRelease(Button button)
  {
    if (actionStates.ContainsKey(button) && actionStates[button].WasPressed)
    {
      var state = actionStates[button];
      Log($"{button} button released.");
      state.WasPressed = false;
      state.WasHeld = false;
      state.HoldTimer = 0;
    }
  }

  private void HandleMove(Vector2 direction)
  {
    horizontalAxis = direction.x;
    verticalAxis = direction.y;

    if (horizontalAxis > 0 && lastHorizontalAxis <= 0)
    {
      Log("Started moving right.");
    }
    else if (horizontalAxis < 0 && lastHorizontalAxis >= 0)
    {
      Log("Started moving left.");
    }
    else if (horizontalAxis == 0)
    {
      if (lastHorizontalAxis > 0)
      {
        Log("Stopped moving right.");
      }
      else if (lastHorizontalAxis < 0)
      {
        Log("Stopped moving left.");
      }
    }
    lastHorizontalAxis = horizontalAxis;

    if (verticalAxis > 0 && lastVerticalAxis <= 0)
    {
      Log("Started moving up.");
    }
    else if (verticalAxis < 0 && lastVerticalAxis >= 0)
    {
      Log("Started moving down.");
    }
    else if (verticalAxis == 0)
    {
      if (lastVerticalAxis > 0)
      {
        Log("Stopped moving up.");
      }
      else if (lastVerticalAxis < 0)
      {
        Log("Stopped moving down.");
      }
    }
    lastVerticalAxis = verticalAxis;
  }

  public static void Log(string message)
  {
    if (Instance.enableLogging)
      Debug.Log(message);
  }

  public static void LogOnce(string message)
  {
    if (Instance.enableLogging && !Instance._logs.Contains(message))
    {
      Debug.Log(message);
      Instance._logs.Add(message);
    }
  }

  public static void LogWarning(string message)
  {
    if (Instance.enableLogging && !Instance._logs.Contains(message))
      Debug.LogWarning(message);
  }
}
