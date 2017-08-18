using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventProcessor : MonoBehaviour
{
    public string Purpose;
     	[Header("Subtracted pre multiplitcation")]
 	[SerializeField]
    float _subPre;
 


   [Header("Added post multiplitcation")]
   [SerializeField]
    float _multiply = 1;
	 

  
  [Header("Added post multiplitcation")]
     [SerializeField]
    float _addPost;

    float processed(float i)
    {
        i = (i - _subPre) * _multiply + _addPost;

        return i;
    }

    public float subPre
    {
        get { return _subPre; }
        set { _subPre = value; 
        input = dummyInput;
        }

    }
 
    public float multiply
    {
        get { return _multiply; }
        set { _multiply = value; 
           input = dummyInput;
        }

    }


    public float divide
    {
        get {  if (_multiply!=0) return 1/_multiply;  else  return 0; }
        set { 
            if (value!=0)
          {  _multiply = 1/value; 
               input = dummyInput;
         } }

    }
    public float addPost
    {
        get { return _addPost; }
        set { _addPost = value;
         input = dummyInput;
         }

    }
 
    //public float minVal;
    //public float maxVal=1;
    //public bool invert;

    //float dif=1;
    /* 
    public void setMax(float f)
    {if (f==minVal) return;
        maxVal=f;
        dif=maxVal-minVal;
    }
    public void setMin(float f)
    {if (f==maxVal) return;
        minVal=f;
        dif=maxVal-minVal;
    }*/

    public float input
    {
        //   get { return _addPost; }
        set
        {
            dummyInput = value;
            output=processed(dummyInput);
            if (isOn) eventProcessorOutput.Invoke(output);

        }

    }
    public bool isOn;
    void OnEnable()
    {
        isOn = true;

    }
    void OnDisable()
    {
        isOn = false;
    }
    public FloatEvent eventProcessorOutput;
    [Range(0, 1)]
    public float dummyInput;
    [Range(0, 1)]
    public float output;
    public bool sendTestOutput;
    void OnValidate()
    {
        output = processed(dummyInput);

        if (sendTestOutput) input = dummyInput;
    }



}
