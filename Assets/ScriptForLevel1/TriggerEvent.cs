using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    
    public GameObject prefab;
    //public GameObject prefab2;
    public float x;
    public float y;
    public float z;
    public float lifetime = 2;
    //public bool destroyText;
    //public Vector3 positionPlaced;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject prefab2;
        //float lifetime = 2;
        //positionPlaced = transform.position;
        if (collision.gameObject.tag == "Player")
        {
        prefab2 = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
        Destroy(prefab2, lifetime);
        }
        
    }
    

    // void OnTriggerExit2D(Collider2D collision)
    // {
    //    Destroy(prefab2, lifetime);
    // }


}
