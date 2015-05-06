using UnityEngine;
using System.Collections;
// Kirill
// Young - revamped it

public class Collectable : MonoBehaviour {

	/// <summary>
	/// Create a drop down menu of all item types, so you can assign
	/// each collectible gameobject individually with a single component
	/// rotL = Rotate Left item
	/// rotR = Rotate Right item
	/// gravRev = Gravity Reversal item
	/// </summary>
	public enum itemType {health, rotL, rotR, gravRev, goal}
	public itemType item;

	private GameManager gm;


	void Start()
	{
		gm = GameManager.GMinstance;
	}

	void OnTriggerEnter(Collider other)
	{
		// Check if collectable collide with player
		if (other.tag == "Player") 
		{
			switch (item)
			{
				// if this item is health
			case itemType.health:
				// reference Player's inventory component and add 1 health kit
				other.GetComponent<SimpleInventory>().hkit += 1;
				// notification
				Debug.Log ("Health collected");
				// remove item from scene
				Destroy (this.gameObject);
				break;
			case itemType.rotL:
				other.GetComponent<SimpleInventory>().rotL += 1;
				Debug.Log ("Rotate Left Artifact collected");
				Destroy (this.gameObject);
				break;
			case itemType.rotR:
				other.GetComponent<SimpleInventory>().rotR += 1;
				Debug.Log ("Rotate Right Artifact collected");
				Destroy (this.gameObject);
				break;
			case itemType.gravRev:
				other.GetComponent <SimpleInventory>().gravRev += 1;
				Debug.Log ("Reverse Gravity collected");
				Destroy (this.gameObject);
				break;
			case itemType.goal:
				gm.LoadNextLevel();
				Debug.Log ("Loading New Level");
				Destroy (this.gameObject);
				break;
			}

		}
	}
}
