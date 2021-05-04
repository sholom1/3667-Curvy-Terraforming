using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider2D))]
public class BuildZone : MonoBehaviour
{
    public static BuildZone LastBuildZone;
    [SerializeField]
    private CinemachineVirtualCamera BuildView;
    [SerializeField]
    private int Priority;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && CurveManager.instance != null)
        {
            CurveManager.instance.isInBuildZone = true;
            if (LastBuildZone != this && LastBuildZone != null)
                CurveManager.instance.OnToggleBuildMode.RemoveListener(LastBuildZone.ToggleBuildMode);
            LastBuildZone = this;
            CurveManager.instance.OnToggleBuildMode.AddListener(ToggleBuildMode);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && CurveManager.instance != null)
        {
            CurveManager.instance.isInBuildZone = false;
        }
    }
    private void ToggleBuildMode(bool value)
    {
        BuildView.Priority = value ? Priority : -Priority;
    }
}
