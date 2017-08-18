using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventInvert : MonoRect {
public BoolEvent invertedEvent;


public void TriggerInverted(bool b)
{Debug.Log("called "+b,gameObject);
    invertedEvent.Invoke(!b);
}
}
