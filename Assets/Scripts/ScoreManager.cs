using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }
    [SerializeField]
    private TextMeshProUGUI BudgetText;
    private int budget;
    [SerializeField]
    private int dCost, sCost;
    // Start is called before the first frame update
    public void BuyCurve(Curve curve)
    {
        UpdateScore(curve.IsStatic ? sCost : dCost);
    }
    public void SellCurve(Curve curve)
    {
        UpdateScore(Mathf.RoundToInt(-.5f * (curve.IsStatic ? sCost : dCost)));
    }
    private void UpdateScore(int value)
    {
        budget += value;
        BudgetText.text = $"Money spent: {budget}";
    }
}
