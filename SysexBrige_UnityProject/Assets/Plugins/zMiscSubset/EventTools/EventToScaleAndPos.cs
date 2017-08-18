using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventToScaleAndPos : MonoRect {
float scales;
public void setScale(float f)
{scales=f;
transform.localScale=new Vector3(f,1,1);
}
	
public void setPos(float f)
{
rect.setLocalX(-f*scales);
}
	
	
}
