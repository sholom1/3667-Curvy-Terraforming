using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D), typeof(LineRenderer))]
[ExecuteInEditMode]
public abstract class Curve: MonoBehaviour
{
    public EdgeCollider2D edgeCollider;
    protected LineRenderer lineRenderer;

    [SerializeField]
    protected AnchorPoint _StartAnchor;
    [SerializeField]
    protected AnchorPoint _EndAnchor;

    protected virtual void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        if (CurveManager.instance != null)
            CurveManager.instance.ActiveCurves.Add(this);
        Compute();
    }
    public abstract void Compute();

    public abstract void UpdateCurveFunction();

    public AnchorPoint GetClosestAnchor(AnchorPoint other, float linkRange)
    {
        float distanceToStart = Vector2.Distance(other.transform.position, _StartAnchor.transform.position);
        float distanceToEnd = Vector2.Distance(other.transform.position, _EndAnchor.transform.position);
        if (distanceToStart < linkRange && distanceToStart < distanceToEnd)
            return _StartAnchor;
        if (distanceToEnd < linkRange && distanceToEnd < distanceToStart)
            return _EndAnchor;
        return null;
    }

    protected virtual void OnDestroy()
    {
        if (CurveManager.instance != null)
        CurveManager.instance.ActiveCurves.Remove(this);
    }
}
