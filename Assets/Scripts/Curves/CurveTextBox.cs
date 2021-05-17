using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CurveTextBox : MonoBehaviour
{
    public FractionDisplay A;
    public TextMeshProUGUI function;
}
[System.Serializable]
public class FractionDisplay
{
    public TextMeshProUGUI Numerator;
    public TextMeshProUGUI Denominator;
}
