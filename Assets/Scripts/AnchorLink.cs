using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorLink : MonoBehaviour
{
    private AnchorPoint _RightAnchor;
    private AnchorPoint _LeftAnchor;
    public delegate void OnMove(Vector2 position);
    public OnMove onMove;
    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetAnchors(AnchorPoint rhs, AnchorPoint lhs)
    {
        _RightAnchor = rhs;
        _RightAnchor.SetLink(this);
        _LeftAnchor = lhs;
        _LeftAnchor.SetLink(this);
        onMove += UpdatePreview;
    }
    public void Confirm()
    {
        Destroy(lineRenderer);
        onMove -= UpdatePreview;
        onMove += UpdateLink;   
    }
    public void UpdateLink(Vector2 position)
    {
        _RightAnchor.transform.position = position;
        _LeftAnchor.transform.position = position;
        _RightAnchor.curve.Compute();
        _LeftAnchor.curve.Compute();
    }
    public void UpdatePreview(Vector2 position)
    {
        lineRenderer.SetPositions(new Vector3[] { _RightAnchor.transform.position, _LeftAnchor.transform.position });
    }
}
