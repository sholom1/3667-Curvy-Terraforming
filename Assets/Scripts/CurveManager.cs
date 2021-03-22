using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveManager : MonoBehaviour
{
    public static CurveManager instance;
    public List<Curve> ActiveCurves;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
}
