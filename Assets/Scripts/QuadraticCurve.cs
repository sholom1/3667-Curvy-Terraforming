using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static CurveFunctions;

[RequireComponent(typeof(EdgeCollider2D), typeof(LineRenderer))]
public class QuadraticCurve : Curve
{
    public AnchorPoint MiddleAnchor;
    [SerializeField]
    [Range(10, 200)]
    private int _Iterations;

    private float curveLength;

    public override void Compute()
    {
        Vector2[] points = new Vector2[_Iterations];
        Vector2 startPos = StartAnchor.transform.localPosition;
        Vector2 endPos = EndAnchor.transform.localPosition;
        Vector2 midPos = (StartAnchor.transform.localPosition + EndAnchor.transform.localPosition) / 2;
        midPos.y = MiddleAnchor.transform.localPosition.y;
        MiddleAnchor.transform.localPosition = midPos;
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
        UpdateCurveFunction();
    }

    public override void UpdateCurveFunction()
    {
        Vector2 startPos = StartAnchor.transform.localPosition;
        Vector2 midPos = MiddleAnchor.transform.localPosition;
        Vector2 endPos = EndAnchor.transform.localPosition;
        float a = (startPos.y - 2 * midPos.y + endPos.y) / Mathf.Pow(endPos.x - startPos.x, 2);
        float b = (2 / Mathf.Pow(endPos.x - startPos.x, 2)) * (endPos.x * (midPos.y - startPos.y) + startPos.x * (midPos.y - endPos.y));
        float c = startPos.y - (a * (startPos.x * startPos.x) + b * startPos.x);
        Fraction aFrac = RealToFraction(a, .01);
        Fraction bFrac = RealToFraction(b, .01);
        QuadraticCurveText quadraticCurveText = CurveManager.instance.QuadraticCurveText;
        quadraticCurveText.gameObject.SetActive(true);
        CurveManager.instance.LinearCurveText.gameObject.SetActive(false);
        quadraticCurveText.A.Numerator.text = aFrac.N.ToString();
        quadraticCurveText.A.Denominator.text = aFrac.D.ToString();
        quadraticCurveText.B.Numerator.text = bFrac.N.ToString();
        quadraticCurveText.B.Denominator.text = bFrac.D.ToString();
        quadraticCurveText.function.text = $"x + {c.ToString("0.00")}";
    }
    public override void MarkStatic(bool value)
    {
        base.MarkStatic(value);
        MiddleAnchor.isStatic = value;
    }
}
