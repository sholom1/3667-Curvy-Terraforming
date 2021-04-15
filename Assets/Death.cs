using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Abyss"))
        {
        Destroy(gameObject);
        respawn.instance.RespawningPoint();
        }
        
    }
}
