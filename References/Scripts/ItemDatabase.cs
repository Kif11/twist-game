using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	public List<Item> Items= new List<Item> ();

	// Use this for initialization
	void Awake(){
				//add a new item to our database using the class constructor
		
				//int _ID, string _itemName, string _itemDescription, itemType _thisItemType
				Items.Add (new Item (1, "Key", "Wow, this probably triggers something", Item.itemType.key));
				Items.Add (new Item (2, "Other Key", "Not the first key", Item.itemType.key));
				Items.Add (new Item (3, "Bonussss", "congrats bruh-chan", Item.itemType.other));
		}

}
