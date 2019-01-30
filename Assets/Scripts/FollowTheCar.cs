using UnityEngine;

public class FollowTheCar : MonoBehaviour {
	
	public Transform theCar;
	public Vector3 offset;

	// Update is called once per frame
	void Update () {
		
		transform.position = theCar.position + offset;
	}
}
