using UnityEngine;
using System.Collections;
// Matthew K. Greene
// Singleton example
// 2/17/2015

public class SingletonExample : MonoBehaviour {

	private static SingletonExample _instance; 
	// protected values
	[SerializeField]
	private int _health = 10; 
	
	// Get and set properties
	// This removes the need for us to check current health whenever anything affects is
	// We know if we ever go over 100 it will be clamped or less than O
	public int health
	{
		// ????? return value
		get{
			return _health; 
		}
		set{
			_health = value; 
			if(_health >= 100)
			{
				_health = 100; 
			}
			else if(_health <= 0)
			{
				_health = 0;
				// Initiate death
				// restart the scene etc etc 
			}
		}
	
	}
	
	public static SingletonExample instance
	{
		get{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SingletonExample>(); 
				// We don't destroy this in the next scene
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance; 
		}
	}
	void Awake()
	{
		// first found instance
		if(_instance == null)
		{
			_instance = this; 
			DontDestroyOnLoad(_instance.gameObject); 
		}
		else{
			// Destroy the rest
			if(this != _instance) // single line condition does not need {}; 
			Destroy(this.gameObject); 
		}
	}
	public IEnumerator LoadNextScene(string name, float waitTime) // function parameters...name of scene, how long until we load
	{
		yield return new WaitForSeconds(waitTime); 
		Application.LoadLevel(name); 
	}
}
