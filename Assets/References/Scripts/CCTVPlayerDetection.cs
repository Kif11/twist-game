using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour 
{
	private GameObject player;
	private AlarmController lastPlayerSighting;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<AlarmController>();
	}
	
//	void OnTriggerStay (Collider other)
//	{
//		if (other.gameObject == player)
//		{
//			Debug.Log("Player hit camera field");
//			// raycast from the camera in order to resolve the problem where the player is behinde
//			// a wall but still intersectin with the camera collider
//			
//			// Direction of raycast variable
//			Vector3 relPlayerPos = player.transform.position - transform.position;
//			RaycastHit hit;
//			
//			if (Physics.Raycast(transform.position, relPlayerPos, out hit))
//			{
//				if (hit.collider.gameObject == player)
//				{
//					lastPlayerSighting.position = player.transform.position;
//				}
//			}
//		}
//	}

}
