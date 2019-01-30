using UnityEngine;
using UnityEngine.UI;

public class SpeedCaculator : MonoBehaviour {

	public Transform theCar;
	public Transform car0;
	public Transform car1;
	public Transform car2;
	public Transform car3;
	public float[] distances;
	public Text distancesText;
	public SendDataToArduino sendData;

	void Start(){
		sendData = GetComponent<SendDataToArduino>();
	}
	
	// Update is called once per frame
	void Update () {
		
		distances = new float[4];
		distances[0] = Vector3.Distance(theCar.position, car0.position);
		distances[1] = Vector3.Distance(theCar.position, car1.position);
		distances[2] = Vector3.Distance(theCar.position, car2.position);
		distances[3] = Vector3.Distance(theCar.position, car3.position);
		distancesText.text = "car0: " + distances[0] + 
							"\ncar1: " + distances[1] + 
							"\ncar2: " + distances[2] + 
							"\ncar3: " + distances[3];
		sendData.sendDistances();
	}
}
