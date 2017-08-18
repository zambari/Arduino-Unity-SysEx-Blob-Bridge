using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// please consider zExtensions
// https://github.com/zambari/zMisc.Unity
/*
public static class extensinos
{
    public static char[] ToCharArray(this byte[] b,int len=1)
    {
		if (len==0) len=b.Length;
		if (len==-1) return new char[1];
		Debug.Log(len);
        char[] c = new char[len];
        for (int i = 0; i < len; i++)
            c[i] = (char)b[i];
        return c;
    }
    public static byte[] ToByteArray(this string s)
    {
        byte[] byteArray = new byte[s.Length];
        for (int i = 0; i < s.Length; i++)
            byteArray[i] = (byte)s[i];
        return byteArray;
    }
	   public static string ArrayToString(this byte[] b)
    {
     string s="";
        for (int i = 0; i < b.Length; i++)
		{if (b[0]==0) return s;
          s+=(char)b[i];
		}
        return s;
    }
}
	
	 */