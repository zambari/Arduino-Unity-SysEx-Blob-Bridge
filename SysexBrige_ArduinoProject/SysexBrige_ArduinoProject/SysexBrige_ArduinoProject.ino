
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
#define MIDI_ACTIVE_UPDATE_TIME 500

#include <LinkedList.h>
#include <Base64.h>
long nextMills;
long nextClk;
/*
┌───────┐                  ┌───────┐  
│            │                  │            │ ---- MIDI----→    THE GUYS (midi clock splitting)
│ Arduinoul  │  ---- MIDI----→ │    BCR     │ ----              ┌─────────────────────────┐
│            │  ←----          │            │    │             │  SYNTH                                    │
└───────┘      │          └───────┘    │             │    ┌ █ █ │ █ █ █ │ █ █ │ █ █ █┬┘ 
                     │                              │             └───  
                     │                               ↓    
            ┌───────┐             ┌─────────┐
            │            │             │ ▆▆▆▆▆▆▆▆ │  
            │    ESX     │ ←-- MIDI---│ ▆ PC / UNITY   │ <- YOU ARE HERE
            │            │             │ ▆▆▆▆▆▆▆▆ │  C# prototyping tool
            └───────┘             └─────────┘                   


*/



/// SYSEX TRANSMIT BEGIN
/// a library maybe

class SysexTransmit{
uint8_t bufferRaw[BUFSIZE];
uint8_t bufferBase64[BUFSIZE64];
uint8_t recieveIndex;
bool isRecieving=false;


  public:
  void TransmitBlob(char * s, int blobByteCount)
  { 
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
void runCommand1()
{
  Serial.print("commadn1");
}
void runCommand2()
{
  Serial.print("commadn2");


}
enum ArduinoCallOrders { cmdRestart, cmdCommand1, cmdCommand2};


void  OnRecieveBlob(  char* blobPointer, int decodedLen)        // DO USEFUL STUFF HERE
{Serial.write("recieved");
  // an exapmle is a restart order, or other sort of basic RPC
     
  char command=blobPointer[0];

   switch (command )
   {
     case (cmdRestart ) : 
       Serial.print("restarting");
       Restart();
     break;
 
      
     case (cmdCommand1) : 
      runCommand1();
     break;
         
     case (cmdCommand2) : 
       runCommand2();
     break;
   }

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
    Serial.begin(BAUD_USART); // for easier debugging
    Serial.println("\nArduino Restarted\n");

}




void loop() {
  while (Serial.available())
   transmit.handleRpc(Serial.read());
  
  // OPTIONAL BEING
  if (millis()>nextMills)
  {
    nextMills=millis()+MIDI_ACTIVE_UPDATE_TIME;
    Serial.write(MIDI_ACTIVE_SENSE);
  }
  // OPTIONAL END
  
}


