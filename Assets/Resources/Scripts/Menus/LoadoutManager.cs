using UnityEngine;
using System.Collections;

public class LoadoutManager : MonoBehaviour {
	
	public InventoryPanel inventory;
	//public InventoryPanel roster;
	public Carousel roster;
	
	GUITexture btnAccept;
	
	
	void OnEnable() {		
		if (roster != null && GameObject.Find ("BtnNext") != null) {
			btnAccept = GameObject.Find ("BtnNext").transform.guiTexture;
			btnAccept.enabled = false;
		}
		if (inventory != null) {
			Gesture.onTouchUpE += CheckHighlights;
		}
	}
	
	void OnDisable() {
		if (inventory != null) {
			SavedData.ItemLoadout = GetDataString(inventory.SelectedItems);
			SavedData.Inventory   = GetDataString(inventory.Items);
			
			Gesture.onTouchUpE -= CheckHighlights;
		}
		
		if (roster != null) {
			if (roster.SelectedItem.name.Substring(roster.SelectedItem.name.Length-6, 6) != "Locked") {
				SavedData.CharacterLoadout = roster.SelectedItem.name;
			}
		}
		
		PowerupSpawner.loadoutChange = true;
		
		Gesture.onTouchUpE -= CheckHighlights;
	}
		
	void Update() {		
		if (roster != null && btnAccept != null) {
			
			if (roster.SelectedItem.name.Substring(roster.SelectedItem.name.Length-6, 6) != "Locked") {
				btnAccept.enabled = true;
			}
			else btnAccept.enabled = false;
//			btnAccept.enabled = false;
//			
//			foreach (Transform t in roster.SelectedItems) {
//				if (t != null) {
//					btnAccept.enabled = true;
//					break;
//				}
//			}
		}
	}
	
	void CheckHighlights(Vector2 touchPos) {
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit[] hits = Physics.RaycastAll(ray, 1000.0f);
		
		HighlightableItem hitem;
		bool selectable = false;
		foreach (RaycastHit hit in hits) {
			if (hit.transform.GetComponent<InventoryItem>() != null) {
				selectable = true;
				break;
			}
		}
		
		if (selectable) {
			foreach (RaycastHit hit in hits) {
				hitem = hit.transform.GetComponent<HighlightableItem>();
				if (hitem != null) hitem.ToggleSelected();
			}
		}
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
