using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : InjectableMonoBehaviour
{
  [Inject] private IPlayerInput inputHandler;
  [SerializeField] private float moveSpeed = 5f;
  // Start is called before the first frame update
  override protected void Start()
  {
    base.Start();
    if (inputHandler == null)
    {
      Debug.LogError("InputHandler service not found.");
    }
  }

  // Update is called once per frame
  void Update()
  {
    // Basic 2d top-down movement
    float horizontal = inputHandler.GetAxisHorizontal();
    float vertical = inputHandler.GetVerticalAxis();

    Vector3 movement = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
    transform.position += movement;
  }
}
