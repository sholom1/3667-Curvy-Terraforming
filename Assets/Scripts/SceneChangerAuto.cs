using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangerAuto : MonoBehaviour
{

    float timeToLoadScene = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToScene", timeToLoadScene);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


