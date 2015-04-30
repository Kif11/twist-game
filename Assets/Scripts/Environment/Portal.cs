using UnityEngine;
using System.Collections;
// Kirill Kovalevskiy

public class Portal : MonoBehaviour {

	// Linked portal
	public GameObject otherPortal;

	private GameObject player;
	// Class to hold all player related values
	private PlayerValues playerValues;
	private Vector3 otherPortalLocalTransfrm;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerValues = player.GetComponent<PlayerValues>();
	}

	
	void OnTriggerEnter(Collider other) 
	{
	
		Debug.Log (playerValues.isTeleporting);

		// If Player enter the portal
		if (other.tag == "Player")
		{
			// Teleport the player
			// Won't run until the previous teleport is finished
			if (!playerValues.isTeleporting)
				StartCoroutine(teleport(otherPortal));
		}
		
	}
	
	IEnumerator teleport (GameObject otherPortal)
	{

		playerValues.isTeleporting = true;

		// Move teleported object to otherPortal position
		// I add otherPortal local transformation in order to push the object slightly in front of otherPortal
		player.transform.position = otherPortal.transform.position + otherPortal.transform.right;

		// Portall culldown for avoid double teleportation
		yield return new WaitForSeconds(1);
		playerValues.isTeleporting = false;

	}
}
