using UnityEngine;
using System.Collections;

public class LoadoutManager : MonoBehaviour {
	
	public InventoryPanel inventory;
	public InventoryPanel roster;
	
	GUITexture btnAccept;
	
	
	void OnEnable() {
		if (roster != null) {
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
			SavedData.CharacterLoadout	 = GetDataString(roster.SelectedItems);
		}
		
		PowerupSpawner.loadoutChange = true;
		
		Gesture.onTouchUpE -= CheckHighlights;
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
	
	void CheckHighlights(Vector2 touchPos) {
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit[] hits = Physics.RaycastAll(ray, 1000.0f);
		
		foreach (RaycastHit hit in hits) {
			HighlightableItem hitem = hit.transform.GetComponent<HighlightableItem>();
			if (hitem != null) {
				hitem.ToggleSelected();
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
