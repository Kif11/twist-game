using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// Kirill Kovalevskiy

public class GamePlay : MonoBehaviour {

	GameManager gm;
	Text difLevel;
	
	// Use this for initialization
	void Awake () 
	{
		gm = GetComponentInParent<GameManager>();
		difLevel = transform.FindChild("Canvas/Difficulty").GetComponent<Text>();
	}
	
	// This run when the object is turned on
	void OnEnable () 
	{
	
		difLevel.text = "Difficulty: " + gm.difficultyLevel;
	
	}
}
