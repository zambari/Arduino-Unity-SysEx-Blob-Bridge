using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class stem : MonoBehaviour {
SerialPort serial;
[SerializeField]
string[] portNames;
public string portName = "/dev?";
//public int baudRate = 31250;
public int baudRate = 9600;
	// Use this for initialization
	void Start () {



/*
	portNames = System.IO.Ports.SerialPort.GetPortNames ();
Debug.Log(portNames.Length);
			if (portNames.Length == 0) {
				portNames = System.IO.Directory.GetFiles ("/dev/");                
			}

			foreach (string portName in portNames) {                                
				if (portName.StartsWith ("/dev/tty.usb") || portName.StartsWith ("/dev/ttyUSB"))
					Debug.Log(portName);
			}                
			Debug.Log("no morep orts");

*/
portName="/dev/tty.usbmodem1a1221";


		serial= new SerialPort(portName, baudRate);
		Debug.Log("serial baud :"+serial.BaudRate);
Debug.Log("buffer size is "+serial.ReadBufferSize);
		if (serial==null) Debug.Log("we failed");
		else
		{ 
			
			serial.Open();
			Debug.Log("serial baud :"+serial.BaudRate);
			serial.BaudRate=baudRate;
			if (serial.IsOpen)
			 Debug.Log("serial ok");
			 else Debug.Log("not open");

StartCoroutine(ReadSerialLoopWin());
		}
	//	var session = MidiNetworkSession.DefaultSession;
		
	}
	
	// Update is called once per frame
	void Update () {

	}
string s="";
	protected void receivedData (string data)
	{
		if (string.IsNullOrEmpty(s)) s="";
		if (s.Length>30) {
	Debug.Log(s); s="";
		}
		s+=data;

	}
	/// from unityserial
public int nCoroutineRunning;
	public IEnumerator ReadSerialLoopWin ()
	{

		while (true) {

			if (!enabled) {
				//print ("behaviour not enabled, stopping coroutine");
				yield break; 
			}

			//print("ReadSerialLoopWin ");
			nCoroutineRunning++; 
			//print ("nCoroutineRunning: " + nCoroutineRunning);

			string serialIn = "";
						int reader=serial.ReadByte();
					char c = (char)reader;

					
					serialIn += c;

			/*
			try {
		//		while (true) 
				{  // BytesToRead crashes on Windows -> use ReadLine or ReadByte in a Thread or Coroutine
					int reader=serial.ReadByte();
					char c = (char)reader;

					
					serialIn += c;

					//serialIn += s_serial.ReadLine();
				}

			} catch (System.TimeoutException) {
				//print ("System.TimeoutException in serial.ReadLine: " + te.ToString ());
			} catch (System.Exception e) {
				print ("System.Exception in serial.ReadLine: " + e.ToString ());
			}
*/
			if (serialIn.Length > 0) {
		
					receivedData (serialIn);
			}

			yield return null;
		}

	}

	static string GetPortName ()
	{

		string[] portNames;

		switch (Application.platform) {

		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXDashboardPlayer:
		case RuntimePlatform.LinuxPlayer:

			portNames = System.IO.Ports.SerialPort.GetPortNames ();

			if (portNames.Length == 0) {
				portNames = System.IO.Directory.GetFiles ("/dev/");                
			}

			foreach (string portName in portNames) {                                
				if (portName.StartsWith ("/dev/tty.usb") || portName.StartsWith ("/dev/ttyUSB"))
					return portName;
			}                
			return "";

		default: // Windows

			portNames = System.IO.Ports.SerialPort.GetPortNames ();

			// Defaults to last port in list (most chance to be an Arduino port)
			if (portNames.Length > 0)
				return portNames [portNames.Length - 1];
			else
				return "";
		}
	}
}
