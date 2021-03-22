using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class AnchorPoint : MonoBehaviour
{
    public Curve curve;
    private AnchorLink link;
    
    public void Move(Vector2 worldPos)
    {
        transform.position = worldPos;
        curve.Compute();
    }
    public bool HasLink()
    {
        return link != null;
    }
    public void SetLink(AnchorLink anchorLink)
    {
        if (HasLink())
            link = null;
        link = anchorLink;
    }
}
