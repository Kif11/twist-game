using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	public int healthGain = 10;

	void OnTriggerEnter(Collider other)
	{
		// Check if collectable collide with player
		if (other.tag == "Player") 
		{
			if (this.tag == "Health") {
				other.GetComponent<PlayerValues>().PlayerHealthGain(healthGain);
				Debug.Log ("Health is collected");
				Destroy (this.gameObject);
			}
		}
	}
}
