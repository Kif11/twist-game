using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// Young Chu
// Simple NON-Dynamic inventory-esque component
// This script assumes each button press/item slot is dedicated to only one item forever
// eg. 1 is always Health Potion, 2 is always Mana Potion, 3 is always etc..

// Requires Rigidbody because of the Reverse Gravity item
[RequireComponent(typeof(Rigidbody))]
public class SimpleInventory : MonoBehaviour 
{
	/// <summary>
	/// Simple inventory
	/// Doesn't use built-in arrays/enums/classes/etc since it only
	/// has to keep track of one value of the items (amount)
	/// </summary>

	/// <summary>
	/// Integers containing amount of each item
	/// rotL 		= Rotate Left artifact
	/// rotR 		= rotate right artifact
	/// gravRev 	= Reverse gravity artifact
	/// gravLow 	= low gravity artifact
	/// hkit 		= health kit
	/// </summary>
	[SerializeField]
	[Tooltip("Current number of Rotate Left Artifacts in inventory")]
	private int _rotL 		= 0;

	[SerializeField]
	[Tooltip("Current number of Rotate Right Artifacts in inventory")]
	private int _rotR 		= 0;

	[SerializeField]
	[Tooltip("Current number of Reverse Gravity Artifacts in inventory")]
	private int _gravRev	= 0;

	[SerializeField]
	[Tooltip("Current number of Health Kits in inventory")]
	private int _hkit 		= 0;

	/// <summary>
	/// Booleans for button presses of 1/2/3/4
	/// 1 = rotL
	/// 2 = rotR
	/// 3 = gravRev
	/// 4 = hkit
	/// </summary>
	private bool oneP;
	private bool twoP;
	private bool threeP;
	private bool fourP;

	/// <summary>
	/// Environment component reference
	/// Environment tagged object is the parent of all things in the level when they rotate
	/// </summary>
	private Environment environmentRef;

	/// <summary>
	/// Reverse Gravity
	/// </summary>
	private Rigidbody rb = null;
	public bool inverseGravity = false;

	// Text UI to give notifications
	private GameObject noteObj;
	private Text noteText;

	// preset strings
	private string needmore = "Insufficient";
	private string cantuse = "Unable to activate";


	void Start()
	{
		// running function to find reference to Environment
		SetEnvironment();

		// setting rb
		rb = this.GetComponent<Rigidbody>();

		// assigning text vars
		noteObj = GameObject.FindGameObjectWithTag("Notes");
		noteText = noteObj.GetComponent<Text>();
	}


	// Have this function run whenever new scene is loaded, so it'll get new Environment
	public void SetEnvironment()
	{
		// Find object tagged Environment and gets component 'Environment' component and assigns to var
		environmentRef = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}


	// resets everything in inventory except health kits
	// use when restarting (and then reset health kits also)
	// or when moving to new level
	public void ResetInventory()
	{
		rotL = 0;
		rotR = 0;
		gravRev = 0;
		// adding lots of force downwards on player just in case to negate
		// residual upward force from previous level
		rb.AddForce(Physics.gravity * 200);
	}


	void Update()
	{
		// assigns key presses to the bools
		oneP = Input.GetKeyDown(KeyCode.Alpha1);
		twoP = Input.GetKeyDown(KeyCode.Alpha2);
		threeP = Input.GetKeyDown(KeyCode.Alpha3);
		fourP = Input.GetKeyDown (KeyCode.Alpha4);

		// If oneP is pressed and environment isnt already rotating
		if(oneP && !environmentRef.rotating)
		{
			// and if Rotate Left uses is more than 0
			if(_rotL > 0)
			{
				// set rotate direction
				environmentRef.direction = Vector3.forward;
				// rotate
				environmentRef.CallRotate ();
				// Taking away a use, use the get/set function to be extra defensive
				rotL -= 1;
			}
			// otherwise (if !> 0 uses)
			else if(_rotL <= 0)
			{
				StartCoroutine(DisplayNote(needmore, "Rotate Left Artifacts"));
			}
			else
			{
				StartCoroutine(DisplayNote(cantuse, "Rotate Left Artifact"));
			}
		}
		if(twoP && !environmentRef.rotating)
		{
			if(_rotR > 0)
			{
				// set rotate direction
				environmentRef.direction = Vector3.back;
				// rotate
				environmentRef.CallRotate();
				// take away a use
				rotR -= 1;
			}
			else if(_rotR <= 0)
			{
				StartCoroutine(DisplayNote (needmore, "Rotate Right Artifacts"));
			}
			else
			{
				StartCoroutine(DisplayNote(cantuse, "Rotate Right Artifact"));
			}
		}
		if(threeP)
		{
			// if gravity isnt inversed alraedy
			if(inverseGravity == false)
			{
				// and have a gravity item to use
				if(_gravRev > 0)
				{
					// Set inverse gravity to true
					inverseGravity = true;
					// take away a use
					gravRev -= 1;
				}
				else
				{
					StartCoroutine(DisplayNote (needmore, "Reverse Gravity Artifacts"));
				}
			}
			// otherwise (its already inverse)
			else
			{
				// set it to false
				inverseGravity = false;
			}

		}
		// if four pressed
		if(fourP)
		{
			// and have more than 0 health kits
			if(_hkit > 0)
			{
				// if have less than max health
				if(PlayerValues.instance.health < PlayerValues.instance.maxHealth)
				{
					// heal without a reference since its on same game object
					PlayerValues.instance.health += 25;
					// take away a use
					hkit -= 1;
				}
				// or if have full health
				else if(PlayerValues.instance.health >= PlayerValues.instance.maxHealth)
				{
					// notify that you cant use
					StartCoroutine(DisplayNote(cantuse, "Health Kit"));
				}
			}
			// otherwise, dont have health kits to use
			else
			{
				// notify
				StartCoroutine(DisplayNote(needmore, "Health Kits"));
			}
		}


		// if inverse gravity is true
		if(inverseGravity == true)
		{
			// reverse gravity for player
			rb.AddForce(Physics.gravity * rb.mass*-3);
		}
	}


	IEnumerator DisplayNote(string msg, string item)
	{
		noteText.text = msg + " " + item;

		yield return new WaitForSeconds(3f);

		noteText.text = "";
	}


	/// <summary>
	/// Getter/Setter for quantity of each item
	/// </summary>
	public int rotL
	{
		get
		{
			// give the value of var when asked for it
			return _rotL;
		}
		set
		{
			// setting the value, for when wanting to take away/give that doesnt follow conventional use (Update)
			_rotL = value;
			// if amount is less than 0, negatives, just in case
			if(_rotL < 0)
			{
				// make it 0 instead
				_rotL = 0;
			}
		}
	}

	public int rotR
	{
		get
		{
			return _rotR;
		}
		set
		{
			_rotR = value;
			if(_rotR < 0)
			{
				_rotR = 0;
			}
		}
	}

	public int gravRev
	{
		get
		{
			return _gravRev;
		}
		set
		{
			_gravRev = value;
			if(_gravRev < 0)
			{
				_gravRev = 0;
			}
		}
	}

	public int hkit
	{
		get
		{
			return _hkit;
		}
		set
		{
			_hkit = value;
			if(_hkit < 0)
			{
				_hkit = 0;
			}
		}
	}
}
