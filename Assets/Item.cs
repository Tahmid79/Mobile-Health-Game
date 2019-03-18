using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class Item {

	public string questionStatement;
	public int trueSelectionIndex;

	public int id, count;
	public string name, type, description;


	public Item(string itemName, string itemType, string itemDescription, int itemCount){

		name = itemName;
		type = itemType;
		description = itemDescription;
		count = itemCount;
		}

	

}
