using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
  /// Scrolling speed
  
  public Vector2 speed = new Vector2(2, 2);

  /// Moving direction
  public Vector2 direction = new Vector2(-1, 0);

  
  /// Movement should be applied to camera
  public bool isLinkedToCamera = false;

  void Update()
  {
    // Movement
    Vector3 movement = new Vector3(
      speed.x * direction.x, speed.y * direction.y, 0);

    movement *= Time.deltaTime;
    transform.Translate(movement);

    // Move the camera
    if (isLinkedToCamera)
    {
      Camera.main.transform.Translate(movement);
    }
  }
}
