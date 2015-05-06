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
	private int sceneNumber = 0;

	// stores difficulty level
	public int difficultyLevel = 1;


	public void Awake()
	{
		ListChildren();
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

	// Find out what child game object we have
	public void ListChildren()
	{
		// Show how many children we have (not embeded)
		int children = transform.childCount; 
		for(int i = 0; i < children; i++)
		{
			// Return name of the child
//			Debug.Log (transform.GetChild (i)); 
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

	// Loads next level
	public void LoadNextLevel()
	{
		// basic name format for scenes
		string scene = "scene_";

		// adds one to current scene number
		sceneNumber++;

		// give scene new value
		scene = scene + sceneNumber.ToString ();

		// Load new scene
		Application.LoadLevel(scene);
		Debug.Log ("loaded " + scene);

		// Destroy extra menu elements that we no longer need
		foreach (Transform child in this.transform) 
		{
			Destroy(child.gameObject);
		}
	}

	// reloads level
	public void ReloadLevel()
	{
		string scene = "scene_";

		scene = scene + sceneNumber.ToString ();
		Application.LoadLevel(scene);
		Debug.Log ("loaded " + scene);
	}
	
}
