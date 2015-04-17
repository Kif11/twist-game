using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Inventory.
/// Only contains differnt keys for differnt doors
/// </summary>
[RequireComponent(typeof(ItemDatabase))]
public class InventoryCS : MonoBehaviour {
	[SerializeField]
	public List<Item>		inventory 	= new List<Item>();
	private ItemDatabase 	itemDB		= null;
	private int maxInventory = 3;
	
	// Use this for initialization
	

	void Awake () {
		itemDB = GetComponent<ItemDatabase> ();

		for (int i = 0; i <  maxInventory; i++) {
			inventory.Add(new Item());	
		}

		DontDestroyOnLoad(this.gameObject);
	}
	
	/// <summary>
	/// Adds items based.
	/// </summary>
	/// <param name="id">Identifier.</param>
	public void AddItem(int id)
	{
		
		//loop thru current inventory
		for(int i = 0; i < maxInventory; i++)
		{
			//Check for our first available inventory slot
			if(inventory[i].ID==0)	//we have found an empty slot
			{
				for(int j=0; j<itemDB.Items.Count; j++)
				{
					if(itemDB.Items[j].ID == id)
					{
						inventory[i] = itemDB.Items[j];
						Debug.Log(inventory[i].itemName+" item has been added");
						break;
					}
				}
				break;
			}
			else if(i>=(maxInventory-1)){
				Debug.Log("Inventory is currently full....");}
		}
	}
	
	public void removeItem(int id){
		for(int i= 0; i< maxInventory; i++)
		{
			if(inventory[i].ID==id)
			{
				inventory[i]=new Item();
				break;
			}
		}
	}
	
	public bool CheckForItem(int id)
	{
		bool hasItem =false;
		for(int i =0; i < maxInventory; i++)
		{
			if(inventory[i].ID==id)
			{
				hasItem=true;
				break;
			}
		}
		return hasItem;
	}

}
