using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public GameObject otherPortal;
	public bool rightPortal;
	
	private int dropDirection;

	void OnTriggerEnter(Collider other) 
	{
		if (rightPortal)
			dropDirection = 1;
		else
			dropDirection = -1;
	
		if (other.tag == "Player") 
		{
			other.transform.position = otherPortal.transform.position + otherPortal.transform.right * dropDirection;
		}
		
	}
}
