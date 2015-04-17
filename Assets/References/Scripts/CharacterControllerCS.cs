using UnityEngine;
using System.Collections;
/// <summary>
/// Player Character controller.
/// </summary>
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InventoryCS))]
public class CharacterControllerCS : MonoBehaviour 
{

	private static CharacterControllerCS _instance = null; //Needed for calling a private int from here 
	private CharacterController	charController 	= null;
	private InventoryCS			charInventory 	= null;

	[SerializeField]
	private float speed_ 			= 0;
	private float speedBase 		= 5;
	private float runSpeed			= 10;
	private float sneakSpeed		= 2.5f;		

	[SerializeField]
	// public float sneakEfficency 	= 1;			// Define you chance to be discovered by enemies
	private float sneakBase			= 10;
	public bool isMoving;
	private bool isSneaking;
	private bool isRunning;

	private float targetDist		= 0.05f;		// Used for mouse movement
	private Vector3 velocity		= Vector3.zero; // Used for keyboard movement

	private GameObject targetObject = null;
	private Transform target		= null;

	private int			currentDoorKeyNeeded = 1;	//Each door key ha a different item ID and is used here as this


//	public float sneakEfficency					//for detecting the player in enemy script
//	{
//
//		get{
//			return _sneakEfficency;
//		}
//		set{
//			_sneakEfficency = value;
//
//			if(speed_ == speedBase)
//				_sneakEfficency = sneakBase;
//			if(speed_ == runSpeed)
//				_sneakEfficency = sneakBase/2;
//			if(speed_ == sneakSpeed)
//				_sneakEfficency = sneakBase*2;
//		}
//	}

	public static CharacterControllerCS instance
	{
		get{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<CharacterControllerCS>();
			}
			return _instance;
		}
	}
	
	void Awake () 
	{
		speed_ 			= speedBase;
		//_sneakEfficency = sneakBase;
		
		// Debug.Log ("Awake sneak " + _sneakBase);

		charController 	= GetComponent<CharacterController>();
		charInventory	= GetComponent<InventoryCS>();

		targetObject = GameObject.Find ("Door" + currentDoorKeyNeeded);
		target = targetObject.transform;

		if(_instance == null)
		{
			_instance = this;
		}

	}

	void Update () 
	{
		// Debug.Log (sneakEfficency);
		// Debug.Log (target);
		// Debug.Log ("Sneak " + isSneaking);
		// Debug.Log ("Runing " + isRunning);
		// Debug.Log ("Moving " + isMoving);
		
		velocity.x = 0;
		velocity.z = 0;

		KeyMovement();
		SneakEfficency();
		DoorCheck();
		//Point character to mouse cursor by using raycast then calls coroutine to move
		if (Input.GetKey("space")) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, 100))
			{
				Vector3 newTarget = hit.point + new Vector3(0,1,0);
				StopCoroutine("MouseMovement");
				StartCoroutine("MouseMovement", newTarget);
			}
		}
	}
	
	
	/// <summary>
	/// This function used by EnemyController to adjust enemy discovering range.
	/// </summary>
	public float SneakEfficency ()
	{
		
		float sneakEfficency = 1f;
		
		// Change sneaking efficiency base on player control.
		if (isSneaking && isMoving)
		{
			sneakEfficency *= 2f;
		}
		else if (isRunning && isMoving)
		{
			sneakEfficency *= 0.5f;
		}
		// Debug.Log ("Sneak efficiency" + sneakEfficency);
		return sneakEfficency;
	}

	/// <summary>
	/// Moves the player to the target.
	/// </summary>
	/// <param name="target">The "Target" is the place where the mouse is </param>
	IEnumerator MouseMovement(Vector3 target)
	{
		while(Vector3.Distance(transform.position, target)>targetDist)
		{
			transform.position = Vector3.Lerp (transform.position, target, speed_*Time.deltaTime);
			transform.LookAt(Input.mousePosition);
			transform.rotation = Quaternion.identity;
			yield return null;
		}
	}

	/// <summary>
	/// Checks the if we have the key needed to move doors.
	/// </summary>
	private void DoorCheck()
	{
			if (((Vector3.Distance (transform.position, target.position) < 5)) && 
				(Vector3.Distance (transform.position, target.position) > .25f)) 
			{
					if (charInventory.CheckForItem (currentDoorKeyNeeded)) 
					{
						DoorCS.instance.OpenDoor ("Door" + currentDoorKeyNeeded);

						targetObject = GameObject.Find ("Door" + currentDoorKeyNeeded);
						target = targetObject.transform;
				
						currentDoorKeyNeeded++;
					} 
					else 
						Debug.Log ("nah we need key#" + currentDoorKeyNeeded);
			}
	}


	private void KeyMovement()
	{
		isMoving = false;
		isSneaking = false;
		isRunning = false;
		
		if(Input.GetKey ("a") || Input.GetKey("left"))
			velocity -= transform.right * speed_;
		else if(Input.GetKey ("d") || Input.GetKey("right"))
			velocity += transform.right * speed_;
		if(Input.GetKey ("w") || Input.GetKey("up"))
			velocity += transform.forward * speed_;
		else if(Input.GetKey ("s") || Input.GetKey("down"))
			velocity -= transform.forward * speed_;
			
		// Check if player moving
		// Debug.Log (charController.velocity);
		if (charController.velocity.magnitude > 0)
		{
			isMoving = true;
		}
		
		// Moves around but increases speed while shift is down
		if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift))
		{
			speed_ = runSpeed;
			isRunning = true;
		}	
		// Decreases speed by ctrl
		else if(Input.GetKey(KeyCode.LeftControl)||Input.GetKey(KeyCode.RightControl))
		{
			speed_ = sneakSpeed;
			isSneaking = true;
		}
		else
		{
			speed_ = speedBase;
		}
		
		velocity += Physics.gravity * Time.deltaTime;
		charController.Move (velocity * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{

		// When player encounters a Bonus, collect and add to PlayerStatistics
		if(other.tag == "Bonus")
			GameControllerCS.instance.PlayerStatistics[1]++;

		// When player encouters a key, collect
		if (other.tag == "Key" )
			charInventory.AddItem (currentDoorKeyNeeded);

		// If hit by a bullet, remove health
		else if (other.name == "Bullet") 
			GameControllerCS.instance.playerHealth -= EnemyController.instance.weaponDamage;
			
		if (other.tag == Tags.cameraFOV)
			Debug.Log ("Alarm trigered!");

		// If player enters the goal, load scene
		if (other.tag == "Goal" )
		{
			Debug.Log("Load next scene here");
			StartCoroutine(GameControllerCS.instance.LoadNextScene("Stats", 1));
			// GameControllerCS.instance.LoadNextScene("Stats", 1);
		}

		Destroy(other.gameObject);	//Always destroy the trigger after
	}
}