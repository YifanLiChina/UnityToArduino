using UnityEngine;

public class speedManager : MonoBehaviour {

	public Rigidbody rb;
	public float forwardForce = 2000f;
	public float sidewaysForce = 500f;

	void FixedUpdate()
	{
		rb.AddForce(0, 0, forwardForce * Time.deltaTime);
	}
}
