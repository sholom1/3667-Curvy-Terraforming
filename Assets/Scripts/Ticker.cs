using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{



    public TickerItem tickerItemPrefab; //this is where we get the prefab item to populate our ticker  
    [Range(1f, 40f)]
    public float itemDuration = 10.0f;
    public string[] fillerItems;

    float width; //keep track of its own current width
    float pixelsPerSecond;
    TickerItem currentItem; 

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSecond = (width / itemDuration)/2.0f;
        AddTickerItem(fillerItems[0]);

    }

    // Update is called once per frame
    void Update()
    {
       // if (currentItem.GetXPosition <= -currentItem.GetWidth)                //issue here? 
       // {
         //   AddTickerItem(fillerItems[Random.Range(0, fillerItems.Length)]); 
        // }
    }

    void AddTickerItem(string message) {
        currentItem = Instantiate(tickerItemPrefab, transform);
        currentItem.Initialize(width, pixelsPerSecond, message); 
    
    }

}
