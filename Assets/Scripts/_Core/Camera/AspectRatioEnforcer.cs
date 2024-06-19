using UnityEngine;

public class AspectRatioEnforcer : MonoBehaviour
{
  public float targetAspect = 16f / 10f; // Target aspect ratio (16:10)
  public bool cropX = true;
  public bool cropY = true;
  [SerializeField] private Camera cam;

  void Awake()
  {
    if (cam == null)
    {
      cam = GetComponentInChildren<Camera>();
      if (cam == null)
      {
        cam = Camera.main;
      }
    }
  }

  private void Update()
  {
    float windowAspect = (float)Screen.width / (float)Screen.height;
    float scaleHeight = windowAspect / targetAspect;

    if (scaleHeight < 1.0f && cropY)
    {
      Rect rect = cam.rect;

      rect.width = 1.0f;
      rect.height = scaleHeight;
      rect.x = 0;
      rect.y = (1.0f - scaleHeight) / 2.0f;

      cam.rect = rect;
    }
    else if (scaleHeight >= 1.0f && cropX)
    {
      float scaleWidth = 1.0f / scaleHeight;

      Rect rect = cam.rect;

      rect.width = scaleWidth;
      rect.height = 1.0f;
      rect.x = (1.0f - scaleWidth) / 2.0f;
      rect.y = 0;

      cam.rect = rect;
    }
  }
}