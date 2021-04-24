using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    [SerializeField]
    private new Rigidbody2D rigidbody;
    private WheelsRotate[] wheels;
    private float inputAxis;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        wheels = GetComponentsInChildren<WheelsRotate>();
        CurveManager.instance.OnToggleBuildMode.AddListener(value => CanMove = !value);
    }
    private void Update()
    {
        inputAxis = Input.GetAxis("Horizontal");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (inputAxis < 0)
                rigidbody.velocity = new Vector3(inputAxis * walkSpeed, rigidbody.velocity.y);
            if (inputAxis > 0)
                rigidbody.velocity = new Vector3(inputAxis * walkSpeed, rigidbody.velocity.y);
        }
    }
    private bool canMove = true;
    public bool CanMove
    {
        get { return canMove; }
        set
        {
            if (value != canMove)
            {
                canMove = value;
                Array.ForEach(wheels, wheel => wheel.enabled = value);
            }
        }
    }
}
