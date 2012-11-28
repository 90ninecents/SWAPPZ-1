using UnityEngine;
using System.Collections;

public class LoadoutManager : MonoBehaviour {
	public Transform itemLoadoutPanel;
	public Transform inventoryPanel;
	public Transform characterLoadoutPanel;
	public Transform rosterPanel;
	
	InventoryPanel itemLoadout;
	InventoryPanel characterLoadout;
	InventoryPanel inventory;
	InventoryPanel roster;
	
	void OnEnable() {
		if (itemLoadoutPanel != null) {
			itemLoadout = itemLoadoutPanel.GetComponent<InventoryPanel>();
			inventory = inventoryPanel.GetComponent<InventoryPanel>();
		}
		if (characterLoadoutPanel != null) {
			characterLoadout = characterLoadoutPanel.GetComponent<InventoryPanel>();
			roster = rosterPanel.GetComponent<InventoryPanel>();
		}
	}
	
	void OnDisable() {
		if (itemLoadout != null) 		SavedData.ItemLoadout 		 = GetDataString(itemLoadout.Items);
		if (inventory != null) 			SavedData.Inventory 		 = GetDataString(inventory.Items);
		if (characterLoadout != null) 	SavedData.CharacterLoadout	 = GetDataString(characterLoadout.Items);
		if (roster != null) 			SavedData.UnlockedCharacters = GetDataString(roster.Items);
		
		PowerupSpawner.loadoutChange = true;
	}
	
	string GetDataString(Transform[] itemList) {
		string items ="";
		foreach (Transform t in itemList) {
			if (t != null) {
				if (items != "") items += SavedData.Separator;
				// store item's name without (Clone) at the end
				items += t.name.Substring(0, t.name.Length-7);
			}
		}
		
		return items;
	}
}
