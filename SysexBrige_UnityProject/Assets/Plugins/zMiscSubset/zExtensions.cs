using UnityEngine;

using System;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;


//  added to char array 2017.08.18

public static class zExtensions
{ 
        public static char[] ToCharArray(this byte[] b,int len=0, char paddingchar=(char)0) // 2017.08.18
    {
		if (len==0) len=b.Length;
		if (len==-1) return new char[1];

        char[] c = new char[len];
    
    Debug.Log("source " +b.Length+" cl "+c.Length+" len ="+len);
    
        for (int i = 0; i < c.Length; i++)
        {
            
            if (i<b.Length)
            c[i] = (char)b[i];
            else
            c[i]=paddingchar;
        }
        return c;
    }
    public static byte[] ToByteArray(this string s)// 2017.08.18
    {
        byte[] byteArray = new byte[s.Length];
        for (int i = 0; i < s.Length; i++)
            byteArray[i] = (byte)s[i];
        return byteArray;
    }
	   public static string ArrayToString(this byte[] b) // 2017.08.18
    {
     string s="";
        for (int i = 0; i < b.Length; i++)
		{if (b[0]==0) return s;
          s+=(char)b[i];
		}
        return s;
    }
       public static string ArrayToString(this char[] b) // 2017.08.182
    {
     string s="";
        for (int i = 0; i < b.Length; i++)
		{if (b[0]==0) return s;
          s+=(char)b[i];
		}
        return s;
    }
    public static Color baseColor = new Color(1f / 6, 1f / 2, 1f / 2, 1f / 2);

    public static bool executeIfTrue(this bool condition, Action ac)
    {
        if (condition) ac.Invoke();
        return false;
    }

    public static bool checkFloat(this float f)
    {
        if (Single.IsNaN(f))
        {
            Debug.Log("invalid float (NAN), dividing by zero? !");
            return false;

        }
        return true;
    }

    public static void AddLayoutElementFlexible(this GameObject g, bool flexible = true)
    {
        LayoutElement le = g.GetComponent<LayoutElement>();
        if (le == null) le = g.AddComponent<LayoutElement>();
        le.flexibleHeight = 1; le.flexibleWidth = 1;
#if UNITY_EDITOR
        UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(le, false);
#endif
    }
    public static string ToShortString(this float f)
    {
        return (Mathf.Round(f * 100) / 100).ToString();

    }
    public static string ToStringShort(this float f)
    {
        return (Mathf.Round(f * 100) / 100).ToString();

    }

    public static bool shiftPressed()

    {
        return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }
    // LAYOUT HELPER



    public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
    {
        T t = gameObject.GetComponent<T>();
        if (t == null) t = gameObject.AddComponent<T>();
        return t;
    }

    public static bool disabled(this MonoBehaviour source)

    {
        return (!source.enabled || !source.gameObject.activeInHierarchy);

    }
    public static Transform[] GetChildren(this Transform transform)
    {
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
        return children;
    }

    public static LayoutElement[] getActiveElements(this HorizontalLayoutGroup layout)
    {
        List<LayoutElement> elements = new List<LayoutElement>();
        if (layout == null) return elements.ToArray();
        for (int i = 0; i < layout.transform.childCount; i++)
        {
            GameObject thisChild = layout.transform.GetChild(i).gameObject;
            LayoutElement le = thisChild.GetComponent<LayoutElement>();
            if (le != null)
            {
                if (!le.ignoreLayout) elements.Add(le);
            }
        }
        return elements.ToArray();
    }
    public static string asByteSize(this float byteCount)
    {

        if (byteCount < 10000) return Mathf.Round(byteCount / 1024) + "kb ";
        else
            return (byteCount / (1024 * 1024)).ToShortString() + "MB ";

    }
    public static Color Random(this Color c)
    {
        /*  if (c==null) { c =baseColor; }
          float r=c.r;
          float g=c.g;
          float b=c.b;
          r=r/2+r*UnityEngine.Random.value;
          g=g/2+g*UnityEngine.Random.value;
          g=g/2+b*UnityEngine.Random.value;

          c=new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 0.3f);
          return c;*/
        //        Color n=UnityEngine.Random.ColorHSV(0.4f,0.8f,0.3f,0.6f);
        c.a = UnityEngine.Random.value * 0.4f + 0.2f;
        return UnityEngine.Random.ColorHSV(0.4f, 0.8f, 0.3f, 0.6f);
    }
    /// <summary>
    /// slowish but faster to type,   returns rect.GetComponent<Image>();
    /// </summary>

    public static Image image(this RectTransform rect, float transparency = 1)
    {
        Image thisImage = rect.GetComponent<Image>();
        if (thisImage == null)
        {
            thisImage = rect.gameObject.AddComponent<Image>();
            thisImage.color = thisImage.color.Random();
        }
        return thisImage;
    }
    public static RectTransform rect(this GameObject go)
    {
        RectTransform r = go.GetComponent<RectTransform>();
        if (r == null) r = go.AddComponent<RectTransform>();
        return r;
    }
}