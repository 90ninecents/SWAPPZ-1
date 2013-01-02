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
							t.GetComponent<Button>().PreFire();
							t.GetComponent<Button>().Fire();
							break;
						}
					}
					else if (t.guiText != null) {
						if (t.guiText.enabled &&
							t.guiText.GetScreenRect().Contains(touchPos)) {
							t.GetComponent<Button>().PreFire();
							t.GetComponent<Button>().Fire();
							break;
						}
					}
					else if (t.collider != null) {
						if (t.collider.bounds.IntersectRay(ray)) {
							t.GetComponent<Button>().PreFire();
							t.GetComponent<Button>().Fire();
							break;
						}
					}
				}
			}
		}
		
	}
}
