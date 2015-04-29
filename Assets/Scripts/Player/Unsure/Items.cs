using UnityEngine;
using System.Collections;
// Young Chu
//


// Our item class is a template for item objects
// Doesnt need to inherit from MonoBehaviour
// Make this class readable in he inspector
[System.Serializable]
public class Items 
{
	public int ID;
	public string itemName;
	public string itemDescription;
	public itemType thisItemType;

	// enum list
	//research the hell is an "enum"
	public enum itemType{
		consumable,
		quest,
		weapon
	};


	// Class constructor
	// Create a class instance

	public Items(int _ID, string _itemName, string _itemDescription, itemType _thisItemType) // Parameters for creating a class instance
	{
		// Our class variables will equal the parameters we put in our constructor
		ID 				= _ID;
		itemName 		= _itemName;
		itemDescription = _itemDescription;
		thisItemType 	= _thisItemType;
	}

	// Create an empty constructor
	public Items()
	{

	}

}
