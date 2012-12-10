using UnityEngine;
using System.Collections;

public class LoadoutManager : MonoBehaviour {
	public Transform inventoryPanel;
	public Transform rosterPanel;
	
	InventoryPanel inventory;
	InventoryPanel roster;
	
	GUITexture btnAccept;
	
	void OnEnable() {
		if (inventoryPanel != null) {
			inventory = inventoryPanel.GetComponent<InventoryPanel>();
		}
		if (rosterPanel != null) {
			roster = rosterPanel.GetComponent<InventoryPanel>();
			
			btnAccept = GameObject.Find ("BtnNext").transform.guiTexture;
			btnAccept.enabled = false;
		}
	}
	
	void Update() {
		if (roster != null) {
			btnAccept.enabled = false;
			
			foreach (Transform t in roster.SelectedItems) {
				if (t != null) {
					btnAccept.enabled = true;
					break;
				}
			}
		}
	}
	
	void OnDisable() {
		if (inventory != null) {
			SavedData.ItemLoadout = GetDataString(inventory.SelectedItems);
			SavedData.Inventory   = GetDataString(inventory.Items);
		}
		
		if (roster != null) {
			SavedData.CharacterLoadout	 = GetDataString(roster.SelectedItems);
		}
		
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
