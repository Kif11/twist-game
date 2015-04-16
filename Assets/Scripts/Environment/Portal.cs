using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public GameObject otherPortal;
	
	private bool teleporting = false;
	public int direction = 1;

	void OnTriggerEnter(Collider other) 
	{
	
		if (other.tag == "Player")
		{
			Debug.Log (otherPortal.transform.position);
			Debug.Log (otherPortal.transform.right);
			if (!teleporting)
				StartCoroutine(teleport(other));
		}
		
	}
	
	IEnumerator teleport (Collider other)
	{
		teleporting = true;
		other.transform.position = otherPortal.transform.position + otherPortal.transform.right * direction;
		yield return new WaitForSeconds(1f);
		teleporting = false;
	}
}
