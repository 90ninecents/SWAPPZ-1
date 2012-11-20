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
			if (t.guiTexture != null) {
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
