

// comment out to disable serial echo
#define SERIAL_ECHO 

#define SYSEX_END 0xF7
#define SYSEX_START 0xF0
#define MIDI_ACTIVE_SENSE B11111110
#define MIDI_CLOCK 0xF8
#define BUFSIZE 30
#define BUFSIZE64 45
#define BAUD_MIDI 31250
#define BAUD_USART 9600


#define USE_BAUD BAUD_MIDI
#define MIDI_ACTIVE_UPDATE_TIME 500

#include <LinkedList.h>
#include <Base64.h>
long nextMills;
long nextClk;



//https://github.com/zambari/Arduino-Unity-SysEx-Blob-Bridge

/// SYSEX TRANSMIT BEGIN
/// a library maybe

class SysexTransmit{
uint8_t bufferRaw[BUFSIZE];
uint8_t bufferBase64[BUFSIZE64];
uint8_t recieveIndex;
bool isRecieving=false;


  public:
    void TransmitBlob(char * s)
    { 
        int counter=0;
        while(s[counter]!=0 && counter<BUFSIZE)
        counter++;
        TransmitBlob(s,counter);
    }
  void TransmitBlob(char * s, int blobByteCount)
  { 
/*
    Serial.print("blobtramit\ncount is ");
    Serial.print(blobByteCount);
    Serial.print("\n");  
    Serial.print(s); 
     Serial.print("\n");*/

    for (int i=0;i<blobByteCount;i++)
    bufferRaw[i]=s[i];
    int encodedLen= base64_encode(bufferBase64,bufferRaw,blobByteCount); // *output, *input, *le
    Serial.print((char)SYSEX_START);
    for (int i=0;i<blobByteCount;i++)
        Serial.print((char)bufferBase64[i]);
    Serial.print((char)SYSEX_END);
  }

  void Restart() // Restarts program from beginning but does not reset the peripherals and registers
{
        asm volatile ("  jmp 0");  
} 

enum ArduinoCallOrders { cmdRestart, cmdCommand1, cmdCommand2};

void  RunCommand(int cmd,  char* blobPointer, int decodedLen)   
{      Serial.print("\n runCommand \n ");
        Serial.print(cmd);
        Serial.print("\n");
        switch (cmd)
        {
        case 0: Restart();
        case 1:

            TransmitBlob("ala ma ko");  // here you can light a LED or whaevert
        break;
        case 2:
            TransmitBlob("ala ma kota");
        break;
        case 3:
            TransmitBlob("ala ma kotaz");
        break;
        case 4: 
        TransmitBlob("ala ma kotazaa");
        break;
        case 5: 
          TransmitBlob("ala ma kotazaza");
        break;
        case 6: 
          TransmitBlob("ala ma kotazazaza");
        break;
        }
}

void  OnRecieveBlob(  char* blobPointer, int decodedLen)        // DO USEFUL STUFF HERE
{
  RunCommand(blobPointer[0],blobPointer+1,decodedLen-1);
}

  bool handleRpc(char thisChar)  // need to escape character
  { 
    if (thisChar==(char)SYSEX_START)
        {
          if (isRecieving) Serial.print("sysex stat while recireving");
          isRecieving=true;
          recieveIndex=0;

    } else
    if (isRecieving)
    { 
          if (thisChar==(char)SYSEX_END)
          {
            isRecieving=false;
            int decodedLen=  base64_decode(bufferRaw,bufferBase64,recieveIndex);
            OnRecieveBlob(bufferRaw,decodedLen);
         }
          else
          {
              bufferBase64[recieveIndex]=thisChar;
              recieveIndex++;
               if (recieveIndex>=BUFSIZE64)
               {
                Serial.print("\nBuffer Overflow\n");
                isRecieving=false;
                return false;
               }    
          }
        return true;    
    } 
    else
    {
     #ifdef SERIAL_ECHO
      Serial.print("ECHO:");    Serial.print(thisChar); Serial.print('\n');
      #endif
    }
  }
};
SysexTransmit transmit;

/// SYSEX TRANSMIT END


// plain old arduino code:

void setup() {
    //Serial.begin(BAUD_MIDI);
    Serial.begin(USE_BAUD); // for easier debugging
    Serial.println("\nArduino Started\n");

}



void loop() {
  while (Serial.available())
   transmit.handleRpc(Serial.read());
  
  // OPTIONAL Keepalive BEGIN
  if (millis()>nextMills)
  {
    nextMills=millis()+MIDI_ACTIVE_UPDATE_TIME;
    Serial.write(MIDI_ACTIVE_SENSE);
  }
  // OPTIONAL END
  
}


