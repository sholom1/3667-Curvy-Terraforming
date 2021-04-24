using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public bool CanMove = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove)
        {
            float move = Input.GetAxis("Horizontal");

            if (move < 0)
                GetComponent<Rigidbody2D>().velocity = new Vector3(move * walkSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (move > 0)
                GetComponent<Rigidbody2D>().velocity = new Vector3(move * walkSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
