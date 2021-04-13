using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFishFallMovement : MonoBehaviour
{
    [SerializeField] Vector3 movement;
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] float speed = 10.0f;
	[SerializeField] bool isFacingRight = true;

   
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;
   
    IEnumerator Start()
    {
        var pointA = transform.position;
        while(true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 5.0f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointC, 10.0f));
            yield return StartCoroutine(MoveObject(transform, pointC, pointD, 10.0f));
        }
    }
   
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i= 0.0f;
        var rate= 1.0f/time;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    

    
}
