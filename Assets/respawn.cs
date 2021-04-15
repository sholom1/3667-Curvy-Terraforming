using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public static respawn instance;

    public Transform respawnPoint;
    public GameObject carPrefab;

    private void Awake(){
        instance = this;
    }
    

    
    public void RespawningPoint()
    {
        Instantiate(carPrefab, respawnPoint.position, Quaternion.identity);
    }
}
