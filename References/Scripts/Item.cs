using UnityEngine;
using System.Collections;

/// <summary>
/// Item creator
/// </summary>
public class Item{
	public int ID;
	public string itemName;
	public string itemDescription;
	public itemType thisItemType;

	public enum itemType{
		key,
		other
	}

	public Item(int _ID, string _itemName, string _itemDescription, itemType _thisItemType)
	{
		ID = _ID;
		itemName = _itemName;
		itemDescription = _itemDescription;
		thisItemType = _thisItemType;
		
	}

	public Item()
	{

	}
}
