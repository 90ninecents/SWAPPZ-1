using UnityEngine;
using System.Collections;

public class PlayButton : SceneButton {	
	void Awake() {
		sceneName = SavedData.CurrentLevel;
	}
	
	public override void PreFire() {
		//AudioManager.StopChannel("Background");
	}
}
