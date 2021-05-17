using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurveManager : MonoBehaviour
{
    public static CurveManager instance;
    public List<Curve> ActiveCurves;
    public bool isPlacingCurve;
    public UnityEvent<bool> OnToggleBuildMode;
    public UnityEvent<Curve> OnSelectCurve;
    public bool isInBuildZone;
    private Curve SelectedCurve;
    private new Camera camera;
    [SerializeField]
    private GameObject ButtonContainter;
    [SerializeField]
    private LayerMask CurveLayer, AnchorLayer;
    public Button DeleteCurveButton, ToggleCurvePickerButton;
    public LinearCurveText LinearCurveText;
    public QuadraticCurveText QuadraticCurveText;
    public bool isInBuildMode => ButtonContainter.activeInHierarchy;
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
            DeselectCurve();
        }
        if (SelectedCurve != null)
        {
            Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
            SelectedCurve.transform.position = mouseWorldPos;
        }
        if (isInBuildZone)
        {
            if (Input.GetKeyDown(KeyCode.B))
                ToggleBuildMode();
            if (isInBuildMode && !isPlacingCurve && Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && TryGetCurve(out Curve hitCurve))
                SetSelectedCurve(hitCurve);
        }
    }

    public void ToggleBuildMode()
    {
        ToggleBuildMode(ButtonContainter.activeInHierarchy);
    }
    public void ToggleBuildMode(bool value)
    {
        if (SelectedCurve != null)
            Destroy(SelectedCurve.gameObject);
        ButtonContainter.SetActive(!value);
        OnToggleBuildMode.Invoke(!value);
    }
    public void SpawnCurve(Curve curve)
    {
        SetSelectedCurve(Instantiate(curve, camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity));
    }
    public void SetSelectedCurve(Curve curve)
    {
        if (SelectedCurve == curve) return;
        DeleteCurveButton.gameObject.SetActive(true);
        if (SelectedCurve != null)
            Destroy(SelectedCurve.gameObject);
        SelectedCurve = curve;
        SelectedCurve.UpdateCurveFunction();
        isPlacingCurve = true;
        AnimateCurveManager(false);
    }
    public void DeselectCurve()
    {
        DeleteCurveButton.gameObject.SetActive(false);
        AnimateCurveManager(true);
        if (SelectedCurve != null)
        {
            SelectedCurve.edgeCollider.enabled = true;
            SelectedCurve = null;
        }
        LinearCurveText.gameObject.SetActive(false);
        QuadraticCurveText.gameObject.SetActive(false);
        isPlacingCurve = false;
    }
    public void DeleteCurve()
    {
        if (SelectedCurve == null) return;
        ScoreManager.instance.SellCurve(SelectedCurve);
        Destroy(SelectedCurve.gameObject);
        DeselectCurve();
    }
    public void AnimateCurveManager(bool value)
    {
        ToggleCurvePickerButton.onClick.RemoveAllListeners();
        ToggleCurvePickerButton.onClick.AddListener(() => AnimateCurveManager(!value));
        ButtonContainter.GetComponent<Animator>().SetBool("Show", value);
    }
    public bool TryGetCurve(out Curve curve)
    {
        Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        curve = null;
        RaycastHit2D hitAnchor = Physics2D.Raycast(mouseWorldPos, camera.transform.forward, float.MaxValue, AnchorLayer);
        if (hitAnchor.collider != null) return false;
        RaycastHit2D hitCurve = Physics2D.Raycast(mouseWorldPos, camera.transform.forward, float.MaxValue, CurveLayer);
        if (hitCurve.collider != null && hitCurve.collider.TryGetComponent(out curve))
        {
            return true;
        }
        return false;
    }
}
