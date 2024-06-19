using System.Collections.Generic;
using System;
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
  Dictionary<Button, ActionState> actionStates = new Dictionary<Button, ActionState>();
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
  void Update()
  {
    foreach (Button action in Enum.GetValues(typeof(Button)))
    {

      if (!actionStates.ContainsKey(action))
      {
        actionStates[action] = new ActionState();
      }

      var state = actionStates[action];

      if (_inputHandler.IsButtonPressed(action))
      {
        Log($"{action} button pressed.");
        state.WasPressed = true;
      }

      state.IsPressed = _inputHandler.IsButtonHeld(action);

      if (state.IsPressed && state.WasPressed && !state.WasHeld)
      {
        state.HoldTimer += Time.deltaTime;
        if (state.HoldTimer < timeBeforeButtonHeldLog) continue;
        Log($"{action} button held.");
        state.WasHeld = true;
      }

      if (state.WasPressed && !state.IsPressed)
      {
        Log($"{action} button released.");
        state.WasPressed = false;
        state.WasHeld = false;
        state.HoldTimer = 0;
      }
    }

    // Horizontal axis
    horizontalAxis = _inputHandler.GetAxisHorizontal();
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

    // Vertical axis
    verticalAxis = _inputHandler.GetVerticalAxis();
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
}