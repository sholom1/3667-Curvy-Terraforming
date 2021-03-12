using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class AnchorPoint : MonoBehaviour
{
    [SerializeField]
    private Curve curve;
    
    public void Move(Vector2 worldPos)
    {
        transform.position = worldPos;
        curve.Compute();
    }
}
