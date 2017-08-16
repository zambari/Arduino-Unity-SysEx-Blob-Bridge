# Arduino-Unity-SysEx-Blob-Bridge
This pair of scripts exchanges binary blobs between Unity and Arduino  via MIDI-Compatible SysEx messages.

By using Sysex signaling the stream is 'invisible' to other devices in the chain, yet is binary safe.

While this can be used as a more general serialisation mechanism, here implemented in a most simple possible manner, the use case here is to be used as a way to write a Unity-Based setup edtior for a devide that's Arduino based.

There's countless ways to achieve this, but here the constraint was that I want the MIDI sequencer I am building to remain functional, as in send and recieve normal MIDI messages, and don't disrupt other MIDI devices in the chain.

MIDI is a serial, one to one, unidirectional protocol, compatible with RS232 in terms of logical levels (as long as you force it to non-standard baud of 31250). Its not quite compatible electrically though as you need a transoptor on the input stage, and be prepared to drive such isolator on the output stage. 

 Arduino, which is a single USART microcontroller, cannot operate two bauds at the same time, so while MIDI Connection is required, you cannot communicate with the device via USB. This is a solution to this particular problem, but it also creates a very defined transmission state for binary objects, so CRC checks, type casting etc could be achieved (proof of concept at the moment)
 
 Normally, transmission is simple
 

	┌────────────┐     (usb OR midi, not at the same time)                     
	│            │                          ┌────────────────────────────────┐
 	│ Arduinoul  │  ←-- <?> -- MIDI ---→    │  SYNTH                         │
	│            │                          │    ┌ █ █ │ █ █ █ │ █ █ │ █ █ █┬┘
	└────────────┘                          └────┘─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴┘
			         
			                        ┌────────────┐                  
	   no computa        ↑                  │            │                  
 	    connection        ---- USB -----→   │    PC      │    
	                                        │            │                    
	                                        └────────────┘                
	                      
                       
                     
                       
                       
Problem is what happens if you use your Arduino in a more involved setup



	┌────────────┐                  ┌────────────┐  
	│            │                  │            │ ---- MIDI----→   THE GUYS (midi clock splitting)
 	│ Arduinoul  │  ---- MIDI----→  │    BCR     │ ----     
	│            │  ←----           │            │    │ 
	└────────────┘       │          └────────────┘    │ 
	                     │                            │ 
	                     │                            │    			Split NRPN Loop
	            ┌────────────┐                        │			for visual feedback 
	            │            │                        │ 
	            │     ESX    │ ←--------- MIDI--------   
	            │            │                  
	            └────────────┘                                  


MIDI is unidirectional, but you can make loops, so you can get your signal back even after going through a few hops

	┌────────────┐                  ┌────────────┐  
	│            │                  │            │ ---- MIDI----→    THE GUYS (midi clock splitting)
 	│ Arduinoul  │  ---- MIDI----→  │    BCR     │ ----               ┌────────────────────────────────┐
	│            │  ←----           │            │    │               │  SYNTH                         │
	└────────────┘       │          └────────────┘    │               │    ┌ █ █ │ █ █ █ │ █ █ │ █ █ █┬┘ 
	                     │                            │               └────┘─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴┘  
	                     │                            ↓    
	            ┌────────────┐                  ┌────────────────┐
	            │            │                  │ ▆▆▆▆▆▆▆▆ │  
	            │    ESX     │ ←---- MIDI----   │ ▆ PC / UNITY   │
	            │            │                  │ ▆▆▆▆▆▆▆▆ │  C# prototyping tool
	            └────────────┘                  └────────────────┘                   



Base64 Library by adamvr used to encode and decode blobs that do not use the 8th bit in the MIDI channel is reserved).

https://github.com/adamvr/arduino-base64/blob/master/Base64.cpp
