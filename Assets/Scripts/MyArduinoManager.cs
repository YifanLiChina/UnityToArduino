using UnityEngine;
using System.IO.Ports;
using System;

public class MyArduinoManager : MonoBehaviour {

	// public static string myPort = "COM10";
	// for windows
	// public static string myPort = "\\\\.\\COM10";
	// public static int baudrate = 9600;
	// public static SerialPort stream = new SerialPort(myPort, baudrate);
	public static SerialPort stream = new SerialPort("\\\\.\\COM10",9600);
	public string vibratorSpeed = "200";
	public string messageFromArduino;

	// Use this for initialization
	void Start () {
		Open();
	}
	
    // Open the serial port
	public void Open()
    {
        if (stream != null){
			
			if (stream.IsOpen){

				stream.Close();
				print("Closing port because it was already open!");
			}else{

				stream.Open();
				stream.ReadTimeout = 200;
				print("Port is open now");
			}
		}else {

			if (stream.IsOpen){

				print("Port is already open and stream is null!");
			}else{

				print("Port is open now but stream is null!");
			}
		}
    }

    // Update is called once per frame
    void Update () {
		messageFromArduino = ReadFromArduino();
		Debug.Log("I received " + messageFromArduino + " from Arduino");
	}
 
	public void WriteToArduino(string message){
		message = "6,"+message+","+"5,"+message+",";
		stream.Write(message);
		Debug.Log("I is writing " + message + " to Arduino now");
		stream.BaseStream.Flush();
	}

	public string ReadFromArduino(){
		stream.ReadTimeout = 25;
		try{
			return stream.ReadLine();
		}catch(TimeoutException){
			return "nothing";
		}
	}

	public void OnApplicationQuit(){
		stream.Close();
	}
}
