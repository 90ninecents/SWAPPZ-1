using UnityEngine;
using System.Collections;

public class SceneButton : Button {
	public string sceneName;
	
	public override void Fire() {
		GameObject[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		foreach (GameObject go in allObjects) {
			go.active = false;
		}
		
//		GameObject g = GameObject.Find("LoadingIcon");
//		if (g != null) {
//			g.active = true;
//		}
		
		Application.LoadLevelAsync(sceneName);
	}
}
