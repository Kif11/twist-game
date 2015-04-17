using UnityEngine;
using System.Collections;

/// <summary>
/// Door Controller.
/// Moves doors around the stage
/// </summary>
public class DoorCS : MonoBehaviour {
	private static DoorCS _instance;
	private float 	doorSpeed 	= 15;
	


	void Awake () {

		if(_instance == null)
		{
			_instance = this;
		}
	}
	

	void Update () {

	}

	/// <summary>
	/// Gets the instance of this Door.
	/// </summary>
	/// <value>The instance of the door.</value>
	public static DoorCS instance{
		get{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<DoorCS>();
			}
			return _instance;
		}
	}

	/// <summary>
	/// Moves the door(s) to be open. Called in CharacterController
	/// </summary>
	public void OpenDoor(string doorname){

		GameObject DoorToOpen = GameObject.Find (doorname);
		Debug.Log ("Open Door " + doorname);
		for(int openTime = 0; openTime <= 10; openTime++){
		DoorToOpen.transform.Translate (Vector3.right * Time.deltaTime * doorSpeed);
		}
	}
}
