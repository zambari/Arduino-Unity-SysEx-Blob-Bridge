using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(Serial))]
public class SerialHelper : MonoBehaviour
{


    [SerializeField]
    SysexBlockTransfer syseqblock;
    public UnityEvent OnClockPulse;
    public UnityEvent OnActiveSense;
    public FloatEvent RXRate;
    public IntEvent OnByteRecieved;
    public IntEvent OnByteSent;
    int rxCount;
    float lastInterval;
    float measureInterval = 1;
    public string base64string;
    public bool tryC;
    public string plainstring;
    public bool tryD;
    void OnValidate()
    {
        if (tryC)
        {
            tryC = false;
            byte[] x = base64string.ToByteArray();
            byte[] b = System.Convert.FromBase64String(base64string);
            Debug.Log(b.Length);
            Debug.Log(b.ArrayToString());
        }
     
        if (tryD)
        {
            tryD = false;

     
                string encoded = System.Convert.ToBase64String(plainstring.ToByteArray());
                Debug.Log(encoded);

        }

          
    }

    void Start()
    {
       if (syseqblock==null) syseqblock = new SysexBlockTransfer();
        Debug.Log("Serial.checkOpen()=" + Serial.checkOpen());

    }


    public void sendAsSysexBlock(string s)
    {
        if (string.IsNullOrEmpty(s)) return;
        OnByteSent.Invoke(-3);
        OnByteRecieved.Invoke(-3);
        int encodedLen = syseqblock.buildEncoded(s);

        Serial.Write(syseqblock.bufferBase64, 0, encodedLen);
        for (int i = 0; i < encodedLen; i++)
            OnByteSent.Invoke(syseqblock.bufferBase64[i]);

    }

    public void sendByteCommand(int b)
    {
        int encodedLen = syseqblock.buildEncoded((byte)b);
        Serial.Write(syseqblock.bufferBase64, 0, encodedLen);
        for (int i = 0; i < encodedLen; i++)
            OnByteSent.Invoke(syseqblock.bufferBase64[i]);
    }

    public void OnSerialByte(byte c)
    {
        //        Debug.Log(((int)c).ToString());
        rxCount++;
        if (c == SysexBlockTransfer.MIDI_ACTIVE_SENSE)
        {
            OnActiveSense.Invoke();
            return;
        }
        if (c == SysexBlockTransfer.MIDI_CLOCK)
        {
            OnClockPulse.Invoke();
            return;
        }
        syseqblock.ParseByte(c);
        if (c!=SysexBlockTransfer.SYSEX_START && c!=SysexBlockTransfer.SYSEX_END)
        OnByteRecieved.Invoke(c);

    }
    public void OnSerialData(string c)
    {
        //  if (c.Length > 1) Debug.Log("long string! "+c.Length+" c[1]=="+(byte)c[1]);
        for (int i = 0; i < c.Length; i++)
        {
            OnSerialByte((byte)c[i]);
        }

    }


    void Update()
    {
        if (Time.time - lastInterval > measureInterval)
        {
            lastInterval = Time.time;
            RXRate.Invoke(rxCount / measureInterval);
            rxCount = 0;

        }

    }
}
