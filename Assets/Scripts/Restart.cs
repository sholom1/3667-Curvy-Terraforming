using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset;
    public void Reload()
    {
        if (BuildZone.LastBuildZone != null && PlayerMovement.instance != null && CurveManager.instance != null)
        {
            PlayerMovement.instance.transform.position = (Vector2)BuildZone.LastBuildZone.transform.position + offset;
            PlayerMovement.instance.transform.right = Vector2.right;
            CurveManager.instance.ToggleBuildMode(false);
        }
    }
}
