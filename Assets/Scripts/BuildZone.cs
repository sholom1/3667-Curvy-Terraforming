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
    public int scaleScalar;
    private HashSet<Curve> curves = new HashSet<Curve>();
    private AnchorPoint[] staticAnchors;

    private void Start()
    {
        staticAnchors = GetComponentsInChildren<AnchorPoint>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && CurveManager.instance != null)
        {
            CurveManager.instance.isInBuildZone = true;
            if (LastBuildZone != this && LastBuildZone != null)
            {
                CurveManager.instance.OnToggleBuildMode.RemoveListener(LastBuildZone.ToggleBuildMode);
                CurveManager.instance.OnSelectCurve.RemoveListener(LastBuildZone.addCurve);
            }
            LastBuildZone = this;
            CurveManager.instance.OnToggleBuildMode.AddListener(ToggleBuildMode);
            CurveManager.instance.OnSelectCurve.AddListener(addCurve);
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
        int speed = value ? scaleScalar : -scaleScalar;
        StopAllCoroutines();
        foreach (Curve curve in curves)
        {
            if (curve != null)
                ScaleCurve(speed, curve);
        }
        foreach (AnchorPoint anchor in staticAnchors)
        {
            if (anchor != null)
                StartCoroutine(ScaleAnchor(speed, anchor));
        }
    }
    private void addCurve(Curve curve)
    {
        if (!curves.Contains(curve))
        {
            ScaleCurve(scaleScalar, curve);
            curves.Add(curve);
        }
    }
    private void ScaleCurve (int speed, Curve curve)
    {
        System.Array.ForEach(curve.GetComponentsInChildren<AnchorPoint>(), anchor => StartCoroutine(ScaleAnchor(speed, anchor)));
    }
    private IEnumerator ScaleAnchor(int speed, AnchorPoint anchor)
    {
        Vector2 targetScale = new Vector2(.5f, .5f);
        targetScale = speed > 0 ? targetScale * speed : targetScale;
        while ((Vector2)anchor.transform.localScale != targetScale)
        {
            anchor.transform.localScale = Vector2.MoveTowards(anchor.transform.localScale, targetScale, Mathf.Abs(speed) * Time.deltaTime);
            yield return null;
        }
    }
}
