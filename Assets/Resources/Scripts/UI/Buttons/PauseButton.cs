using UnityEngine;
using System.Collections;

public class PauseButton : Button {
	
	public Transform pauseMenu;
	bool paused = false;
	float cooldown = 0.5f;
	
	bool cooling = false;
	
	public override void Fire() {
		if (!cooling) {
			
			paused = (Time.timeScale == 0.0f);
			
			if (paused) Time.timeScale = 1.0f;		// unpause
			else Time.timeScale = 0.0f;				// pause
			
			if (pauseMenu != null) pauseMenu.gameObject.SetActiveRecursively(!paused);
			
			cooling = true;
			Invoke("Cooldown", cooldown);
		}
	}
	
	void Cooldown() {
		cooling = false;
	}
}
