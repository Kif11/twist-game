using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour 
{
	public float fadeSpeed = 0.1f;
	public float highIntensity = 2f;
	public float lowIntensity = 0.5f;
	public float changeMargin = 0.2f;
	public bool alarmOn;
	
	private float targetIntensity;
	
	void Awake()
	{
		light.intensity = 0f;
		targetIntensity = highIntensity;
		
		alarmOn = true;
	}
	
	void Update()
	{
		if (alarmOn)
		{
			// light.intensity = Mathf.Lerp(light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
				
			StartCoroutine(BlinkLight());
			// Debug.Log(light.intensity);
			// CheckTargetIntensity();
		}
//		else
//		{
//			light.intensity = Mathf.Lerp(light.intensity, 0f, fadeSpeed * Time.deltaTime);
//		}
	}
	
	IEnumerator BlinkLight()
	{
		light.intensity = Mathf.Lerp(light.intensity, targetIntensity, fadeSpeed * Time.time);
		Debug.Log ("Before wait");
		yield return new WaitForSeconds (3);
		light.intensity = Mathf.Lerp(targetIntensity, lowIntensity, fadeSpeed * Time.time);
		Debug.Log ("After wait");
	}
	
	void CheckTargetIntensity()
	{
		// Debug.Log (Mathf.Abs (targetIntensity - light.intensity));
		if (Mathf.Abs (targetIntensity - light.intensity) < changeMargin)
		{
			if (targetIntensity == light.intensity)
			{
				targetIntensity = lowIntensity;
			}
			else
			{
				targetIntensity = highIntensity;
			}
		}
	}
}
