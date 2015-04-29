using UnityEngine;
using System.Collections;
// Young Chu
// Simple NOT Dynamic inventory
// This script assumes each button press/item slot is dedicated to only one item forever
// eg. 1 is always Health Potion, 2 is always Mana Potion, 3 is always etc..

public class SimpleInventory : MonoBehaviour 
{
	//Simple inventory

	/// <summary>
	/// Integers containing amount of each item
	/// rotL = Rotate Left artifact
	/// rotR = rotate right artifact
	/// hkit = health kit
	/// </summary>
	[SerializeField]
	private int _rotL = 0;
	[SerializeField]
	private int _rotR = 0;
	[SerializeField]
	private int _hkit = 0;

	/// <summary>
	/// Booleans for button presses of 1/2/3
	/// 1 = rotL
	/// 2 = rotR
	/// 3 = hkit
	/// </summary>
	private bool oneP;
	private bool twoP;
	private bool threeP;

	/// <summary>
	/// Environment component reference
	/// Environment tagged object is the parent of all things in the level when they rotate
	/// </summary>
	private Environment environmentRef;


	void Start()
	{
		// running function to find reference to Environment
		SetEnvironment();
	}


	// Find object tagged Environment and gets component 'Environment' component and assigns to var
	// Have this function run whenever new scene is loaded, so it'll get new Environment
	void SetEnvironment()
	{
		environmentRef = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}


	void Update()
	{
		// assigns key presses to the bools
		oneP = Input.GetKeyDown(KeyCode.Alpha1);
		twoP = Input.GetKeyDown(KeyCode.Alpha2);
		threeP = Input.GetKeyDown(KeyCode.Alpha3);

		// If oneP is pressed
		if(oneP)
		{
			// and if Rotate Left uses is more than 0
			if(_rotL > 0 && !environmentRef.rotating)
			{
				//rotate level left
				environmentRef.direction = Vector3.forward;
				environmentRef.CallRotate ();
				Debug.Log ("Used Rotate Left Artifact");
				// Taking away a use, use the get/set function to be extra defensive
				rotL -= 1;
			}
			// otherwise (if !> 0 uses)
			else
			{
				Debug.Log ("No Rotate Left Artifact in inventory right now!");
				// display text on screen saying dont have any
			}
		}
		if(twoP && !environmentRef.rotating)
		{
			if(_rotR > 0)
			{
				// rotate right
				environmentRef.direction = Vector3.back;
				environmentRef.CallRotate();
				Debug.Log ("Used Rotate Right Artifact");
				// take away a use
				rotL -= 1;
			}
			else
			{
				Debug.Log ("No Rotate Right Artifact in inventory right now!");
				// display text
			}
		}
		if(threeP)
		{
			if(_hkit > 0)
			{
				// heal without a reference since its on same game object
				PlayerValues.instance.health += 25;
				Debug.Log ("Used Health Kit");
				// take away a use
				hkit -= 1;
			}
			else
			{
				Debug.Log ("No Health Kit in inventory right now!");
				// display
			}
		}
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
			// if amount is less than 0, negatives
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
