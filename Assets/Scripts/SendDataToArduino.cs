using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System;

public class SendDataToArduino : MonoBehaviour {

	public static SerialPort sp = new SerialPort("COM9", 9600);
	public SpeedCaculator sd;
	float timePassed = 0.0f;


	void Start () {

		sd = GetComponent<SpeedCaculator>();
		OpenConnection();
	}

    private void OpenConnection()
    {
        if (sp != null){
			
			if (sp.IsOpen){

				sp.Close();
				print("Closing port because it was already open!");
			}else{

				sp.Open();
				sp.ReadTimeout = 200;
				print("Port is open now");
			}
		}else {

			if (sp.IsOpen){

				print("Port is already open and sp is null!");
			}else{

				print("Port is open now but sp is null!");
			}
		}
    }

	public void onApplicationQuit(){

		sp.Close();
	}

	public void sendDistances(){

		Debug.Log("I am sending data to Arduino now");
		sp.Write("car0" + sd.distances[0].ToString());
		sp.Write("car1" + sd.distances[1].ToString());
		sp.Write("car2" + sd.distances[2].ToString());
		sp.Write("car3" + sd.distances[3].ToString());
	}
}
