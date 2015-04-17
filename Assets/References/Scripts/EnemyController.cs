using UnityEngine;
using System.Collections;
/// <summary>
/// Enemy controller.
/// Patrolls, Chases and shoots
/// </summary>

[RequireComponent(typeof(CharacterController))]
public class EnemyController : MonoBehaviour {
	private static EnemyController _instance;

	[SerializeField]
	private float speed_ 			= 0f;	// Set as patrolSpeed or chaseSpeed depending on the Coroutine
	private float patrolSpeed 		= 2.5f;
	private float chaseSpeed 		= 4f;

	private float patrolWaitTime 	= 5f;

	private float shootingRange 	= 5f;	// Fathest distance to start shooting
	private float shootingSpeed		= 2f;	// How offten enemy shoot bullet
	private int _weaponDamage 		= 50;	// Private version of WeaponDamage

	private Vector3				velocity 		= Vector3.zero;
	private CharacterController controller		= null;
	
	private GameObject targetObject	= null;	// For finding players transform.position
	private Transform target		= null;	
	
	private float distanceToPlayer;			// Distance from Enemy to Player
	private float minRange 			= 1;	// Minimum range for chasing
	private float maxRange 			= 10;	// Maximum range for chasing
	private float discoverRange;
	
	//Show debugging info?
	public bool DrawGizmos = true;

	// Use this for initialization
	void Awake () {

		controller = GetComponent<CharacterController> ();
		StartCoroutine("Patrolling");

		if(targetObject == null || targetObject != null)
			targetObject = GameObject.Find("Player");
		
		target = targetObject.transform; //Always player, targetObject finds player when awake;

		if(_instance == null)
		{
			_instance = this;
		}
	}
	
	void Update()
	{
		// Keep trank of distance to player
		distanceToPlayer = Vector3.Distance (transform.position, target.position);
		controller.Move (velocity * Time.deltaTime);
		DistanceCheck ();
	}
	
	/// <summary>
	/// Checks to see if player is within distance.
	/// </summary>
	/// <returns>The check.</returns>
	/// <param name="seconds">Seconds.</param>
	private  void DistanceCheck () 
	{
		
		float sneakEfficency = CharacterControllerCS.instance.SneakEfficency();
		// bool playerIsMoving  = CharacterControllerCS.instance.isMoving;
		
		// Debug.Log ("Sneek efficiency that enemy see " + sneakEfficency);
		
		discoverRange = maxRange / sneakEfficency;
		
		if (distanceToPlayer < discoverRange)
		{
			StartCoroutine ("Chasing");
		}	
	}


	public static EnemyController instance
	{
		get{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<EnemyController>();
			}
			return _instance;
		}
	}

	public int weaponDamage {
		get{
			return _weaponDamage;
		}
		set{
			_weaponDamage = value;
		}
	}


	/// <summary>
	/// Shooting Function called during chase
	/// </summary>
	IEnumerator Shooting (){
		// Spawns a bullet item that is a trigger and when hits the player, removes health_(in players script)
		// or dies after a few seconds (in its own script)
		yield return new WaitForSeconds (1f);
		GameObject bullet;

		bullet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		bullet.name = "Bullet";
		bullet.collider.isTrigger = true;
		bullet.AddComponent ("CharacterController");
		bullet.AddComponent ("BulletCS");
		bullet.transform.position = transform.position;
		StopCoroutine("Shooting");
	}


	/// <summary>
	///	Coroutine that moves the turns around and then goes back
	/// </summary>
	IEnumerator Patrolling(){
		
		speed_ = patrolSpeed;

		velocity += transform.forward * speed_;
		yield return new WaitForSeconds (3);
		velocity.z = 0;
		transform.Rotate (0, -0.5f, 0);
		yield return new WaitForSeconds (1);

		velocity -= transform.forward * speed_;
		yield return new WaitForSeconds (3);
		velocity.z = 0;
		transform.rotation = Quaternion.identity;
		yield return new WaitForSeconds (1);

		controller.Move (velocity * Time.deltaTime);
		StartCoroutine ("Patrolling");
	}


	/// <summary>
	/// Coroutine that chases the player
	/// </summary>
	IEnumerator Chasing(){
		
		transform.rotation = Quaternion.identity;
		StopCoroutine("Patrolling");

		speed_ = chaseSpeed;
		transform.LookAt (target);
		
		// Debug.Log (distanceToPlayer);
		
		// Stop at 3 unit before reaching to player
		if (distanceToPlayer > 3f)
			transform.Translate(Vector3.forward * Time.deltaTime*speed_);

		yield return new WaitForSeconds (shootingSpeed);	// Shooting bullet
		if ((distanceToPlayer < shootingRange)) 
		{
			StartCoroutine ("Shooting");
		}

		yield return new WaitForSeconds (patrolWaitTime); //after 5 seconds check if theyre near, if not patroll again
		if ((Vector3.Distance (transform.position, target.position) > maxRange)) 
		{
			StartCoroutine ("Patrolling");
			StopCoroutine ("Chasing");
		}

		yield return null;
	}
	
	// Called to draw gizmos for debugin
	void OnDrawGizmos()
	{
		if(!DrawGizmos) return;
		//Set gizmo color
		Gizmos.color = Color.blue;
		//Draw front vector - show the direction I'm facing
		Gizmos.DrawRay(transform.position, transform.forward.normalized * 4.0f);
		//Set gizmo color
		//Show proximity radius around cube
		//If cube were an enemy, they would detect the player within this radius
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, discoverRange);
		//Restore color back to white
		Gizmos.color = Color.white;
	}
}
