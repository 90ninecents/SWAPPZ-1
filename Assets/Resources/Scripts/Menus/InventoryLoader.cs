using UnityEngine;
using System.Collections;

public class InventoryLoader : MonoBehaviour {
	// Add this to the parent of inventory panels that require loading on startup
	LoadoutManager manager;
	
	void Awake() {		
		manager = transform.GetComponent<LoadoutManager>();
		
		InventoryPanel p = null;
		Carousel c = null;
		
		if (manager.inventory != null) p = manager.inventory;
		if (manager.roster != null) c = manager.roster;
			
		if (p != null) {
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
		
		
		else if (c != null) {
			// Available Characters
			string[] roster = SavedData.Characters.Split(SavedData.Separator[0]);
			string[] unlocks = SavedData.UnlockedCharacters.Split(SavedData.Separator[0]);
			
			GameObject go;
			
			int i = 0;
			
			
			string s = "";
			
			foreach (Transform tr in c.items) {
				s = tr.name;
				
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
						c.items[i].name = s;
					}
					else {
						go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s+"Locked")) as GameObject;
						c.items[i].name = s+"Locked";
					}
					
					go.transform.GetChild(0).gameObject.layer = c.items[i].gameObject.layer;
					
					go.transform.parent = transform;
					
					//go.transform.localScale = c.items[i].localScale;
					go.transform.localScale = c.items[i].lossyScale;
					go.transform.position = c.items[i].position;
					go.transform.rotation = c.items[i].rotation;
					
					Destroy(c.items[i].GetChild(0).gameObject);
					
					go.transform.GetChild(0).parent = c.items[i];
					
					
					Destroy(go);
				}
				
				i++;
			}
			//c.RotateTo(270);
			c.Reset();
		}
			
			
			
//			foreach (string s in roster) {
//				if (s != "") {
//					bool unlocked = false;
//					
//					foreach (string t in unlocks) {
//						if (t == s) {
//							unlocked = true;
//							break;
//						}
//					}
//					
//					// Load locked/unlocked textures
//					if (unlocked) {
//						go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s)) as GameObject;
//						c.items[i].name = s;
//					}
//					else {
//						go = Instantiate(Resources.Load("Prefabs/Loadout Characters/"+s+"Locked")) as GameObject;
//						c.items[i].name = s+"Locked";
//					}
//					
//					go.transform.GetChild(0).gameObject.layer = c.items[i].gameObject.layer;
//					
//					go.transform.parent = transform;
//					
//					//go.transform.localScale = c.items[i].localScale;
//					go.transform.localScale = c.items[i].lossyScale;
//					go.transform.position = c.items[i].position;
//					go.transform.rotation = c.items[i].rotation;
//					
//					Destroy(c.items[i].GetChild(0).gameObject);
//					
//					go.transform.GetChild(0).parent = c.items[i];
//					
//					
//					Destroy(go);
//				}
//				
//				i++;
//			}
//			c.RotateTo(270);
//			c.Reset();
//		}
	}
}
