using UnityEngine;
using System.Collections;
// Young Chu
// Bullet behaviour useable for both enemy and player

public class BulletBehaviour : MonoBehaviour 
{
	// enum to decide whose bullet it is
	// change in inspector to whatever type of object is is attached to
	private enum bullet {PlayerBullet, EnemyBullet};

	[SerializeField]
	private bullet whoseBullet;

	// speed of bullet
	[SerializeField]
	private float speed = 50f;

	/// damage, set upon instantiation
	/// give a default value of 2.5 (lowest damage of weapons) so it always does SOME damage
	/// in case something goes wrong and damage isnt assigned upon spawn
	public float damage = 2.5f;

	// How long to wait before having bullet remove itself
	[SerializeField]
	private float destroyDelay = 1f;
	
	private GameObject player;

	// Use this for initializations
	void Start () 
	{
		// begin countdown for self destruction
		Destroy(this.gameObject, destroyDelay);

		switch (whoseBullet)
		{
		// if this bullet is enemy's bullet
		case bullet.EnemyBullet:
			// find player
			player = GameObject.FindGameObjectWithTag("Player");
			// look at player's position
			transform.LookAt(player.transform.position);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// getting bullet to move forward based on its transform.
		this.transform.Translate(this.transform.forward * speed * Time.deltaTime, Space.World);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Vector3 direction = player.transform.position;
		Gizmos.DrawRay(transform.position, direction);
	}

	void OnTriggerEnter (Collider other)
	{
		switch (whoseBullet)
		{
		case bullet.PlayerBullet:
			// checks so that only player bullets can hit enemies, that way enemy bullets dont hit themselves/ each other
			if(other.tag == "Enemy")
			{
				// call TakeDamage function on collided object tagged "Enemy" 
				other.GetComponent<EnemyValuesClass>().TakeDamage(damage);
				Destroy (this.gameObject);
			}
			break;
		case bullet.EnemyBullet:
			// reverse of above
			if(other.tag == "Player")
			{
				other.GetComponent<PlayerValues>().PlayerHealthLoss(damage);
				Destroy (this.gameObject);
			}
			break;
		}
		// and if it hits stuff tagged terrain, it just destroys itself so it doesnt go through it.
		if(other.tag == "Terrain")
		{
			Destroy(this.gameObject);
		}
	}
}
