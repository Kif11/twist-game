using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// Young Chu

// Let's make sure whatever we put our inventory script onto has the item database script
[RequireComponent(typeof(ItemDatabases))]

public class InventoryCS : MonoBehaviour 
{

	//SerializeField to see private variables in inspector
	[SerializeField]
	private List<Items> 		inventory = new List<Items>();
	private ItemDatabases	itemDB;
	//Maybe we can change this when we get a bigger bag
	private int maxInventory = 4;

	void Start () 
	{
		// Link to our databases
		itemDB = GetComponent<ItemDatabases> ();

		//Loop based on max inventory and populate array with empty items
		for (int i = 0; i < maxInventory; i++) 
		{
			inventory.Add (new Items());
		}

		AddItem(2);
		AddItem (1);
		AddItem (3);
		AddItem (3);
		RemoveItem (3);
		if(CheckForItem (3))
		{
			Debug.Log("Player has key");
			//open door and remove key
			RemoveItem (3);
		}
		AddItem (1);
		AddItem (1);
	}


	// HOMEWORK: Add a function that swaps 2 item slots
	// Integrate this into platformer
	// Add item with paramter for item id
	public void AddItem(int id)
	{
		//bool itemAdded = false;
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

	public void RemoveItem(int id)
	{
		for(int i = 0; i < maxInventory; i++)
		{
			if(inventory[i].ID == id)
			{
				inventory[i] = new Items();
				break;
			}
		}
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


















