using UnityEngine;

[CreateAssetMenu(fileName = "PauseController", menuName = "Controllers/PauseController")]
public class PauseController : BaseData
{
  [Subscribe][SerializeField] private PauseEventChannel pauseEventChannel;
  public static bool isPaused = false;
  public float textFlashSpeed = 1f;
  public string pauseText = "Paused";
  public void TogglePause()
  {
    Debug.Log("PauseController: TogglePause()");
    isPaused = !isPaused;
    pauseEventChannel.Invoke(isPaused);
  }

  public void SetPause(bool pauseState)
  {
    isPaused = pauseState;
    pauseEventChannel.Invoke(isPaused);
  }
}
