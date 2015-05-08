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

	// game manager instance reference
	private GameManager gm;

	// camera reference
	private CameraMove2 cam;

	// rotate speed of collectibles
	[SerializeField]
	private float speed = 10f;

	void Start()
	{
		gm = GameManager.GMinstance;
		cam = CameraMove2.camInstance;
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
				// remove item from scene
				Destroy (this.gameObject);
				break;
				// if item is rotate Left
			case itemType.rotL:
				other.GetComponent<SimpleInventory>().rotL += 1;
				Destroy (this.gameObject);
				break;
				// if item is rotate right
			case itemType.rotR:
				other.GetComponent<SimpleInventory>().rotR += 1;
				Destroy (this.gameObject);
				break;
				// if item is reverse gravity
			case itemType.gravRev:
				other.GetComponent <SimpleInventory>().gravRev += 1;
				Destroy (this.gameObject);
				break;
				// if item was goal
			case itemType.goal:
				// reset most of player's inventory when beating a level
				other.GetComponent<SimpleInventory>().ResetInventory();
				// make sure gravity is normal for them when they move on
				other.GetComponent<SimpleInventory>().inverseGravity = false;
				// run new level function on camera
				cam.NewLevelStuff();
				// load level
				gm.LoadNextLevel();
				// destroy object for good measure
				Destroy (this.gameObject);
				break;
			}

		}
	}

	void Update()
	{
		// Rotating collectibles so it stands out more
		transform.Rotate(new Vector3(30, 60, 90), speed * Time.deltaTime);
	}
}
