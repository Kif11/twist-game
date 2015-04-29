using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// Young Chu

// Let's make sure whatever we put our inventory script onto has the item database script
[RequireComponent(typeof(ItemDatabase))]

public class Inventory : MonoBehaviour 
{

	//SerializeField to see private variables in inspector
	[SerializeField]
	private List<Item> 		inventory = new List<Item>();
	private ItemDatabase	itemDB;

	//Amount of items that can be held at once
	private int maxInventory = 3;

	// Booleans for keypresses of 1/2/3
	private bool onePressed;
	private bool twoPressed;
	private bool threePressed;

	// Environment component reference
	[SerializeField]
	private Environment environmentRef;
	//Player Values component reference
	private PlayerValues playerValRef;

	void Start () 
	{
		// Link to our databases
		itemDB = GetComponent<ItemDatabase> ();

		//Loop based on max inventory and populate array with empty items (slots)
		for (int i = 0; i < maxInventory; i++) 
		{
			inventory.Add (new Item());
		}

		// add Rotate Left & Right and health kit items
		AddItem(1);
		AddItem (2);
		AddItem (3);

		// running function to find reference to Environment
		SetEnvironment();
		playerValRef = GetComponent<PlayerValues>();
	}

	// Find object tagged Environment and gets component 'Environment' component and assigns to var
	void SetEnvironment()
	{
		environmentRef = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>();
	}


	void Update()
	{
		// Assigning bools to keycode presses, 1, 2, and 3
		onePressed = Input.GetKeyDown(KeyCode.Alpha1);
		twoPressed = Input.GetKeyDown(KeyCode.Alpha2);
		threePressed = Input.GetKeyDown(KeyCode.Alpha3);

		// running use item function when button pressed
		if(onePressed)
		{
			// uses slot 0 since thats where it begins
			UseItem (0);
		}
		if(twoPressed)
		{
			UseItem (1);
		}
		if(threePressed)
		{
			UseItem (2);
		}
	}


	/// <summary>
	/// Using items based on slot of inventory
	/// ID 1 = Rotate level left
	/// ID 2 = rotate level right
	/// ID 3 = health kit
	/// </summary>
	public void UseItem(int slot)
	{
		if(inventory[slot].ID == 0)
		{
			// display on screen, no item
			Debug.Log ("No item in slot " + slot);
		}
		else if(inventory[slot].ID == 1 && !environmentRef.rotating)
		{
			//rotate level left
			Debug.Log ("Rotate left");
			environmentRef.direction = Vector3.forward;
			environmentRef.CallRotate ();
			//remove item
			RemoveItemInSlot(slot);
		}
		else if(inventory[slot].ID == 2 && !environmentRef.rotating)
		{
			// rotate right
			Debug.Log ("Rotate right");
			environmentRef.direction = Vector3.back;
			environmentRef.CallRotate();
			//remove item
			RemoveItemInSlot(slot);
		}
		else if(inventory[slot].ID == 3)
		{
			// heal player
			Debug.Log ("Heal 25 HP");

			// remove item
			RemoveItemInSlot(slot);
		}
	}

	
	// Add item with paramter for item id
	public void AddItem(int id)
	{
		// Loop through our current inventory
		for(int i = 0; i < maxInventory; i++)
		{
			// Check for our first available inventory slot
			if(inventory[i].ID == 0) // we have found an empty slot
			{
				// use 'Count' instead of 'Length' for Lists
				for(int j = 0; j < itemDB.items.Count; j++)
				{
					if(itemDB.items[j].ID == id)
					{
						inventory[i] = itemDB.items[j];
						Debug.Log(inventory[i].itemName + " item has been added");
						//itemAdded = true;
						break; 
					}
				}
				break;
			}
			else if(i >= (maxInventory - 1))
			{
				Debug.Log ("Overencumbered");
			}
		}

	}

	public void RemoveSpecificItem(int id)
	{
		for(int i = 0; i < maxInventory; i++)
		{
			if(inventory[i].ID == id)
			{
				inventory[i] = new Item();
				break;
			}
		}
	}

	public void RemoveItemInSlot(int slot)
	{
		inventory[slot] = new Item();
	}

	public bool CheckForItem(int id)  // function with return type
	{
		bool hasItem = false;
		for(int i = 0; i < maxInventory; i++)
		{
			//if(inventory[i] != null)
			//{
				if(inventory[i].ID == id)
				{
					hasItem = true;
					break;
				}
			//}
		}
		return hasItem;
	}
}


















