using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{

	public void Awake()
	{
		ListChildren();
		EnableState ("Menu");

		DontDestroyOnLoad(this.gameObject);
		
	}

	public int difficultyLevel = 1;
	
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

	public void StartGame()
	{
		// Load first level
//		Debug.Log ("Loading level...");
		Application.LoadLevel("level_01");

		// Make menu inactive after transition to the first level
//		this.gameObject.SetActive (false);

		// Destroy extra menu elements that we no longer need
		foreach (Transform child in this.transform) 
		{
			Destroy(child.gameObject);
		}
	}
	
}
