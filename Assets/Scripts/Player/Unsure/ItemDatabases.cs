using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ^importing libraries


// no MonoBheaviour, no attaching this scrip to a scene object
public class ItemDatabases : MonoBehaviour 
{
	// Creating a list that is filld with our item class
	public List<Items> items = new List<Items>();

	// Do this during initialization
	void Awake()
	{
		// Add a new item to our database using the class constructor
		// public Item(int _ID, string _itemName, string _itemDescription, itemType _thisItemType)
		// ID is the #identifier stupid.....
		items.Add(new Items(1, "Sword of Amazing", "It is amazing", Items.itemType.weapon));
		items.Add(new Items(2, "Potion", "Drink it", Items.itemType.consumable));
		items.Add (new Items (3, "Infinity", "Surprisingly finite", Items.itemType.quest));
	}
}
