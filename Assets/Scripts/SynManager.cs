using UnityEngine;
using System.Threading;
using System.Collections;
using System.IO.Ports;
using System;

public class SynManager : MonoBehaviour {

	public string messageFromArduino;
	private Thread thread;
	private Queue outputQueue; //From Unity to Arduino
	private Queue inputQueue; // From Arduino to Unity
	public static SerialPort stream;
	public bool looping = true;

	void Start () {
		StartThread();
	}

	public void StartThread(){
		outputQueue = Queue.Synchronized(new Queue());
		inputQueue = Queue.Synchronized(new Queue());

		thread = new Thread(ThreadLoop);
		thread.Start();
	}

	public void StopThread(){
		lock(this){
			looping = false;
		}
	}

	public bool IsLooping(){
		lock(this){
			return looping;
		}
	}

	public void ThreadLoop(){
		// open the connection
		stream = new SerialPort("\\\\.\\COM9",9600);
		stream.ReadTimeout = 50;
		stream.Open();

		// loop
		while(IsLooping()){
			// send data to Arduino
			if (outputQueue.Count != 0){
				string command = outputQueue.Dequeue().ToString();
				WriteToArduino(command);
			}

			// read data from Arduino
			messageFromArduino = ReadFromArduino();
			Debug.Log(messageFromArduino);
			if (messageFromArduino != null){
				inputQueue.Enqueue(messageFromArduino);
			}
		}
	}

	public void OnApplicationQuit(){
		stream.Close();
	}

	public void WriteToArduinoWithQueues(string command){
		command = "6,"+command+","+"5,"+command+",";
		outputQueue.Enqueue(command);
	}

	public string ReadFromArduinoWithQueues(){
		if (inputQueue.Count == 0)
			return null;
		return (string) inputQueue.Dequeue();
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
}
