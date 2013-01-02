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
		
		if (Camera.main != null) {
			Ray ray = Camera.main.ScreenPointToRay(touchPos);
		
			foreach (Transform t in transform) {
				if (t.GetComponent<Button>() != null && t.gameObject.active) {
					if (t.guiTexture != null) {
						if (t.guiTexture.enabled &&
							t.guiTexture.GetScreenRect().Contains(touchPos)) {							
							TriggerButton(t);
							break;
						}
					}
					else if (t.guiText != null) {
						if (t.guiText.enabled &&
							t.guiText.GetScreenRect().Contains(touchPos)) {
							TriggerButton(t);
							break;
						}
					}
					else if (t.collider != null) {
						if (t.collider.bounds.IntersectRay(ray)) {
							TriggerButton(t);
							break;
						}
					}
				}
			}
		}
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
