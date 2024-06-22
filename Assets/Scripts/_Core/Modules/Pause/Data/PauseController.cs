using UnityEngine;

[CreateAssetMenu(fileName = "PauseController", menuName = "Controllers/PauseController")]
public class PauseController : BaseData
{
  [Subscribe][SerializeField] private PauseEventChannel pauseEventChannel;
  private bool isPaused = false;
  public void TogglePause()
  {
    isPaused = !isPaused;
    pauseEventChannel.Invoke(isPaused);
  }

  public void SetPause(bool pauseState)
  {
    isPaused = pauseState;
    pauseEventChannel.Invoke(isPaused);
  }
}
