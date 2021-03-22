using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPointManipulator : MonoBehaviour
{
    [SerializeField]
    private LayerMask AnchorLayer;
    [SerializeField]
    private float _ConnectionRange;
    [SerializeField]
    private AnchorLink _AnchorLinkPrefab;

    private new Camera camera;

    private AnchorPoint _SelectedAnchor;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, camera.transform.forward, float.MaxValue, AnchorLayer);
            if (hit.collider != null && hit.collider.TryGetComponent(out AnchorPoint anchor))
            {
                _SelectedAnchor = anchor;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _SelectedAnchor = null;
        }
        if (_SelectedAnchor != null)
        {
            Vector3 previousPosition = _SelectedAnchor.transform.position;
            _SelectedAnchor.Move(mouseWorldPos);
            if (!_SelectedAnchor.HasLink() && previousPosition != _SelectedAnchor.transform.position)
            {
                AnchorPoint lastAnchor = null;
                float lastDistance = float.MaxValue;
                foreach (Curve curve in CurveManager.instance.ActiveCurves)
                {
                    if (curve == _SelectedAnchor.curve) continue;
                    AnchorPoint closestAnchor = curve.GetClosestAnchor(_SelectedAnchor, _ConnectionRange);
                    if (closestAnchor != null)
                    {
                        float distance = Vector2.Distance(closestAnchor.transform.position, _SelectedAnchor.transform.position);
                        if (distance < lastDistance)
                            lastAnchor = closestAnchor;
                    }
                }
                if (lastAnchor == null || lastAnchor.HasLink()) return;
                AnchorLink newLink = Instantiate(_AnchorLinkPrefab);
                newLink.SetAnchors(_SelectedAnchor, lastAnchor);
            }
        }
    }
}
