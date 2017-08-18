using System;
using UnityEngine;
using UnityEngine.Events;


//enum ArduinoCallOrders { cmdRestart, cmdCommand1, cmdCommand2};

[System.Serializable]
public class SysexBlockTransfer
{
    public const byte MIDI_ACTIVE_SENSE = 0xFE;
    public const byte MIDI_CLOCK = 0xF8;
    public const byte SYSEX_START = 0xF0; // (byte)'1';//
    public const byte SYSEX_END = 0xF7;//(byte)'2';/


    const int BUFSIZE = 40;
    const int BUFSIZE64 = 60;
    public byte[] bufferRaw;
    public byte[] bufferDecoded;
    public byte[] bufferBase64;
    bool isRecieving;
    int recieveIndex;

    public StringEvent OnMessageDecodedAsString;

    public ByteArrayEvent OnMessageDecodedEvent;


    protected virtual void DecodeBuffer()
    {
        string encodedString = "";
        for (int i = 0; i < recieveIndex; i++)
            encodedString += (char)bufferBase64[i];

      
        // below is the ugliest code I ever wrote, but two evenings sorting this out was enough
        bool success=false;
        try   {   
            bufferDecoded = Convert.FromBase64String(encodedString);
            success=true;
         } catch   {   }
        if (!success)     try  {   
            encodedString += '=';
            bufferDecoded = Convert.FromBase64String(encodedString);
            success=true;
          } catch   {   }
         if (!success)   try     {
            encodedString += '=';
            bufferDecoded = Convert.FromBase64String(encodedString);
            success=true;
        } catch  {   }
        if (!success)    try   {
                 encodedString += '=';
            bufferDecoded = Convert.FromBase64String(encodedString);
            success=true;
        } catch  {   }
         if (!success)    try     { 
             encodedString += '=';
            bufferDecoded = Convert.FromBase64String(encodedString);
            success=true;
      } catch  {   }
        if (!success)  try  {   
              encodedString=encodedString.Substring(0,recieveIndex-1);
            Debug.Log("trying "+encodedString);
            bufferDecoded = Convert.FromBase64String(encodedString);
            success=true;
        }    catch  {  }
        if (success) 
        { 
            Debug.Log(" OK "); 
            OnMessageDecodedEvent.Invoke(bufferDecoded);
            OnMessageDecodedAsString.Invoke(bufferDecoded.ArrayToString());
     
        }
        else Debug.Log("FAILED");

        
    }

    public int buildEncoded(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;
        string encoded = System.Convert.ToBase64String(s.ToByteArray());
        return fillArrayWithString(encoded, bufferBase64); ;
    }
    public int buildEncoded(byte b)
    {
        string s = ((char)b).ToString();
        string encoded = System.Convert.ToBase64String(s.ToByteArray());
        return fillArrayWithString(encoded, bufferBase64); ;
    }
    public SysexBlockTransfer()
    {
        bufferRaw = new byte[BUFSIZE];
        bufferBase64 = new byte[BUFSIZE64];
    }
    public void SetByte(int index, byte b)
    {
        bufferRaw[index] = b;
    }


    /// <summary>
    /// This method should be used to grap bytes as they come via serial
    /// </summary>
    /// <param name="b"></param>

    public void ParseByte(byte b)
    {
        if (b == MIDI_ACTIVE_SENSE || (b == MIDI_CLOCK)) return;// ignoring these

        if (isRecieving)
        {
            if (b == SYSEX_END)
            {
                isRecieving = false;
                recieveIndex--;
                DecodeBuffer();

                return;
            }
            else
            {
                bufferBase64[recieveIndex] = b;
                recieveIndex++;
            }
        }
        else
        {
            if (b == SYSEX_START)
            {
                isRecieving = true;
                recieveIndex = 0;
                return;
            }
        }
    }
    int fillArrayWithString(string s, byte[] targetArray)
    {
        int l = s.Length;
        targetArray[0] = SYSEX_START;
        for (int i = 0; i < l; i++)
        {
            targetArray[i + 1] = (byte)s[i];
        }
        targetArray[l + 1] = SYSEX_END;
        return l + 2;
    }

    /*
    
      public void OnSerialByte(byte c)
    {
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
    }
    
     */

}