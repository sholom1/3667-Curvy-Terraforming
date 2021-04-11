using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    
    public GameObject VictoryMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        Instantiate(VictoryMusic, transform.position, Quaternion.identity);
        }
        
    }


}
