using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ByteCollector : MonoBehaviour
{
    Text text;
    System.Text.StringBuilder sb;
    public int flushWhen = 1000;
    public bool showByteVals;
    public bool showBytesAsInt;
    public int colCount = 5;
    int colCounter;
	public bool colorBytes;
	public string colorStringBracket="#1f33ee";
	public string colorStringValue="#FFFFFF";
	public string colorStringValueSystem="#1010ff";
	public string colorSysex="#00FF00";
	//	public string colorStringValueSystem="#1010ff";
	public bool showClocks;
	public bool showActiveSense;
    void Start()
    {
        sb = new System.Text.StringBuilder();
        text = GetComponent<Text>();
    }

	void colorEnter(string colorString)
	{
			if (!colorBytes) return;
			sb.Append("<color=");	
			sb.Append(colorString);
				sb.Append(">");
	}
	void colorExit()
	{
		
	if (!colorBytes) return;
	sb.Append("</color>");

	}
    public void OnNewByte(int i)
    {
        byte b = (byte)i;
		if (b==SysexBlockTransfer.MIDI_ACTIVE_SENSE && !showActiveSense) return;
		if (b==SysexBlockTransfer.MIDI_CLOCK && !showClocks) return;
		if (i==-1) {	sb.Append("\n"); return; }
					if (i==-3) {    sb = new System.Text.StringBuilder(); }
				if (i==-2|| i==-3){	sb.Append("\n----\n\n"); return; }
        if (showByteVals)
        {
            colCounter++;
            if (colCounter > colCount)
            {
                colCounter = 0;
                sb.AppendLine();
            }

		//	colorEnter(colorStringBracket);
	
		//	colorExit();
			string color=colorStringValue;

			if (b>127)
				color=colorStringValueSystem;
		     if (i==SysexBlockTransfer.SYSEX_START || i== SysexBlockTransfer.SYSEX_END)
				 color=colorSysex;
			if (b==32) 
			{  sb.Append("\n"); 
					  return;
			 }
			
			colorEnter(color);
			sb.Append("[");


			if (showBytesAsInt)
            sb.Append(b.ToString("D3"));
			else
			sb.Append(b.ToString("X"));
		
			sb.Append("] ");
			colorExit();
        }
        else
        {


            sb.Append((char)b);
        }
        text.text = sb.ToString();
        if (text.text.Length >(colorBytes?flushWhen:flushWhen*5))
        {
            trimBuffer();
        }
    }
public void clearBuffer()
{
	sb = new System.Text.StringBuilder();
	text.text="";
}
void trimBuffer()
{
	sb = new System.Text.StringBuilder();
	sb.Append(text.text.Substring(text.text.Length/2));
	 text.text = sb.ToString();
}
}
