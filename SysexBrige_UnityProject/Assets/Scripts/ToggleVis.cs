using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVis : MonoBehaviour {
Image image;
void Start()
{
	image=GetComponent<Image>();
}

public void toggle()
{
	image.enabled=!image.enabled;
}
	
	
	
}
