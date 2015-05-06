using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// Kirill Kovalevskiy

public class GamePlay : MonoBehaviour {

	GameObject gameManagerObject;
	GameObject difLevelTextObject;
	GameObject timerObject;
	GameObject helpTextObject;

	GameManager gameManager;

	Text difText;
	Text timerText;
	
	// Use this for initialization
	void Start () 
	{
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();

		difLevelTextObject = GameObject.FindGameObjectWithTag("DifficultyText");
		difText = difLevelTextObject.GetComponent<Text>();
		difText.text = "Difficulty: " + gameManager.difficultyLevel;

		timerObject = GameObject.FindGameObjectWithTag("TimerText");
		timerText = timerObject.GetComponent<Text>();
	}

	void Update()
	{
		timerText.text = "Time: " + Time.time;
	}
}
