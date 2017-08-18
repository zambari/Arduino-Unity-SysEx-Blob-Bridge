using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventToggleActive : MonoBehaviour
{

    public GameObject target;
    public Canvas canvas;
    public bool useCanvas;
    public bool useSetActive;
	public bool setNow=true;
bool didValiate;
  protected   void OnValidate()
    {
	
	if (!didValiate)
		{
			if (target==null) target=gameObject;
			canvas=GetComponent<Canvas>();
		if (canvas==null) 
		{
			useCanvas=false;
			useSetActive=true;
			didValiate=true;
		}


		}

		
        useCanvas = !useSetActive;
		isActive=setNow;
	//	isActive=setNow;

    }
    void Start()
    { 
        if (target == null) target = gameObject;
    }
   // [Range(0, 1)]
   // public bool setActiveSignal;
    bool _act=true;
    public bool isActive
    {
        get { return _act; }
        set
        {	//setNow=value;
            //if ( _act==value) return;
            _act = value;
			setNow=value;
           // setActiveSignal = _act;

            if (useCanvas)
            {
                if (canvas == null)
                    canvas = GetComponent<Canvas>();
                if (canvas != null) canvas.enabled = _act;
            }
            else
            {
                if (target == null)
                { target = gameObject; }

                target.SetActive(_act);
            }
        }
    }



}
