using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject BallPrefab;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(BallPrefab, mouseWorldPos, Quaternion.identity);
        }
    }
}
