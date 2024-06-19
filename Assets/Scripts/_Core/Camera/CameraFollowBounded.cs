using UnityEngine;

public enum ScreenEdgeBehaviour
{
  Clamp,
  Teleport,
  ClampYTeleportX,
  ClampXTeleportY
}

public class CameraFollow : MonoBehaviour
{
  public Transform Player;
  [SerializeField] Vector2 boundsSize;
  public Vector3 minEdgePos;
  public Vector3 maxEdgePos;
  [SerializeField] Camera cam;
  public bool keepPlayerOnScreen = true;
  [SerializeField] bool refreshGizmos = false;
  [SerializeField] float smoothSpeed = 0f;
  public ScreenEdgeBehaviour screenEdgeBehaviour = ScreenEdgeBehaviour.Clamp;
  public bool followPlayer = true;

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
  void Start()
  {
    GetCameraBounds();
  }

  void Update()
  {
    KeepPlayerOnScreen(screenEdgeBehaviour);
  }

  void FixedUpdate()
  {
    if (!followPlayer || Player == null || cam == null)
    {
      return;
    }
    float halfCamWidth = cam.orthographicSize * cam.aspect;
    float halfCamHeight = cam.orthographicSize;

    float posX = Mathf.Clamp(Player.position.x, minEdgePos.x + halfCamWidth, maxEdgePos.x - halfCamWidth);
    float posY = Mathf.Clamp(Player.position.y, minEdgePos.y + halfCamHeight, maxEdgePos.y - halfCamHeight);
    Vector3 targetPos = new(posX, posY, cam.transform.position.z);
    cam.transform.position = smoothSpeed == 0 ? targetPos : Vector3.Lerp(transform.position, targetPos, smoothSpeed);
  }

  void KeepPlayerOnScreen(ScreenEdgeBehaviour behaviour)
  {
    if (Player == null || cam == null || !keepPlayerOnScreen)
    {
      return;
    }
    Vector3 pos = Player.position;

    Vector3 minCameraPos = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    Vector3 maxCameraPos = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

    switch (behaviour)
    {
      case ScreenEdgeBehaviour.Clamp:
        pos.x = Mathf.Clamp(pos.x, minCameraPos.x, maxCameraPos.x);
        pos.y = Mathf.Clamp(pos.y, minCameraPos.y, maxCameraPos.y);
        break;
      case ScreenEdgeBehaviour.Teleport:
        if (pos.x < minEdgePos.x)
        {
          pos.x = maxEdgePos.x;
        }
        else if (pos.x > maxEdgePos.x)
        {
          pos.x = minEdgePos.x;
        }
        if (pos.y < minEdgePos.y)
        {
          pos.y = maxEdgePos.y;
        }
        else if (pos.y > maxEdgePos.y)
        {
          pos.y = minEdgePos.y;
        }
        break;
      case ScreenEdgeBehaviour.ClampYTeleportX:
        if (pos.x < minCameraPos.x)
        {
          pos.x = maxCameraPos.x;
        }
        else if (pos.x > maxCameraPos.x)
        {
          pos.x = minCameraPos.x;
        }
        pos.y = Mathf.Clamp(pos.y, minCameraPos.y, maxCameraPos.y);
        break;
      case ScreenEdgeBehaviour.ClampXTeleportY:
        pos.x = Mathf.Clamp(pos.x, minCameraPos.x, maxCameraPos.x);
        if (pos.y < minCameraPos.y)
        {
          pos.y = maxCameraPos.y;
        }
        else if (pos.y > maxCameraPos.y)
        {
          pos.y = minCameraPos.y;
        }
        break;
    }

    Player.position = pos;
  }

  void GetCameraBounds()
  {
    AspectRatioEnforcer aspectRatioEnforcer = GetComponent<AspectRatioEnforcer>();
    float targetAspect = aspectRatioEnforcer != null ? aspectRatioEnforcer.targetAspect : cam.aspect;

    if (!boundsSize.Equals(Vector2.zero))
    {
      minEdgePos = transform.position + new Vector3(-boundsSize.x / 2, -boundsSize.y / 2, 0);
      maxEdgePos = transform.position + new Vector3(boundsSize.x / 2, boundsSize.y / 2, 0);
    }
    else
    {
      if (cam == null)
      {
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
        {
          // fallback to main camera
          cam = Camera.main;
        }
      }
      // Get the camera's size
      float height = cam.orthographicSize * 2;
      float width = height * targetAspect;

      // Calculate the bounds for the edges of the camera
      minEdgePos = transform.position - new Vector3(width / 2, height / 2, 0);
      maxEdgePos = transform.position + new Vector3(width / 2, height / 2, 0);
      boundsSize = new Vector2(Mathf.Abs(minEdgePos.x - maxEdgePos.x), Mathf.Abs(minEdgePos.y - maxEdgePos.y));
    }
  }

  void OnDrawGizmos()
  {
    if (refreshGizmos)
    {
      GetCameraBounds();
      refreshGizmos = false;
    }
    // Draw a red line box representing the camera bounds
    Gizmos.color = Color.red;
    Gizmos.DrawLine(new Vector3(minEdgePos.x, minEdgePos.y, 0), new Vector3(maxEdgePos.x, minEdgePos.y, 0));
    Gizmos.DrawLine(new Vector3(maxEdgePos.x, minEdgePos.y, 0), new Vector3(maxEdgePos.x, maxEdgePos.y, 0));
    Gizmos.DrawLine(new Vector3(maxEdgePos.x, maxEdgePos.y, 0), new Vector3(minEdgePos.x, maxEdgePos.y, 0));
    Gizmos.DrawLine(new Vector3(minEdgePos.x, maxEdgePos.y, 0), new Vector3(minEdgePos.x, minEdgePos.y, 0));
  }
}
