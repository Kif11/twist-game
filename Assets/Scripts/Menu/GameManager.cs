using UnityEngine;
using System.Collections;
// Kirill Kovalevskiy
// Young Chu
// Handles transitioning of scenes

public class GameManager : MonoBehaviour 
{
	// instance of this component
	private static GameManager _GMinstance;

	// The variable that will store the current scene number, 0 should be Main Menu
	[SerializeField]
	[Tooltip("Meant to store current scene number")]
	private int _sceneNumber = 0;

	// Number of last scene in game, last scene should be game over scene
	[SerializeField]
	[Tooltip("Number of final scene")]
	private int _lastScene = 8;

	// stores difficulty level
	public int difficultyLevel = 1;

	// return to main menu bool
	private bool pPressed;

	public int sceneNumber
	{
		get
		{
			return _sceneNumber;
		}
		set
		{
			_sceneNumber = value;
			if(_sceneNumber > lastScene)
			{
				_sceneNumber = lastScene;
			}
		}
	}
	public int lastScene
	{
		get
		{
			return _lastScene;
		}
		set
		{
			_lastScene = value;
		}
	}


	public void Awake()
	{
		// Calls function that dictates what menu shows
		EnableState ("Menu");

		// setting instance var and making sure this object is carried over each scene
		if(_GMinstance == null)
		{
			_GMinstance = this;
			// Stops this object from being destroyed when loading scenes
			DontDestroyOnLoad(_GMinstance.gameObject);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _GMinstance)
				Destroy(this.gameObject);
		}
	}

	// A just in case for when this instance is actually called to make sure that there is something assigned to instance var
	public static GameManager GMinstance
	{
		get
		{
			if(_GMinstance == null)
			{
				_GMinstance = GameObject.FindObjectOfType<GameManager>();
			}
			
			return _GMinstance;
		}
	}


	void Update()
	{
		pPressed = Input.GetKeyDown (KeyCode.P);

		if(pPressed)
		{
			BackToMenuScene();
		}
	}

	
	// Turn on and off our sates
	public void EnableState(string state)
	{
		// Since our child transform are already stored into an array we can loop through them
		foreach(Transform child in transform)
		{
			if (child.name == state)
			{
				child.gameObject.SetActive(true); // "turn on" the game object
			}
			else
			{
				child.gameObject.SetActive(false);
			}
		}
	}

	// load back to main menu screen
	public void BackToMenuScene()
	{
		Application.LoadLevel ("scene_0");
		// renable Menu UI
		EnableState("Menu");
		// reset current scene number
		_sceneNumber = 0;
		// destroy game UI to avoid certain errors
		Destroy(GameObject.FindGameObjectWithTag("GameUI"));
	}

	// Loads next level
	public void LoadNextLevel()
	{
		// basic name format for scenes
		string scene = "scene_";

		// adds one to current scene number
		sceneNumber++;

		// if current scene is greater than (somehow) or equal to the last scene number
		if(sceneNumber >= lastScene)
		{
			// reference to player
			GameObject playRef = GameObject.FindGameObjectWithTag("Player");

			// Destroy player
			Destroy(playRef.gameObject);

			// Show end game menu
			EnableState("End");
		}
		else
		{
			// Have no menu appear
			EnableState("Nothing");
		}

		// give scene new value
		scene = scene + sceneNumber.ToString ();

		// Load new scene based on var's string value
		Application.LoadLevel(scene);
	}

	// reloads level
	public void ReloadLevel()
	{
		string scene = "scene_";

		scene = scene + sceneNumber.ToString ();
		Application.LoadLevel(scene);
	}
	
}
