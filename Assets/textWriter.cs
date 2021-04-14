using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textWriter : MonoBehaviour
{

    private Text uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter) {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if(uiText != null) {
            timer -= Time.deltaTime;
            if(timer <= 0f) {
                //Display next character
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.SubString(0,characterIndex);
            }
        }
    }
}
