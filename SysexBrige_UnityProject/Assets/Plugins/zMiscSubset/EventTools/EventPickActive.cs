using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPickActive : MonoBehaviour {
[Header("will enable one based on index")]
public GameObject[] objects;
public bool getChildren;
void OnValidate()
{
	if (getChildren)
	{
		//getChildren=false;
		getObjects();
	}
}
void getObjects()
{
	objects=new GameObject[transform.childCount];
	for (int i=0;i<objects.Length;i++) objects[i]=transform.GetChild(i).gameObject;
	PickActive(0);
}

void Start()
{	if (getChildren)
		getObjects();
}
void Reset()
{
	getObjects();
}

	public void PickActive(int v)
	{
		if (v<0 || v>=objects.Length) { Debug.Log("invalid selection "+v,gameObject); return;}
		for (int i=0;i<objects.Length;i++)
			if (objects[i]!=null)	objects[i].SetActive(i==v);
	}

	public void PickActiveFloat(float v)
	{
		PickActive(Mathf.RoundToInt(v));
	}

	
	
	
}
