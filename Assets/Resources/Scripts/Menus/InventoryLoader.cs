using UnityEngine;
using System.Collections;

public class InventoryLoader : MonoBehaviour {
	// Add this to the parent of inventory panels that require loading on startup
	
	void Start() {
		// debugging
		//PlayerPrefs.DeleteAll();
//		SavedData.Inventory = "";
//		SavedData.ItemLoadout = "";
//		SavedData.CharacterLoadout = "";
//		SavedData.UnlockedCharacters = "";
		
//		string debugInventory = "ItemArmor|ItemCalzone|ItemInvincibility|ItemNuke|ItemPizzaFull|ItemSpeed|ItemSpinAttack|ItemXP|ItemSizeUp|ItemCombo";
//		SavedData.Inventory = debugInventory;
//		
//		string debugItemLoadout = "ItemSlow";
//		SavedData.ItemLoadout = debugItemLoadout;
//		
//		string debugCharLoadout = "CharacterDonatello";
//		SavedData.CharacterLoadout = debugCharLoadout;
//		
//		string debugRoster = "CharacterRaphael|CharacterMichelangelo|CharacterLeonardo";
//		SavedData.UnlockedCharacters = debugRoster;
		// ----
		
		LoadoutManager manager = transform.GetComponent<LoadoutManager>();
		
		InventoryPanel[] panels = transform.GetComponentsInChildren<InventoryPanel>();
		
		foreach (InventoryPanel p in panels) {
			if (p.populateOnStartup) {
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
				
				else if (p.transform == manager.characterLoadoutPanel) {
					// Selected Characters
					string[] roster = SavedData.CharacterLoadout.Split(SavedData.Separator[0]);
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
				
				else if (p.transform == manager.itemLoadoutPanel) {
					// Selected Items
					string[] inventory = SavedData.ItemLoadout.Split(SavedData.Separator[0]);
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
