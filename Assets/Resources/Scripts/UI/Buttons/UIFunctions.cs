using UnityEngine;
using System.Collections;

public class UIFunctions {

	
	public static void MuteButton (UIButton obj) {
		AudioManager.ToggleMute();
	}
	
	public static void PauseButton (UIButton obj) {
		
	}
	
	public static void NextButton (UIButton obj) {
		StartLoadingGraphic();
		Application.LoadLevel(Application.loadedLevel+1);
	}
	
	public static void BackButton (UIButton obj) {
		StartLoadingGraphic();
		Application.LoadLevel(Application.loadedLevel-1);
	}	
	
	static void StartLoadingGraphic() {
		GameObject loader = Object.Instantiate(Resources.Load("Prefabs/UI/LoadingGraphic")) as GameObject;
		
		GameObject[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		foreach (GameObject go in allObjects) {
			if (!(go == Camera.main.gameObject) && !(go == loader) && (!go.transform.parent == loader) && !(go == AudioManager.instance.gameObject)) go.SetActiveRecursively(false);
		}
	}
}
