using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventIntSwitch : MonoBehaviour {
[Header("Use to reset state")]
public UnityEvent triggerAlways;
[Header("Use to trigger picked actions")]
public UnityEvent[] events;


void Start()
{
   Slider s=GetComponent<Slider>();
   if (s!=null)
   {
	   s.onValueChanged.AddListener(TriggerValueFloat);
   }

}
void Reset()
{
	if (events==null || events.Length==0) events=new UnityEvent[3];
}
public void TriggerValueFloat(float v)
{
	TriggerValue(Mathf.RoundToInt(v));
	
}
public void TriggerValue(int v)
{

}
	
	
}
