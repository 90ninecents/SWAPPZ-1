using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {
	// Handles touch events on child Button objects
	Button lastPressed;

	void OnEnable() {
		lastPressed = null;
		
		Gesture.onTouchDownE += OnTouchDown;
		Gesture.onTouchUpE += OnTouchUp;
	}
	
	void OnDisable() {
		Gesture.onTouchDownE -= OnTouchDown;
		Gesture.onTouchUpE -= OnTouchUp;
	}
	
	void OnTouchDown (Vector2 touchPos) {
		if (Camera.main != null) {
			Ray ray = Camera.main.ScreenPointToRay(touchPos);
		
			foreach (Transform t in transform) {
				if (t.GetComponent<Button>() != null && t.gameObject.active && t.GetComponent<Button>().enabled) {
					if (t.guiTexture != null) {
						if (t.guiTexture.enabled &&
							t.guiTexture.GetScreenRect().Contains(touchPos)) {
							lastPressed = t.GetComponent<Button>();
							lastPressed.ToggleState();
							break;
						}
					}
					else if (t.guiText != null) {
						if (t.guiText.enabled &&
							t.guiText.GetScreenRect().Contains(touchPos)) {
							lastPressed = t.GetComponent<Button>();
							lastPressed.ToggleState();
							break;
						}
					}
					else if (t.collider != null) {
						if (t.collider.bounds.IntersectRay(ray)) {
							lastPressed = t.GetComponent<Button>();
							lastPressed.ToggleState();
							break;
						}
					}
				}
			}
		}
	}
	
	void OnTouchUp (Vector2 touchPos) {
//		if (lastPressed != null) {
//			lastPressed.ToggleState();
//			lastPressed = null;
//		}
		
		if (lastPressed != null && Camera.main != null) {
			lastPressed.ToggleState();
			Transform t = lastPressed.transform;
			
			Ray ray = Camera.main.ScreenPointToRay(touchPos);
		
			//foreach (Transform t in transform) {
				//if (t.GetComponent<Button>() != null && t.gameObject.active) {
					if (t.guiTexture != null) {
						if (t.guiTexture.enabled &&
							t.guiTexture.GetScreenRect().Contains(touchPos)) {
							TriggerButton(t);
						}
					}
					else if (t.guiText != null) {
						if (t.guiText.enabled &&
							t.guiText.GetScreenRect().Contains(touchPos)) {
							TriggerButton(t);
						}
					}
					else if (t.collider != null) {
						if (t.collider.bounds.IntersectRay(ray)) {
							TriggerButton(t);
						}
					}
				//}
			//}
		}
		
		lastPressed = null;
	}
	
	void TriggerButton(Transform t) {
		if (t.GetComponent<AttackButton>() == null) {
			if (t.name == "BtnBack") AudioManager.PlayAudio("ButtonBack", "Effects");
			else AudioManager.PlayAudio("ButtonForward", "Effects");
		}
		
		t.GetComponent<Button>().PreFire();
		t.GetComponent<Button>().Fire();
	}
}
