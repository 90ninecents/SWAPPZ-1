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
	
	bool dragging = false;
	
	void OnEnable() {
		Gesture.onDraggingE += OnDrag;
		Gesture.onDraggingEndE += OnDragEnd;
		Gesture.onTouchUpE += OnTouchUp;
		
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
		Gesture.onDraggingE -= OnDrag;
		Gesture.onDraggingEndE -= OnDragEnd;
		Gesture.onTouchUpE -= OnTouchUp;
		
		if (itemLoadout != null) {
			string items ="";
			foreach (Transform t in itemLoadout.Items) {
				if (t != null) {
					if (items != "") items += SavedData.Separator;
					//items += t.GetComponent<InventoryItem>().itemPrefab.name;
					// store item's name without (Clone) at the end
					items += t.name.Substring(0, t.name.Length-7);
				}
			}
			SavedData.ItemLoadout = items;
		}
		
		if (inventory != null) {
			string items ="";
			foreach (Transform t in inventory.Items) {
				if (t != null) {
					if (items != "") items += SavedData.Separator;
					//items += t.GetComponent<InventoryItem>().itemPrefab.name;
					// store item's name without (Clone) at the end
					items += t.name.Substring(0, t.name.Length-7);
				}
			}
			SavedData.Inventory = items;
		}
		
		if (characterLoadout != null) {
			string items ="";
			foreach (Transform t in characterLoadout.Items) {
				if (t != null) {
					if (items != "") items += SavedData.Separator;
					//items += t.GetComponent<InventoryItem>().itemPrefab.name;
					// store item's name without (Clone) at the end
					items += t.name.Substring(0, t.name.Length-7);
				}
			}
			SavedData.CharacterLoadout = items;
		}
		
		if (roster != null) {
			string items ="";
			foreach (Transform t in roster.Items) {
				if (t != null) {
					if (items != "") items += SavedData.Separator;
					//items += t.GetComponent<InventoryItem>().itemPrefab.name;
					// store item's name without (Clone) at the end
					items += t.name.Substring(0, t.name.Length-7);
				}
			}
			SavedData.UnlockedCharacters = items;
		}
		
		PowerupSpawner.loadoutChange = true;
	}
	
	void OnDrag(DragInfo di) {
		dragging = true;
	}
	void OnDragEnd(Vector2 pos) {
		dragging = false;
	}
	
	void OnTouchUp(Vector2 pos) {
		if (!dragging) {
			Ray ray = Camera.main.ScreenPointToRay(pos);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 1000)) {
				if (hit.transform.GetComponent<InventoryItem>() != null) {
					
					if (characterLoadout != null && characterLoadout.LastRemoved == hit.transform) {
						// this fires before inventory panels' touch up handlers, so add the item manually here
						characterLoadout.AddItem(hit.transform);
						SceneButton sb = gameObject.AddComponent<SceneButton>();
						sb.sceneName = "ItemLoadoutScene";
						sb.Fire();
						Destroy(sb);
					}
				}
			}
		}
	}
}
