using UnityEngine;
using System.Collections;

public class LevelButton : Button {
	public string sceneName;
	public string levelName;
	
	public override void Fire() {
		GameObject loader = Instantiate(Resources.Load("Prefabs/UI/LoadingGraphic")) as GameObject;
		
		GameObject[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		foreach (GameObject go in allObjects) {
			if (!(go == Camera.main.gameObject) && !(go == loader) && (!go.transform.parent == loader)) go.SetActiveRecursively(false);
		}
		
		SavedData.CurrentLevel = levelName;
		
		Application.LoadLevelAsync(sceneName);
	}
}
