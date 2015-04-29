using UnityEngine;
using System.Collections;
// Young Chu
// Script handling health value of player and Level transition

// Keeps the gameobject from being destroyed on load
public class PlayerValues : MonoBehaviour 
{
	// instance of this component for easy access
	private static PlayerValues _instance;

	// maximum health player can have
	[SerializeField]
	private float maxHealth = 100f;

	// current health value of player
	[SerializeField]
	private float _health = 100f;

	// Bool checking if player is teleporting or not
	public bool isTeleporting = false;

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
				// die/ game over
			}
			// checking if current health value exceeds max health value
			if(_health >= maxHealth)
			{
				// if yes, set it to be just maxHealth value then
				_health = maxHealth;
			}
		}
	}


	// loading next scene
	public IEnumerator LoadNextScene(string name, float waitTime) // function parameters...name of scene, how long until we load
	{
		yield return new WaitForSeconds(waitTime); 
		Application.LoadLevel(name); 
	}
}




