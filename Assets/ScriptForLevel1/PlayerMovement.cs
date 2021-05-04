using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }
    [SerializeField]
    private float targetSpeed = 10.0f;
    [SerializeField]
    private bool AWD = true;
    [SerializeField]
    private new Rigidbody2D rigidbody;
    private WheelJoint2D[] wheels;
    private float inputAxis;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        wheels = GetComponentsInChildren<WheelJoint2D>();
        if (CurveManager.instance)
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
            for (int i = 0; i < (AWD ? wheels.Length : 1); i++)
            {
                JointMotor2D jointMotor2D = wheels[i].motor;
                jointMotor2D.motorSpeed = -(inputAxis * targetSpeed);
                wheels[i].motor = jointMotor2D;
            }
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
                Array.ForEach(wheels, wheel => wheel.connectedBody.simulated = value);
                rigidbody.constraints = value ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
