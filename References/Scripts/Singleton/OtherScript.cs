using UnityEngine;
using System.Collections;

public class OtherScript : MonoBehaviour {

	private SingletonExample singleton; 
	// Use this for initialization
	void Start () {
		singleton = SingletonExample.instance;
		singleton.health += 20;			// using health attribute "SET"
		Debug.Log(singleton.health); 	// using health attribute "GET"  
		StartCoroutine(singleton.LoadNextScene("OtherScene", 1.5f)); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
