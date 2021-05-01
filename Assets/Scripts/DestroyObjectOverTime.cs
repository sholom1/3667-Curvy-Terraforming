using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOverTime : MonoBehaviour
{
    //public float lifetime;
    public GameObject prefab;
    
    public void timeToDestroy()
    {
        float lifetime = 2;
        lifetime -= Time.deltaTime;
        if(lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
