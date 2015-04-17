using UnityEngine;
using System.Collections;

public class OtherScene : MonoBehaviour {

	private SingletonExample singleton; 
	// Use this for initialization
	void Start () {
		singleton = SingletonExample.instance; 
		Debug.Log(singleton.health); // calling health attribute get on our singleton
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
