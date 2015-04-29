using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	GameManager gm;
	Slider difficultySlider;

	// Use this for initialization
	void Awake () 
	{
	
		gm = GetComponentInParent<GameManager>();
		difficultySlider = transform.FindChild("Canvas/Difficulty").GetComponent<Slider>();
	
	}
	
	// Very important
	// This function is called when a game object is disabled
	void OnDisable()
	{
		gm.difficultyLevel = (int)difficultySlider.value;
	}

}
