using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class EventTimeoutDetection : MonoBehaviour{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }
    public float timeOut = 1;
    float lastTrigger;
    bool isTimedOut=true;


    void Update()
    {

        if (Time.time - lastTrigger > timeOut)
        {
            if (!isTimedOut)
            {
                isTimedOut = true;
                text.text = "<color=#FF0000>TIMEOUT</color>";
            }
        }
        else
        {
            if (isTimedOut)
            {
                isTimedOut = false;
                text.text = " ";
            }
        }
    }
    public void Trigger()
    {
        lastTrigger = Time.time;


    }






}
