using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ^importing libraries



public class ItemDatabase : MonoBehaviour 
{
	// Creating a list that is filld with our item class
	public List<Item> items = new List<Item>();

	// Do this during initialization
	void Awake()
	{
		// Add a new item to our database using the class constructor
		// public Item(int _ID, string _itemName, string _itemDescription, itemType _thisItemType)
		items.Add(new Item(1, "Left Rotation Artifact", "Rotates the level to the left", Item.itemType.artifact));
		items.Add(new Item(2, "Right Rotation Artifact", "Rotates the level to the right", Item.itemType.artifact));
		items.Add(new Item(3, "Health Kit", "Restores 25 Health", Item.itemType.consumable));
	}
}
