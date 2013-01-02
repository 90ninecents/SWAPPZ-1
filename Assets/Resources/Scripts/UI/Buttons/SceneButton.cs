using UnityEngine;
using System.Collections;

public class SceneButton : Button {
	public string sceneName;
	
	public override void Fire() {
		GameObject loader = Instantiate(Resources.Load("Prefabs/UI/LoadingGraphic")) as GameObject;
		
		GameObject[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		foreach (GameObject go in allObjects) {
			if (!(go == Camera.main.gameObject) && !(go == loader) && (!go.transform.parent == loader) && !(go == MusicPlayer.music.gameObject)) go.SetActiveRecursively(false);
		}
		
		Application.LoadLevelAsync(sceneName);
	}
}
