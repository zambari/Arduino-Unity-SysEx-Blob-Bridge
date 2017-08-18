using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventIntForward : MonoBehaviour {

public IntEvent whenTriggered;

public void Trigger(int i)
{
	whenTriggered.Invoke(i);
}
	
	
	
}
