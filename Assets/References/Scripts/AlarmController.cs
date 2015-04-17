using UnityEngine;
using System.Collections;

public class AlarmController : MonoBehaviour 
{
	public float lightHighIntensity = 0.25f;
	public float lightLowIntensity = 0f;
	public float fadeSpeed = 7f;
	
	private bool alarmActive = true;
	private AlarmLight alarm;
	private Light mainLight;

	void Awake()
	{
		alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
		mainLight = GameObject.FindGameObjectWithTag(Tags.mainLight).light;
	}
	
	void Update()
	{
		SwitchAlarms();
	}
	
	void SwitchAlarms()
	{
		float newIntensity;
		if (alarmActive)
		{
			newIntensity = lightLowIntensity;
		}
		else
		{
			newIntensity = lightHighIntensity;
		}
		
		// Provide smooth transition between two intensities for the light
		Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);
		
	}
}
