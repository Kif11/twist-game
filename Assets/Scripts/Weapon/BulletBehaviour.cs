using UnityEngine;
using System.Collections;
// Young Chu
// Bullet behaviour useable for both enemy and player

public class BulletBehaviour : MonoBehaviour 
{
	// speed of bullet
	[SerializeField]
	private float speed = 50f;

	// damage of bullet
	public float damage = 2.5f;

	// How long to wait before having bullet remove itself
	[SerializeField]
	private float destroyDelay = 1f;

	// reference to player
	private GameObject player;

	// Use this for initializations
	void Start () 
	{
		// begin countdown for self destruction
		Destroy(this.gameObject, destroyDelay);
		
		// find player
		player = GameObject.FindGameObjectWithTag("Player");
		// look at player's position
		transform.LookAt(player.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// getting bullet to move forward based on its transform.
		this.transform.Translate(this.transform.forward * speed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter (Collider other)
	{
		// if object hit was tagged player
		if(other.tag == "Player")
		{
			// deal damage using get/set from health function
			other.GetComponent<PlayerValues>().health -= damage;
			// destroy bullet
			Destroy (this.gameObject);
		}

		// and if it hits stuff tagged terrain/Environment, it just destroys itself so it doesnt go through it.
		else if(other.tag == "Terrain" || other.tag == "Environment")
		{
			Destroy(this.gameObject);
		}
	}
}
