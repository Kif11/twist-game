using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Young Chu
// Script handling numerical values and booleans regarding player

// Keeps the gameobject from being destroyed on load
public class PlayerValues : MonoBehaviour 
{
	// instance of this component for easy access
	private static PlayerValues _instance;

	// reference to Game Manager
	private GameManager gm;

	// maximum health player can have
	[SerializeField]
	private float _maxHealth = 100f;

	// current health value of player
	[SerializeField]
	private float _health = 100f;

	// Bool checking if player is teleporting or not
	public bool isTeleporting = false;

	// Restart Button boolean
	private bool restartPressed = false;	


	// Things to do when resetting/putting this component on something
	void Reset()
	{
		_health = 100f;
	}

	// setting instance var and making sure this object is carried over each scene
	void Awake ()
	{
		if(_instance == null)
		{
			_instance = this;
			// Stops this object from being destroyed when loading scenes
			DontDestroyOnLoad(_instance.gameObject);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}

	void Start()
	{
		// assign Game Manager instance to ref var
		gm = GameManager.GMinstance;
	}

	void Update()
	{
		// assigning bool a keypress to respond to
		restartPressed = Input.GetKeyDown (KeyCode.R);

		// if R is pressed
		if(restartPressed)
		{
			// call ReloadLevel function from GameManager component
			gm.ReloadLevel();
			// reset player position
			this.transform.position = new Vector3(0, 5, 0);
			// Run restart function to reset everything that should be resetted
			LevelRestarted();
		}
	}

	// Function that resets all values that should be resetted on player when restarting level
	public void LevelRestarted()
	{
		// resetting renderer back to visible
		renderer.enabled = true;

		// resetting speed back to normal
		this.GetComponent<PlayerMovement>().speed = 6;

		// resetting health
		_health = 100;

		// Resetting inventory
		this.GetComponent<SimpleInventory>().ResetInventory();
		this.GetComponent<SimpleInventory>().hkit = 0;
	}

	// A just in case for when this instance is actually called to make sure that there is something assigned to instance var
	public static PlayerValues instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PlayerValues>();
			}
			
			return _instance;
		}
	}


	/// <summary>
	/// Getter/Setter for _health variable
	/// </summary>
	public float health
	{
		get
		{
			return _health;
		}
		set
		{
			_health = value;
			
			/// checking if current health is lower than 0 (dead)
			if(_health <= 0)
			{
				_health = 0;
				// emulating destroying object, without the errors
				// calls on player's movement speed to 0, so cant move
				this.GetComponent<PlayerMovement>().speed = 0;
				// turn off renderer
				renderer.enabled = false;
			}
			// checking if current health value exceeds max health value
			if(_health >= _maxHealth)
			{
				// if yes, set it to be just maxHealth value then
				_health = _maxHealth;
			}
		}
	}

	public float maxHealth
	{
		get
		{
			return _maxHealth;
		}
		set
		{
			_maxHealth = value;
		}
	}
}




