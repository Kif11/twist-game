using UnityEngine;
using System.Collections;
// Kirill Kovalevskiy
// Script controlling environment behavior

public class Environment : MonoBehaviour 
{

	public GameObject level;
	public bool inverseGravity = false;

	private GameObject player;
	public Vector3 direction;

	// Angle of to rotate level to
	private int angle = 90;

	// Variable to hold rotation state
	// true mean that level is currently rotating
	public bool rotating = false;
		
	void Start () 
	{
		level = GameObject.FindGameObjectWithTag ("Environment");
		player = GameObject.FindGameObjectWithTag ("Player");

	}

	void Update () 
	{

        /*
         * 
         * NOTE(kirill): Since new inventory system has been implemented this
         * functionality is disabled. One might use it for debuging
         * level rotation
         * 
		// If key is pressed run and level is 
		// not currently rotating run Rotate couroutine 
		if(Input.GetButtonDown ("RotateLevelLeft") && !rotating)
		{
			direction = Vector3.forward;
			StartCoroutine (Rotate(direction));
		}
		if(Input.GetButtonDown ("RotateLevelRight") && !rotating)
		{
			direction = Vector3.back;
			StartCoroutine (Rotate(direction));
		}
        */

		if(inverseGravity)
		{
			Physics.gravity = new Vector3(0, 10, 0);
		}
		else
		{
			Physics.gravity = new Vector3(0, -10, 0);
		}
	}

	// Mainly used for calling Rotate Coroutine from other components
	public void CallRotate()
	{
		if(!rotating)
		{
			StartCoroutine (Rotate(direction));
		}
	}

	// Rotate object to an angle
	public IEnumerator Rotate (Vector3 direction)
	{
		rotating = true;

		Vector3 playerPosition = player.transform.position;

		for(int i = 0; i < angle; i++)
		{
			//Debug.Log("Rotating " + i);
			transform.RotateAround(playerPosition, direction, 1);
			yield return null;
		}	

		// After couroutine is finished
		// Set rotating to false
		rotating = false;
	}
}
