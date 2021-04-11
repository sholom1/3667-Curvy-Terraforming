using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAutoMove : MonoBehaviour
{
    [SerializeField] Vector3 movement;
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] float speed = 10.0f;
	[SerializeField] bool isFacingRight = false;
    [SerializeField] bool isFacingLeft = false;

   
    public Vector3 pointB;
   
    IEnumerator Start()
    {
        var pointA = transform.position;
        while(true)
        {
            //Flip();
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 5.0f));
            
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 10.0f));
            //Flip();
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
        Flip();
    }
    

    void Flip()
	{
		Vector3 playerScale = transform.localScale;
		playerScale.x = playerScale.x * -1;
		transform.localScale = playerScale;

        //transform.Rotate(0, 180, 0);

        isFacingLeft = !isFacingLeft;
	}
}
