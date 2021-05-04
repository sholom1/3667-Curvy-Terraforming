using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CurveManager : MonoBehaviour
{
    public static CurveManager instance;
    public List<Curve> ActiveCurves;
    public bool isPlacingCurve;
    public UnityEvent<bool> OnToggleBuildMode;
    public bool isInBuildZone;
    private Curve SelectedCurve;
    private new Camera camera;
    [SerializeField]
    private GameObject ButtonContainter;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
        camera = Camera.main;
    }
    private void Update()
    {
        if (isPlacingCurve && Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Placing Curve");
            SelectedCurve.edgeCollider.enabled = true;
            SelectedCurve = null;
            isPlacingCurve = false;
        }
        if (SelectedCurve != null)
        {
            Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
            SelectedCurve.transform.position = mouseWorldPos;
        }
        if (isInBuildZone && Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildMode();
        }

    }

    public void ToggleBuildMode()
    {
        ToggleBuildMode(ButtonContainter.activeInHierarchy);
    }
    public void ToggleBuildMode(bool value)
    {
        if (SelectedCurve != null)
            Destroy(SelectedCurve);
        ButtonContainter.SetActive(!value);
        OnToggleBuildMode.Invoke(!value);
    }

    public void SpawnCurve(Curve curve)
    {
        SetSelectedCurve(Instantiate(curve, camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity));
    }
    public void SetSelectedCurve(Curve curve)
    {
        if (SelectedCurve != null)
            Destroy(SelectedCurve);
        SelectedCurve = curve;
        isPlacingCurve = true;
    }
}
