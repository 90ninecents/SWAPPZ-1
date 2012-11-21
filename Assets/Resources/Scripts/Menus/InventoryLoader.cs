using UnityEngine;
using System.Collections;

public class InventoryLoader : MonoBehaviour {
	// Add this to the parent of inventory panels that require loading on startup
	
	void Start() {
		// debugging
		//PlayerPrefs.DeleteAll();
		
		string debugInventory = "ItemNuke|ItemArmor|ItemHealth|ItemDamage|ItemSpeed|ItemSpinAttack|ItemNuke|ItemArmor|ItemHealth|ItemDamage|ItemSpeed|ItemSpinAttack";
		SavedData.Inventory = debugInventory;
		
		string debugRoster = "Donatello|Raphael|Michelangelo|Leonardo|Splinter"; // |April to come back in future?
		SavedData.UnlockedCharacters = debugRoster;
		// ----
		
		InventoryPanel[] panels = transform.GetComponentsInChildren<InventoryPanel>();
		
		foreach (InventoryPanel p in panels) {
			if (p.populateOnStartup) {
				if (p.capacity <= 6) {
					// Characters
					string[] roster = SavedData.UnlockedCharacters.Split(SavedData.Separator[0]);
					GameObject go;
					
					foreach (string s in roster) {
						if (s != "") {
							go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s)) as GameObject;
							p.AddItem(go.transform);
						}
					}
				}
				else {
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
}
