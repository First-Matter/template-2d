using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : EventDrivenBehaviour
{
  public bool Pause;
  public GameObject PausePanel;
  [Subscribe][SerializeField] private PauseEventChannel pauseEventChannel;

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
      Time.timeScale = 0;
    }
    else
    {
      if (PausePanel != null)
        PausePanel.SetActive(false);
      Time.timeScale = 1;
    }
  }
}
