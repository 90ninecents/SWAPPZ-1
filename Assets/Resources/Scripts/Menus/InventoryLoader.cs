using UnityEngine;
using System.Collections;

public class InventoryLoader : MonoBehaviour {
	// Add this to the parent of inventory panels that require loading on startup
	
	void Start() {
		
		SavedData.UnlockedCharacters = "CharacterLeonardo|CharacterDonatello|CharacterMichelangelo|CharacterRaphael";
		
		LoadoutManager manager = transform.GetComponent<LoadoutManager>();
		
		InventoryPanel[] panels = transform.GetComponentsInChildren<InventoryPanel>();
		
		foreach (InventoryPanel p in panels) {
			if (p.transform == manager.rosterPanel) {
				// Available Characters
				string[] roster = SavedData.UnlockedCharacters.Split(SavedData.Separator[0]);
				GameObject go;
				
				foreach (string s in roster) {
					if (s != "") {
						go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s)) as GameObject;
						p.AddItem(go.transform);
					}
				}
			}				
			
			else if (p.transform == manager.inventoryPanel) {
				// Inventory
				string[] inventory = SavedData.Inventory.Split(SavedData.Separator[0]);
				GameObject go;
				
				foreach (string s in inventory) {
					if (s != "") {
						go = Instantiate(Resources.Load("Prefabs/Loadout Items/"+s)) as GameObject;
						p.AddItem(go.transform);
					}
				}
			}
				
		}
	}
}
