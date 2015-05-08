using UnityEngine;
using System.Collections;
// Kirill Kovalevskiy
// Handles how camera moves and sets player position

public class CameraMove2 : MonoBehaviour
{
	private static CameraMove2 _camInstance;	// Cam instance

	public float smooth = 1.5f;         	// The relative speed at which the camera will catch up.

	private Transform player;           	// Reference to the player's transform.
	private SimpleInventory inventoryRef; 	// Reference to simpleinventory component


	public static CameraMove2 camInstance
	{
		get
		{
			if(_camInstance == null)
			{
				_camInstance = GameObject.FindObjectOfType<CameraMove2>();
			}
			
			return _camInstance;
		}
	}

	
	void Awake ()
	{
		if(_camInstance == null)
		{
			_camInstance = this;
			// Stops this object from being destroyed when loading scenes
			DontDestroyOnLoad(_camInstance.gameObject);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _camInstance)
				Destroy(this.gameObject);
		}

		// Setting up the reference for player
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}


	// Run this when new level loaded
	public void NewLevelStuff()
	{
		// Setting player's position to a specific position everytime camera is loaded (every scene)
		// y axis is 5 so player doesn't get stuck in floor when downward force is applied
		player.position = new Vector3(0, 5, 0);
		
		// assigning reference to inventory script
		inventoryRef = player.GetComponent<SimpleInventory>();
		
		StartCoroutine(SetEnv());
	}

	IEnumerator SetEnv()
	{
		yield return new WaitForSeconds(0.5f);

		// run inventory's set environment function every scene
		// to reassign environment every level
		inventoryRef.SetEnvironment();
	}
}
