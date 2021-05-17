using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static CurveFunctions;
public class LinearCurve : Curve
{
    public override void Compute()
    {
        Vector2[] points2D = new Vector2[] { StartAnchor.transform.localPosition, EndAnchor.transform.localPosition };
        Vector3[] points3D = new Vector3[] { StartAnchor.transform.localPosition, EndAnchor.transform.localPosition };
        edgeCollider.points = points2D;
        lineRenderer.SetPositions(points3D);
        UpdateCurveFunction();
    }

    public override void UpdateCurveFunction()
    {
        Vector2 diff = StartAnchor.transform.position - EndAnchor.transform.position;
        LinearCurveText linearCurveText = CurveManager.instance.LinearCurveText;
        linearCurveText.gameObject.SetActive(true);
        CurveManager.instance.QuadraticCurveText.gameObject.SetActive(false);
        float slope = diff.x / diff.y;
        Fraction fraction = RealToFraction(slope, 0.01);
        linearCurveText.A.Numerator.text = fraction.N.ToString();
        linearCurveText.A.Denominator.text = fraction.D.ToString();
        float b = StartAnchor.transform.position.y - (slope * StartAnchor.transform.position.x);
        linearCurveText.function.text = $"x + {b.ToString("0.00")}";
    }
}
