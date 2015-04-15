using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	//Here is a private reference only this class can access
	private static GameController _instance;
	
	//This is the public reference that other classes will use
	public static GameController instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<GameController>();
			return _instance;
		}
	}

	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	
	public void switchLevel(string name)
	{
		Application.LoadLevel(name);
	}
}