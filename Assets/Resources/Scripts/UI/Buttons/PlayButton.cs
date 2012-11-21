using UnityEngine;
using System.Collections;

public class PlayButton : Button {	
	public override void Fire() {
		GameObject[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		foreach (GameObject go in allObjects) {
			go.active = false;
		}
		
		Application.LoadLevelAsync(SavedData.CurrentLevel);
	}
}
