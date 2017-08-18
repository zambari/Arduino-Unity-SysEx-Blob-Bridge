using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventFadeImage : MonoRect
{
    public Color colorA = Color.black;
    public Color colorB = Color.white;
    [Range(0, 1)]
    public float preview;
    public void fadeImage(float f)
    {
        image.color = Color.Lerp(colorA, colorB, f);

    }
 protected  void OnValidate()
    {
       
        fadeImage(preview);
    }



}
