using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	public int healthGain = 10;

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
				other.GetComponent<PlayerValues>().PlayerHealthGain(healthGain);
				Debug.Log ("Health is collected");
				Destroy (this.gameObject);
			}
			if(this.tag == "Quest")
			{
				Debug.Log ("Objective complete, changing levels soon.");
				StartCoroutine(singleton.LoadNextScene("level_02", 2f)); 
			}
		}
	}
}
