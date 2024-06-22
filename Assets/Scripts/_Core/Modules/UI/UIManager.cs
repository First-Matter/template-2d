using UnityEngine;

public class ManageUI : EventDrivenBehaviour
{
  [Data][SerializeField] private GameData _gameData;
  [Header("Text Formatting")]
  [SerializeField] private string pauseText = "Paused";
  [SerializeField] private float textFlashSpeed = 1f;
  [SerializeField] private string scoreTextFormat = "Score: {0}\nHigh Score: {1}";
  [SerializeField] private string levelTextFormat = "Level: {0}";
  [Header("Bar Settings")]
  [SerializeField] private Color healthBarColor = Color.red;
  [SerializeField] private Color manaBarColor = Color.blue;
  [SerializeField] private Color expBarColor = Color.green;

  private void Awake()
  {
    InitializeUISettings();
    EnsureMainCameraOverlayCanvas();
  }

  protected override void OnValidate()
  {
    base.OnValidate();
    InitializeUISettings();
    ValidateBarUI();
  }

  private void InitializeUISettings()
  {
    ManageScoreUI();
    ManageBarColors();
    ManageLevelUI();
    ManagePauseUI();
  }

  private void ManageScoreUI()
  {
    if (_gameData != null && _gameData.scoreData != null)
      _gameData.scoreData.ScoreTextFormat = scoreTextFormat;
  }
  private void ManagePauseUI()
  {
    if (_gameData != null && _gameData.pauseController != null)
    {
      _gameData.pauseController.pauseText = pauseText;
      _gameData.pauseController.textFlashSpeed = textFlashSpeed;
    }
  }

  private void ManageBarColors()
  {
    _gameData.playerHealth.updateChannel.BarColor = healthBarColor;
    _gameData.playerMana.updateChannel.BarColor = manaBarColor;
    _gameData.playerExp.updateChannel.BarColor = expBarColor;
  }

  private void ManageLevelUI()
  {
    _gameData.levelUpData.TextFormat = levelTextFormat;
  }

  private void ValidateBarUI()
  {
    BarUI[] barUIs = GetComponentsInChildren<BarUI>();
    foreach (var barUI in barUIs)
    {
      barUI.OnValidate();
    }
  }
  private void EnsureMainCameraOverlayCanvas()
  {
    if (Camera.main == null)
    {
      Debug.LogWarning("No main camera found. Ensure a camera is tagged as MainCamera.");
      return;
    }
    Canvas canvas = GetComponentInChildren<Canvas>();
    if (canvas.renderMode != RenderMode.ScreenSpaceCamera)
      canvas.renderMode = RenderMode.ScreenSpaceCamera;
    if (canvas.worldCamera == null || canvas.worldCamera != Camera.main)
      canvas.worldCamera = Camera.main;
  }
}
