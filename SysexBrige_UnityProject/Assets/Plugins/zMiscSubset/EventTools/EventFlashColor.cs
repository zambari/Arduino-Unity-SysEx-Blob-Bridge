using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventFlashColor : MonoRect
{

    public Color flashColor;
    public float fadeTime = 0.5f;
    float startTime;
	public void Flash()
	{
if (gameObject.activeInHierarchy)
		StartCoroutine(fade());
	}
	public void Flash(string s)

	{
		Flash();
	}
    IEnumerator fade()
    {
        startTime = Time.time;
        image.color = flashColor;
        while (Time.time < startTime + fadeTime)
        {
            float r = 1 - (Time.time - startTime) / fadeTime;
            image.color = new Color(flashColor.r, flashColor.g, flashColor.b, r);
            yield return null;
        }


    }



}
