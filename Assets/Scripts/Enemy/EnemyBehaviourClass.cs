using UnityEngine;
using System.Collections;
// Young Chu
// Behaviour class for BASIC enemies

//TODO: Instead of exposing BuletPrefab and Enemy Bulet class parameters as public
// need to require them. It is anoing to specify them every time in the editor.

public class EnemyBehaviourClass : MonoBehaviour 
{
	
	/// <summary>
	/// Enemy states
	/// </summary>
	// Make a list of our states (alive, dead, jumping, attacking, etc.)
	public enum enemyState {idle, attacking}; //as long as you want
	// Store into an enemyState type variable
	public enemyState currentState;

	private PlayerValues instance;

	/// <summary>
	/// Range detection variables
	/// </summary>
	// Range of detection
	public float		detectRadius = 25f;
	// the target's transform/position, assigned from Start()
	public Transform target = null;
	// calculating distance between enemy and target
	public float distanceFromTarget;
	// bool for if player has been sighted
	public bool targetSighted = false;

	/// <summary>
	/// Shooting variables
	/// </summary>
	// Assign from inspector
	public GameObject bulletPrefab;
	// reference to prefab's bullet behaviour
	public 	BulletBehaviour enemyBulletClass;
	// can enemy fire?
	public bool canFire = true;
	// delay between each shot
	public float delay = 1f;
	// dmg of basic enemy shots
	public float dmg = 10f;
	
	
	void Start () 
	{
		// assign default enemy state, just in case
		currentState = enemyState.idle;
		
		// find transform of target using its tag
		// hopefully theres always something tagged Player, otherwise an error occurs
		target = GameObject.FindGameObjectWithTag("Player").transform;

		instance = PlayerValues.instance;
	}

	/// <summary>
	/// Checking where player is in relation to this enemy, and what state this enemy is in
	/// seems it can be overrided as well, not sure if there are negative repercussions though.
	/// </summary>
	public virtual void Update () 
	{
		// constantly assigning the distance from 'target' if 'target' isn't null
		if(target != null)
		{
			distanceFromTarget = Vector3.Distance (target.position, this.transform.position);
		}
		
		// if the distance from target is within detection range...
		if(distanceFromTarget <= detectRadius && target != null)
		{
			// ... run line of sight function
			CheckSight();
		}
		// otherwise...
		else
		{
			// ... back to idle
			currentState = enemyState.idle;
		}
		
		switch (currentState) 
		{
			// what happens during each state
		case enemyState.idle:
			// this should typically be true
			targetSighted = false;
			break; // Break out
		case enemyState.attacking:
			// calls attack coroutine if can fire and player is in line of sight AND if player's health is more than 0
			if(canFire == true && targetSighted == true && instance.health > 0)
			{
				StartCoroutine(EnemyAttack ());
			}

			break;
		}
	}

	/// <summary>
	/// Overridable attack function for BASIC enemies
	/// Basically spawns a bullet and sets damage for bullet
	/// </summary>
	public virtual IEnumerator EnemyAttack()
	{
		canFire = false;
		// Bullet prefab is currently assigned through inspector, since I dont know how to use Resource folder
		Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

		// Get bullet prefab's bulletbehaviour component and set enemyclass var as that
		enemyBulletClass = bulletPrefab.GetComponent<BulletBehaviour>();
		
		// set bullet damage
		enemyBulletClass.damage = dmg;


		yield return new WaitForSeconds (delay);
		canFire = true;
	}

	// see visual range of detection
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, detectRadius);
		//Gizmos.DrawSphere(transform.position, detectRadius);
	}

	// Checks if player is in unobstructed view for the enemy
	void CheckSight()
	{
		RaycastHit hit;

		/// <summary>
		/// create a line starting at enemy position and ending at 'target' position and returns true when the line hits anything on the way
		/// Linecast automatically ignores colliders in the layer "IgnoreRaycast" 
		/// Bullets and pickups should be placed in this layer, so enemy doesn't lose sight of player when it shoots
		/// </summary>
		if(Physics.Linecast(transform.position, target.position, out hit)) 
		{
			// if the collider hit by linecast is tagged Player...
			if(hit.collider.tag == "Player")
			{
				// ... set sighted to true; commence attacking
				targetSighted = true;
				currentState = enemyState.attacking;
			}
			// otherwise...
			else
			{
				// target not sighted; back to idling
				targetSighted = false;
				currentState = enemyState.idle;
			}

		}

	}
}
