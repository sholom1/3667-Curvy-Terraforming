using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurveSpawner : MonoBehaviour
{
    private int maxCurves = 2;
    [SerializeField]
    private float minSize, maxSize;
    private Curve[] curves;
    [SerializeField]
    private Curve[] CurvePrefabs;
    [SerializeField]
    private GameObject CurrentCurveParent;
    [SerializeField]
    private GameObject NextCurveParent;
    [SerializeField]
    private TextMeshProUGUI BudgetText;
    private int budget;
    [SerializeField]
    private int dCost, sCost;

    private void Start()
    {
        curves = new Curve[] 
        { 
            GenerateCurve(CurrentCurveParent.transform.position, minSize, maxSize), 
            GenerateCurve(NextCurveParent.transform.position, minSize, maxSize) 
        } ;
    }
    public void SpawnNext(bool asStatic)
    {
        Curve curve = GetNextCurve();
        curve.MarkStatic(asStatic);
        budget += asStatic ? sCost : dCost;
        BudgetText.text = $"Money spent: {budget}";
        CurveManager.instance.SetSelectedCurve(curve);
    }
    public Curve GetNextCurve()
    {
        Curve value = curves[0];
        curves[0] = curves[1];

        Vector2 currentPosition = new Vector2(CurrentCurveParent.transform.position.x, CurrentCurveParent.transform.position.y);
        curves[0].transform.position = currentPosition;
        Vector2 position = new Vector2(NextCurveParent.transform.position.x, NextCurveParent.transform.position.y);
        curves[1] = GenerateCurve(position, minSize, maxSize);
        return value;
    }
    private Curve GenerateCurve(Vector2 position, float minDelta, float maxDelta)
    {
        minDelta = Mathf.Min(minDelta, maxDelta); maxDelta = Mathf.Max(minDelta, maxDelta);
        Curve generatedCurve = Instantiate(CurvePrefabs[Random.Range(0, CurvePrefabs.Length)], position, Quaternion.identity);
        Vector2 randomPosition() => new Vector2(Random.Range(minDelta, maxDelta), Random.Range(minDelta, maxDelta));
        generatedCurve.StartAnchor.transform.position = position + randomPosition();
        generatedCurve.EndAnchor.transform.position = position - randomPosition();
        if (generatedCurve is QuadraticCurve quadratic)
            quadratic.MiddleAnchor.transform.position = position + new Vector2(0, Random.Range(minDelta, maxDelta));
        generatedCurve.Compute();
        return generatedCurve;
    }
}
