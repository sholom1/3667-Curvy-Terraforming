using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EdgeCollider2D), typeof(LineRenderer))]
public class QuadraticCurve : Curve
{
    [SerializeField]
    private AnchorPoint _MiddleAnchor;
    [SerializeField]
    [Range(10, 200)]
    private int _Iterations;

    private float curveLength;

    public override void Compute()
    {
        Vector2[] points = new Vector2[_Iterations];
        Vector2 startPos = _StartAnchor.transform.localPosition;
        Vector2 midPos = _MiddleAnchor.transform.localPosition;
        Vector2 endPos = _EndAnchor.transform.localPosition;
        points[0] = startPos;
        points[_Iterations - 1] = endPos;
        curveLength = 0;
        for (int i = 1; i < _Iterations - 1; i++)
        {
            points[i] = CurveFunctions.Quadratic(startPos, midPos, endPos, (float)i / _Iterations);
            curveLength += Vector2.Distance(points[i - 1], points[i]);
        }
        Debug.Log(curveLength);
        edgeCollider.points = points;
        lineRenderer.positionCount = _Iterations;
        lineRenderer.SetPositions(CurveFunctions.toVector3Array(points));
    }

    public override void UpdateCurveFunction()
    {
        throw new System.NotImplementedException();
    }
}
