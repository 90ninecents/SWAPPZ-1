using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {
	static GameObject[] powerupChoices;
	public static bool loadoutChange = true;
	
	GameObject powerup;
	
	public bool available = true;
	
	void OnEnable() {
		if (loadoutChange) {
			string[] temp = SavedData.ItemLoadout.Split(SavedData.Separator[0]);
			powerupChoices = new GameObject[temp.Length];
			
			int count = 0;
			foreach (string s in temp) {
				if (s != "") {
					GameObject go = Resources.Load("Prefabs/Loadout Items/"+s) as GameObject;
					
					powerupChoices[count] = go.GetComponent<InventoryItem>().itemPrefab;
					count++;
				}
			}
			
			loadoutChange = false;
		}
	}
	
	void Update() {
		if (!available) available = (powerup == null);
	}
	
	public void Spawn() {
		if (available && powerupChoices.Length > 0) {
			GameObject p = powerupChoices[Random.Range(0,powerupChoices.Length)];
			
			if (p != null) {
				powerup = Instantiate(p) as GameObject;
				powerup.transform.position = transform.position;
				powerup.transform.position += new Vector3(0,powerup.renderer.bounds.extents.y,0);
				
				available = false;
			}
		}
	}
}
