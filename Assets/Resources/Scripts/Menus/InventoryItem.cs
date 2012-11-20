using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour {
	// A 2D representation of an in-game item that can be added to the player's loadout
	
	public string itemName = "Item";
	public string description = "This is a generic item.";
	
	public GameObject itemPrefab = null;		// The in-game item that this item unlocks during gameplay
}
