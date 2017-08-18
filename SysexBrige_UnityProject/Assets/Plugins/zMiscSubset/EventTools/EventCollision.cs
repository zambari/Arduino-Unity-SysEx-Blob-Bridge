using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventCollision : MonoBehaviour
{
    #pragma warning disable 414
    public UnityEvent OnTriggerOrCollision;
    /*[SerializeField]
    [ReadOnly]
    bool wasTrigger;
    [SerializeField]
    [ReadOnly]
    bool wasCollision;*/
    protected virtual void OnCollisionEnter()
    {
        OnTriggerOrCollision.Invoke();
      //  wasTrigger = false;
       // wasCollision = true;
    }

    protected virtual void OnTriggerEnter()
    {
        OnTriggerOrCollision.Invoke();
     //   wasTrigger = true;
   //     wasCollision = false;
    }

    void OnValidate()
    {
     //   wasTrigger = false;
      //  wasCollision = false;
    }



}
