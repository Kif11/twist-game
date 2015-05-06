using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	Text helpText;


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

		helpTextObject = GameObject.FindGameObjectWithTag("HelpText");
		helpText = helpTextObject.GetComponent<Text>();

		// Create a list for help messages
		string[] helpMsgs = new string[6];
		
		helpMsgs[0] = "Welcome to Twisted World!";
		helpMsgs[1] = "Your goal is to find the exit from this labyrinth";
		helpMsgs[2] = "Use W, A, S, D to move around";
		helpMsgs[3] = "You can rotate level and reverse the gravity by using artifacts that you collected";
		helpMsgs[4] = "Try to explore different gravity anomalies to get where you want";
		helpMsgs[5] = "Go ahead and explore!";


		StartCoroutine("ShowHelpMsg", helpMsgs);
	}

	void Update()
	{
		timerText.text = "Time: " + Time.time;
	}

	IEnumerator ShowHelpMsg (string[] helpMsgs)
	{
		// Itterate trough all messages in the list
		// and display them with given time interval
		foreach (string item in helpMsgs)
		{
			helpText.text = item;
			yield return new WaitForSeconds(5f);
		}

	}
}
