using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateByClock : MonoBehaviour {
float lastZ;

public int PPQN=24;
private float step;

void OnValidate()
{ getStep();



}

void Start()
{
 getStep();

}

void getStep()
{
	step=360f/(16f*PPQN);

}
public void Tick()
{

lastZ+=step;
transform.localRotation=Quaternion.Euler(0,0,-lastZ);

}
	
	
	
}
