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
    public bool isConfirmed = false;
    public Canvas canvas;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
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
        Destroy(canvas.gameObject);
        isConfirmed = true;
        onMove -= UpdatePreview;
        onMove += UpdateLink;
        onMove((_RightAnchor.transform.position + _LeftAnchor.transform.position) / 2);
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
        if (Vector2.Distance(_RightAnchor.transform.position, _LeftAnchor.transform.position) > AnchorPointManipulator.instance.ConnectionRange)
        {
            BreakLink();
            return;
        }
        transform.position = (_RightAnchor.transform.position + _LeftAnchor.transform.position) / 2;
        lineRenderer.SetPositions(new Vector3[] { _RightAnchor.transform.position, _LeftAnchor.transform.position });
    }
    public void BreakLink()
    {
        _RightAnchor.RemoveLink();
        _LeftAnchor.RemoveLink();
        Destroy(gameObject);
    }
}
