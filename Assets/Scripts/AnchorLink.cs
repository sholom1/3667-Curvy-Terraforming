using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnchorLink : MonoBehaviour
{
    private AnchorPoint _RightAnchor;
    private AnchorPoint _LeftAnchor;
    public delegate void OnMove(Vector2 position);
    public OnMove onMove;
    public LineRenderer lineRenderer;
    public bool isConfirmed = false;
    public Canvas canvas;
    [SerializeField]
    private Button ConfirmButton;
    [SerializeField]
    private Button DeleteButton;

    private bool isStaticLink => _RightAnchor.gameObject.isStatic || _LeftAnchor.gameObject.isStatic;

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
        Destroy(ConfirmButton.gameObject);
        isConfirmed = true;
        onMove -= UpdatePreview;
        if (!isStaticLink)
        {
            onMove += UpdateLink;
            onMove((_RightAnchor.transform.position + _LeftAnchor.transform.position) / 2);
        }
        else
        {
            AnchorPoint staticAnchor = _RightAnchor.gameObject.isStatic ? _RightAnchor : _LeftAnchor;
            AnchorPoint antiStatic = !_RightAnchor.gameObject.isStatic ? _RightAnchor : _LeftAnchor;
            antiStatic.transform.position = staticAnchor.transform.position;
            antiStatic.curve.Compute();
        }
        onMove += _ =>
        {
            if (!gameObject.activeInHierarchy) gameObject.SetActive(true);
        };
    }
    public void UpdateLink(Vector2 position)
    {
        if (!_RightAnchor.gameObject.isStatic)
        {
            _RightAnchor.transform.position = position;
            _RightAnchor.curve.Compute();
        }
        if (!_LeftAnchor.gameObject.isStatic)
        {
            _LeftAnchor.transform.position = position;
            _LeftAnchor.curve.Compute();
        }
        transform.position = (_RightAnchor.transform.position + _LeftAnchor.transform.position) / 2;
    }
    public void UpdatePreview(Vector2 position)
    {
        if (Vector2.Distance(_RightAnchor.transform.position, _LeftAnchor.transform.position) > AnchorPointManipulator.instance.ConnectionRange)
        {
            BreakLink();
            return;
        }
        transform.position = (_RightAnchor.transform.position + _LeftAnchor.transform.position) / 2;
        lineRenderer.SetPositions(CurveFunctions.toVector3Array(new Vector2[] { _RightAnchor.transform.position, _LeftAnchor.transform.position }));
    }
    public void BreakLink()
    {
        _RightAnchor.RemoveLink();
        
        _LeftAnchor.RemoveLink();
        if (isConfirmed)
        {
            if (!_LeftAnchor.gameObject.isStatic)
                _LeftAnchor.Move(transform.position - transform.right);
            if (!_RightAnchor.gameObject.isStatic)
                _RightAnchor.Move(transform.position + transform.right);
        }
        Destroy(gameObject);
    }
}
