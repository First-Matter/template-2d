using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : EventDrivenBehaviour
{
  public bool Pause;
  public GameObject PausePanel;
  public GameObject SettingsPanel;
  [Subscribe][SerializeField] private PauseEventChannel pauseEventChannel;
  [Data][SerializeField] private GameData gameData;

  private void OnEnable()
  {
    pauseEventChannel.RegisterEvent(OnPauseEvent);
  }

  private void OnDisable()
  {
    pauseEventChannel.UnRegisterEvent(OnPauseEvent);
  }

  private void OnPauseEvent(bool isPaused)
  {
    Pause = isPaused;
    if (Pause)
    {
      if (PausePanel != null)
        PausePanel.SetActive(true);
      if (SettingsPanel != null && !gameData.isSelectingUpgrade)
        SettingsPanel.SetActive(true);
      Time.timeScale = 0;
    }
    else
    {
      if (PausePanel != null)
        PausePanel.SetActive(false);
      if (SettingsPanel != null && SettingsPanel.activeSelf)
        SettingsPanel.SetActive(false);
      Time.timeScale = 1;
    }
  }
}
