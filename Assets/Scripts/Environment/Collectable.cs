using UnityEngine;
using System.Collections;
// Kirill

public class Collectable : MonoBehaviour {

	private PlayerValues singleton;

	void Start()
	{
		singleton = PlayerValues.instance;
	}

	void OnTriggerEnter(Collider other)
	{
		// Check if collectable collide with player
		if (other.tag == "Player") 
		{
			if (this.tag == "Health") {
				other.GetComponent<SimpleInventory>().hkit += 1;
				Debug.Log ("Health is collected");
				Destroy (this.gameObject);
			}
			if(this.tag == "Quest")
			{
				Debug.Log ("Objective complete, changing levels soon.");
				StartCoroutine(singleton.LoadNextScene("level_02", 0.2f)); 
			}
		}
	}
}
