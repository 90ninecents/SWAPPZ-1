using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {
	static string powerupPath = "Prefabs/Objects/Powerups/";
	static string[] powerupChoices;
	GameObject powerup;
	
	public bool available = true;
	
	void Awake() {
		if (powerupChoices == null) powerupChoices = SavedData.ItemLoadout.Split(SavedData.Separator[0]);
	}
	
	void Update() {
		if (!available) available = (powerup == null);
	}
	
	public void Spawn() {
		if (available) {
			string p = powerupChoices[Random.Range(0,powerupChoices.Length)];
			
			if (p != "") {
				powerup = Instantiate(Resources.Load(powerupPath+p)) as GameObject;
				powerup.transform.position = transform.position;
				powerup.transform.position += new Vector3(0,powerup.renderer.bounds.extents.y,0);
				
				available = false;
			}
		}
	}
}
