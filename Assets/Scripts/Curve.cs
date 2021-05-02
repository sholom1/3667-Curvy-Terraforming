using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D), typeof(LineRenderer))]
[ExecuteInEditMode]
public abstract class Curve: MonoBehaviour
{
    public EdgeCollider2D edgeCollider;
    protected LineRenderer lineRenderer;

    public AnchorPoint StartAnchor;
    public AnchorPoint EndAnchor;

    protected virtual void Awake()
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
        float distanceToStart = Vector2.Distance(other.transform.position, StartAnchor.transform.position);
        float distanceToEnd = Vector2.Distance(other.transform.position, EndAnchor.transform.position);
        if (distanceToStart < linkRange && distanceToStart < distanceToEnd)
            return StartAnchor;
        if (distanceToEnd < linkRange && distanceToEnd < distanceToStart)
            return EndAnchor;
        return null;
    }

    protected virtual void OnDestroy()
    {
        if (CurveManager.instance != null)
        CurveManager.instance.ActiveCurves.Remove(this);
    }
    public virtual void MarkStatic(bool value)
    {
        StartAnchor.isStatic = value;
        EndAnchor.isStatic = value;
    }
}
