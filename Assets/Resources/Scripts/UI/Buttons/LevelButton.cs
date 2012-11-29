using UnityEngine;
using System.Collections;

public class LevelButton : Button {
	public string sceneName;
	public string levelName;
	
	public override void Fire() {
		GameObject[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		foreach (GameObject go in allObjects) {
			if (go.name == "LoadingIcon") {
				go.renderer.enabled = true;
			}
			else go.active = false;
		}
		
		SavedData.CurrentLevel = levelName;
		
		Application.LoadLevelAsync(sceneName);
	}
}
