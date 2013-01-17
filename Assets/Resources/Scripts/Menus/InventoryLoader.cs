using UnityEngine;
using System.Collections;

public class InventoryLoader : MonoBehaviour {
	// Add this to the parent of inventory panels that require loading on startup
	
	void Start() {
		LoadoutManager manager = transform.GetComponent<LoadoutManager>();
		
		//InventoryPanel[] panels = transform.GetComponentsInChildren<InventoryPanel>();
		
		InventoryPanel p = null;
		Carousel c = null;
		
		if (manager.inventory != null) p = manager.inventory;
		if (manager.roster != null) c = manager.roster;
		
//		foreach (InventoryPanel p in panels) {
//			if (p == manager.roster) {
//				// Available Characters
//				string[] roster = SavedData.Characters.Split(SavedData.Separator[0]);
//				string[] unlocks = SavedData.UnlockedCharacters.Split(SavedData.Separator[0]);
//				
//				GameObject go;
//				
//				foreach (string s in roster) {
//					if (s != "") {
//						bool unlocked = false;
//						
//						foreach (string t in unlocks) {
//							if (t == s) {
//								unlocked = true;
//								break;
//							}
//						}
//						
//						// Load locked/unlocked textures
//						if (unlocked) {
//							go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s)) as GameObject;
//							p.AddItem(go.transform);
//						}
//						else {
//							go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s+"Locked")) as GameObject;
//							p.AddItem(go.transform, false);
//						}
//						
//						go.transform.parent = transform;
//					}
//				}
//			}				
			
		if (p != null) {
			// Inventory
			SavedData.Inventory = "ItemArmor|ItemCombo|ItemFlashBomb|ItemInvincibility|ItemNuke|ItemPizzaFull|ItemSizeUp|ItemSlow|ItemSpeed|ItemSpinAttack|ItemXP";
			
			string[] inventory = SavedData.Inventory.Split(SavedData.Separator[0]);
			GameObject go;
			
			foreach (string s in inventory) {
				if (s != "") {
					go = Instantiate(Resources.Load("Prefabs/Loadout Items/"+s)) as GameObject;
					p.AddItem(go.transform);
				}
				
			}
		}
		
		
		else if (c != null) {
			// Available Characters
			string[] roster = SavedData.Characters.Split(SavedData.Separator[0]);
			string[] unlocks = SavedData.UnlockedCharacters.Split(SavedData.Separator[0]);
			
			GameObject go;
			
			int i = 0;
			foreach (string s in roster) {
				if (s != "") {
					bool unlocked = false;
					
					foreach (string t in unlocks) {
						if (t == s) {
							unlocked = true;
							break;
						}
					}
					
					// Load locked/unlocked textures
					if (unlocked) {
						go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s)) as GameObject;
						
						go.transform.position = c.items[i].position;
						go.transform.localScale = c.items[i].localScale;
						go.transform.rotation = c.items[i].rotation;
						
						Destroy(c.items[i].GetChild(0).gameObject);
						go.transform.GetChild(0).parent = c.items[i];
						Destroy(go);
						
						c.items[i].name = s;
						//p.AddItem(go.transform);
					}
					else {
						go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s+"Locked")) as GameObject;
						
						go.transform.position = c.items[i].position;
						go.transform.localScale = c.items[i].localScale;
						go.transform.rotation = c.items[i].rotation;
						
						Destroy(c.items[i].GetChild(0).gameObject);
						go.transform.GetChild(0).parent = c.items[i];
						Destroy(go);
						
						c.items[i].name = s+"Locked";
					}
				}
				
				i++;
			}
			c.RotateTo(270);
		}
		
	}
}
