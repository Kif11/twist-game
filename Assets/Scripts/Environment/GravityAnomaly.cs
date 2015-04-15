using UnityEngine;
using System.Collections;

public class GravityAnomaly : MonoBehaviour {

	private bool inAnomaly = false;
	private Collider player;

	void Update()
	{
		if(inAnomaly)
		{
			Rigidbody rb = player.GetComponent<Rigidbody>();
			rb.AddForce(Physics.gravity * rb.mass*-3);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Player entered an anomaly");
			player = other;
			inAnomaly = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Player leave the anomaly");
			
			inAnomaly = false;
		}
	}

}
