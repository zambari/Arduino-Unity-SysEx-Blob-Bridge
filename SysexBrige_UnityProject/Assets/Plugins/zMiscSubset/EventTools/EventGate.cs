using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGate : MonoBehaviour {

[SerializeField]
private bool _gateOpen=true;

public void EventGateInput()
{
	if (gateOpen)
	whenGateOpen.Invoke();
	else
	whenGateClosed.Invoke();
}
public VoidEvent whenGateOpen;
public VoidEvent whenGateClosed;


public void SetGateOpen(bool b)
{
	gateOpen=b;
}
public bool gateOpen
{
	get 
	{
		return _gateOpen;
	} 
	set
	{
		_gateOpen = value;
	}
}

}
