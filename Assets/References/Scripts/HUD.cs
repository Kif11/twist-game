
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// HUD for game
/// put on main camera.
/// </summary>
public class HUD: MonoBehaviour
{
	public Text 	health;
	public	Text	StatisticsText;
	// Use this for initialization
	
	
	
	void Start () {
		health.text = "Health: "+ GameControllerCS.instance.playerHealth;
		StatisticsText.text = "Collected "+ GameControllerCS.instance.PlayerStatistics[1]+" bonus";
	}

	// Update is called once per frame
	void Update () {
		health.text = "Health: "+ GameControllerCS.instance.playerHealth;
		StatisticsText.text = "Collected "+ GameControllerCS.instance.PlayerStatistics[1]+" bonus";
	}
}

