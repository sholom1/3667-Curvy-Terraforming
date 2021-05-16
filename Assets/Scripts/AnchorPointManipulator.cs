using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPointManipulator : MonoBehaviour
{
    [SerializeField]
    private LayerMask AnchorLayer;
    [SerializeField]
    private float connectionRange;
    public float ConnectionRange => BuildZone.LastBuildZone != null ? connectionRange * BuildZone.LastBuildZone.scaleScalar : connectionRange;
    [SerializeField]
    private AnchorLink _AnchorLinkPrefab;

    private new Camera camera;

    [SerializeField]
    private AnchorPoint _SelectedAnchor;
    [SerializeField]
    private AnchorPoint _PreviousAnchor;

    public static AnchorPointManipulator instance;

    public List<AnchorPoint> StaticAnchors;

    [SerializeField]
    private bool isInBuildMode = false;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        if (CurveManager.instance)
            CurveManager.instance.OnToggleBuildMode.AddListener(value => isInBuildMode = value);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInBuildMode) return;
        Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetSelectedAnchor(mouseWorldPos);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseAnchor();
        }
        if (_SelectedAnchor != null && !_SelectedAnchor.isStatic)
        {
            Vector3 previousPosition = _SelectedAnchor.transform.position;
            _SelectedAnchor.Move(mouseWorldPos);
            if (previousPosition != _SelectedAnchor.transform.position)
            {
                if (!_SelectedAnchor.HasLink())
                {
                    AnchorPoint lastAnchor = null;
                    float lastDistance = float.MaxValue;
                    //Find closest anchor under curves
                    foreach (Curve curve in CurveManager.instance.ActiveCurves)
                    {
                        if (curve == _SelectedAnchor.curve) continue;
                        AnchorPoint closestAnchor = curve.GetClosestAnchor(_SelectedAnchor, ConnectionRange);
                        if (closestAnchor != null)
                        {
                            float distance = Vector2.Distance(closestAnchor.transform.position, _SelectedAnchor.transform.position);
                            if (distance < lastDistance)
                                lastAnchor = closestAnchor;
                        }
                    }
                    //Find closest static anchor
                    foreach (AnchorPoint anchor in StaticAnchors)
                    {
                        float distance = Vector2.Distance(_SelectedAnchor.transform.position, anchor.transform.position);
                        if (distance < lastDistance && distance < ConnectionRange)
                        {
                            lastAnchor = anchor;
                            lastDistance = distance;
                        }
                    }
                    if (lastAnchor == null || lastAnchor.HasLink()) return;
                    AnchorLink newLink = Instantiate(_AnchorLinkPrefab);
                    newLink.SetAnchors(_SelectedAnchor, lastAnchor);
                }
            }
        }
    }

    private void SetSelectedAnchor(Vector2 mouseWorldPos)
    {
        if (CurveManager.instance.isPlacingCurve) return;
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, camera.transform.forward, float.MaxValue, AnchorLayer);
        if (hit.collider != null && hit.collider.TryGetComponent(out AnchorPoint anchor))
        {
            //Hide Link confirmation UI of previous link
            if (_PreviousAnchor != null && _PreviousAnchor.HasLink() && _PreviousAnchor.link.isConfirmed)
                _PreviousAnchor.link.gameObject.SetActive(false);

            if (anchor.HasLink())
                anchor.link.ToggleColliders(false);
            else
                anchor.curve.edgeCollider.enabled = false;

            _SelectedAnchor = anchor;
        }
    }

    private void ReleaseAnchor()
    {
        _PreviousAnchor = _SelectedAnchor != null ? _SelectedAnchor : _PreviousAnchor;
        if (_PreviousAnchor == null) return;
        if (_PreviousAnchor.HasLink())
            _PreviousAnchor.link.ToggleColliders(true);
        else
            _PreviousAnchor.curve.edgeCollider.enabled = true;
        _SelectedAnchor = null;
    }
}
