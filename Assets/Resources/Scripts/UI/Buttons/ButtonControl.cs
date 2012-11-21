using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {
	// Handles touch events on child Button objects

	void OnEnable() {
		Gesture.onTouchUpE += OnTouch;
	}
	
	void OnDisable() {
		Gesture.onTouchUpE -= OnTouch;
	}
	
	void OnTouch (Vector2 touchPos) {
		
		foreach (Transform t in transform) {
			if (t.GetComponent<Button>() != null) {
				
				if (t.renderer != null) {
					if (t.renderer.bounds.Contains(new Vector3(touchPos.x-Screen.width/2, touchPos.y-Screen.height/2, t.position.z))) {
						t.GetComponent<Button>().Fire();
						break;
					}
				}
				else if (t.guiTexture != null) {
					if (t.guiTexture.GetScreenRect().Contains(touchPos)) {
						t.GetComponent<Button>().Fire();
						break;
					}
				}
				else if (t.guiText != null) {
					if (t.guiText.GetScreenRect().Contains(touchPos)) {
						t.GetComponent<Button>().Fire();
						break;
					}
				}
			}
		}
	}
}
