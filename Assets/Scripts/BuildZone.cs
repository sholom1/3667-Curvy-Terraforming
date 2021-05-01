using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider2D))]
public class BuildZone : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera BuildView;
    [SerializeField]
    private int Priority;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && CurveManager.instance != null)
        {
            CurveManager.instance.isInBuildZone = true;
            CurveManager.instance.OnToggleBuildMode.AddListener(ToggleBuildMode);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && CurveManager.instance != null)
        {
            CurveManager.instance.isInBuildZone = false;
            CurveManager.instance.OnToggleBuildMode.RemoveListener(ToggleBuildMode);
        }
    }
    private void ToggleBuildMode(bool value)
    {
        BuildView.Priority = value ? Priority : -Priority;
    }
}
