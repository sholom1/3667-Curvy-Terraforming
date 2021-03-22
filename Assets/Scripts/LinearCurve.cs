using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LinearCurve : Curve
{
    

    public override void Compute()
    {
        Vector2[] points2D = new Vector2[] { _StartAnchor.transform.localPosition, _EndAnchor.transform.localPosition };
        Vector3[] points3D = new Vector3[] { _StartAnchor.transform.localPosition, _EndAnchor.transform.localPosition };
        edgeCollider.points = points2D;
        lineRenderer.SetPositions(points3D);
    }

    public override string GetCurveFunction()
    {
        throw new System.NotImplementedException();
    }
}
