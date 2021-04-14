using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text messageText;

    private void Awake(){
        messageText = transform.Find("message").Find("message").GetComponent<Text>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        //messageText.text = "Hello World!";
        textWriter.AddWriter(messageText, "Hello World!", 1f);
    }

    
}
