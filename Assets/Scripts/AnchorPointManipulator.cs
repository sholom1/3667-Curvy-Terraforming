using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPointManipulator : MonoBehaviour
{
    [SerializeField]
    private LayerMask AnchorLayer;

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
            _SelectedAnchor.Move(mouseWorldPos);
        }
    }
}
