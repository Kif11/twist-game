using UnityEngine;
using System.Collections;
// Young Chu
// Simple and convenient way - but not perfect - to transition through levels sequentially


// Biggest downfall of this is that it only goes through a single
// naming format, in this case: "level_0" and just adds a number
// which you then load the level with the matching name; "level_01", "level_02", etc.


// To break it down, you'll have to name every scene with one base naming:
// level_00 = Main Menu
// level_01 = Level 1
// level_02 = Level 2, and so on

// Can be annoying for knowing what scene is specifically what unfortunately
// But this is just a simple and quick way I found

public class SimpleLevelTransition : MonoBehaviour {

	// The variable that will store the current scene number
	// Example variable (Debug one)
	[Tooltip("Debug variable that stores current scene number")]
	public int scenezNumber = 0; 	// 0 should be for first scene(main menu?)


	// Actual use example variable
	[Tooltip("Actual variable that stores current scene number")]
	public int sceneNumber = 0;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// this is just example function to test, press 1 and check console for debug logs and you'll see it works
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			// basic name format for scenes
			// actual value of string can be changed to whatever of course, "scene" for example,
			// just make sure scene files names are changed accordingly as well
			// the variable is declared in the function because we don't want to store
			// the basic naming format, otherwise we'll end up having scene = "level_012", "level_0123", etc.
			string scene = "level_0";

			// Add to current scene value
			scenezNumber++;

			// makes the string "level_0" = to "level_0" + the integer sceneNumber converted to a string (sceneNumber++)
			// so end values ends up being: scene = "level_0[#]"
			// Minus the []'s and # being the value of what sceneNumber equals after adding 1 to it.
			scene = scene + scenezNumber.ToString ();

			// This is where you would load the scene with the 'scene' variable as the paramter
			// or Call the switchLevel function with the 'scene' variable as a parameter

			// Debug just to show you it works
			Debug.Log (scene);
		}


		// Press 2 to actually see it happen, start at whatever scene the player is in,
		// as its currently attached to the player, either way, it'll load level_01 first
		// we'll obvious move these functions to the GameManager or something thats a constant THROUGHOUT the game
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			switchLevel ();
		}
	}


	// So it would basically look something like this:

	public void switchLevel()
	{
		string scene = "level_0";

		sceneNumber++;
		scene = scene + sceneNumber.ToString ();

		Application.LoadLevel(scene);
		Debug.Log ("loaded " + scene);

	}
// ================================================
	// or this
	// I have no idea why you would use this one instead of the first one
	// But maybe you find a reason to.

	public void SetNextSceneValue()
	{
		string scene = "level_0";
		
		sceneNumber++;
		scene = scene + sceneNumber.ToString ();

		switchLevelz (scene);
	}

	public void switchLevelz(string name)
	{
		Application.LoadLevel(name);
	}




}
