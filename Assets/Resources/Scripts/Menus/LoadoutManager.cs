using UnityEngine;
using System.Collections;

public class LoadoutManager : MonoBehaviour {
	public Transform itemLoadoutPanel;
	public Transform characterLoadoutPanel;
	InventoryPanel itemLoadout;
	InventoryPanel characterLoadout;
	
	void OnEnable() {
		itemLoadout = itemLoadoutPanel.GetComponent<InventoryPanel>();
		characterLoadout = characterLoadoutPanel.GetComponent<InventoryPanel>();
		
		print ("enable");
	}
	
	void OnDisable() {
		if (itemLoadout.Items.Length > 0) {
			string items ="";
			foreach (Transform t in itemLoadout.Items) {
				if (t != null) {
					if (items != "") items += SavedData.Separator;
					items += (t.GetComponent<InventoryItem>().itemPrefab.name);
				}
			}
			
			SavedData.ItemLoadout = items;
		}
		
		if (characterLoadout.Items.Length > 0) {
			string items ="";
			foreach (Transform t in characterLoadout.Items) {
				if (t != null) {
					if (items != "") items += SavedData.Separator;
					items += (t.GetComponent<InventoryItem>().itemPrefab.name);
				}
			}
			
			SavedData.CharacterLoadout = items;
		}
	}
}
