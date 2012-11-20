using UnityEngine;
using System.Collections;

public class PauseButton : Button {
	
	public Transform pauseMenu;
	bool paused = false;
	
	public override void Fire() {
		paused = !(Time.timeScale==0.0f);
		
		if (pauseMenu != null) pauseMenu.gameObject.SetActiveRecursively(paused);
		
		if (paused) Time.timeScale = 0.0f;
		else Time.timeScale = 1.0f;
	}
}
