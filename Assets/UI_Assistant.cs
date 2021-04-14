using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Assistant : MonoBehaviour
{
    private Text messageText;

    private void Awake(){
        messageText = transform.Find("message").Find("message").GetComponent<Text>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        messageText.text = "Hello World!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
