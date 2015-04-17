using UnityEngine;
using System.Collections;

public class BulletCS : MonoBehaviour {


	private float				speed			= 55f;	
	private GameObject 			targetObject	= null;
	private Transform 			target			= null;	
	
	void Start() {
		transform.localScale += new Vector3(-0.75f, -0.75f, -0.75f);

		if(targetObject == null || targetObject != null)
			targetObject = GameObject.Find("Player");
		target = targetObject.transform;

		transform.LookAt (target);
	}

	void Update () {
		StartCoroutine(Moving());
	}


	/// <summary>
	/// Moves this instance of bullet.
	/// </summary>
	IEnumerator Moving(){

		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		StartCoroutine ("Die");
		yield return null;

	}

	/// <summary>
	/// Destroys after 2 seconds
	/// </summary>
	IEnumerator Die()
	{
		yield return new WaitForSeconds (2f);
		Destroy (this.gameObject);
	}
}
