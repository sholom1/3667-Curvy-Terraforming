using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D), typeof(LineRenderer))]
public abstract class Curve: MonoBehaviour
{
    protected EdgeCollider2D edgeCollider;
    protected LineRenderer lineRenderer;

    protected virtual void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        Compute();
    }
    public abstract void Compute();

    public abstract string GetCurveFunction();
}
