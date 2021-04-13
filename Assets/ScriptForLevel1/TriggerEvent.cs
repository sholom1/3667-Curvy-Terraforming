using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    
    public GameObject prefab;
    public bool destroyText;
    //public Vector3 positionPlaced;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //positionPlaced = transform.position;
        if (collision.gameObject.tag == "Player")
        {
        Instantiate(prefab, transform.position, Quaternion.identity);
        }
        
    }
    

    void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(prefab);
    }


}
