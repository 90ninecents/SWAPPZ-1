using UnityEngine;
using System.Collections;

public class PlayButton : SceneButton {	
	void Awake() {
		sceneName = SavedData.CurrentLevel;
	}
}
