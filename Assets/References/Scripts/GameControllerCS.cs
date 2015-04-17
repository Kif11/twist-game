using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Game controller
/// Contains Singletons like health, bonuses collected and the statistics
/// </summary>
public class GameControllerCS : MonoBehaviour {
	// Use this for initialization
	private static GameControllerCS _instance;
	private int _health = 100;
	private int _bonuses = 0;
	private int[] _PlayerStatistics = new int[2]; //Contains the data for health and bonuses found


	/// <summary>
	/// The players health
	/// </summary>
	public int playerHealth
	{
		//Get{} and Set{} Health Value 
		get
		{
			return _health;
		}
		
		set
		{
			_health = value;
			if( _health <= 0)
			{
				_health = 0;
				
				PlayerDead();
			}

		}
	}

	/// <summary>
	/// The Player Staticstics such as items collected
	/// </summary>
	public int[] PlayerStatistics{
		//Gets{} set{} item amounts
		get{
			return _PlayerStatistics;
		}
		set{
			_PlayerStatistics[0] = _health;
			_PlayerStatistics[1] = _bonuses;
		}
	}

	/// <summary>
	/// Gets the instance for GameControllerCS.
	/// </summary>
	/// <value>The instance of GameController.</value>
	public static GameControllerCS instance{
		get{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameControllerCS>();
				//DontDestroyOnLoad(instance.gameObject);
			}
			return _instance;
		}
	}

	void awake(){
		if(_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(_instance.gameObject);
		}
		else{
			if(this!= _instance)
				Destroy(this.gameObject);
		}
	}


	/// <summary>
	/// Load the nexts scene.
	/// </summary>
	/// <returns>The scene.</returns>
	/// <param name="name">Name.</param>
	/// <param name="waitTime">Wait time.</param>
	public IEnumerator LoadNextScene (string name, float waitTime)
	{
		Debug.Log("Loading next scene...");
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(name);
	}
	
	void PlayerDead()
	{
		// Loads statistic level upon player death
		Application.LoadLevel(1);
	}
}
