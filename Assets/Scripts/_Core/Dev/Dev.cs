using System.Collections.Generic;
using UnityEngine;

public class Dev : EventDrivenBehaviour
{
  // [Inject] private IGameAudio _audioHandler;
  [Listen("AxisChannel")][SerializeField] private EventChannel<Vector2> inputAxisChannel;
  [Listen("ButtonPressedChannel")][SerializeField] private EventChannel<Button> buttonPressedChannel;
  [Listen("ButtonHeldChannel")][SerializeField] private EventChannel<Button> buttonHeldChannel;
  [Listen("ButtonReleasedChannel")][SerializeField] private EventChannel<Button> buttonReleasedChannel;
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

  void Awake()
  {
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
    // if (_audioHandler == null)
    // {
    //   Debug.LogError("Audio service not found.");
    // }

    // Register events
    buttonPressedChannel.RegisterEvent(HandleButtonPress);
    buttonHeldChannel.RegisterEvent(HandleButtonHold);
    buttonReleasedChannel.RegisterEvent(HandleButtonRelease);
    inputAxisChannel.RegisterEvent(HandleMove);
  }

  private void OnDisable()
  {
    // Unregister events
    buttonPressedChannel.UnRegisterEvent(HandleButtonPress);
    buttonHeldChannel.UnRegisterEvent(HandleButtonHold);
    buttonReleasedChannel.UnRegisterEvent(HandleButtonRelease);
    inputAxisChannel.UnRegisterEvent(HandleMove);
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
    if (actionStates.ContainsKey(button))
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
