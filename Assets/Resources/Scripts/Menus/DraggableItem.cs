using UnityEngine;
using System.Collections;

public class DraggableItem : MonoBehaviour {
	bool dragging = false;

	// Use this for initialization
	void OnEnable () {
		Gesture.onTouchDownE += OnTouchDown;
		Gesture.onTouchUpE += OnTouchUp;
		Gesture.onDraggingE += OnDrag;
	}
	
	void OnDisable () {
		Gesture.onTouchDownE -= OnTouchDown;
		Gesture.onTouchUpE -= OnTouchUp;
		Gesture.onDraggingE -= OnDrag;
	}
	
	void OnDrag(DragInfo di) {
		if (dragging) transform.position += new Vector3(di.delta.x, di.delta.y, 0);
	}
	
	void OnTouchDown(Vector2 touchPos) {
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit hit;
			
		if (Physics.Raycast(ray, out hit, 1000)) {
			if (hit.collider == collider) {
				dragging = true;
			}
		}
		 
	}
	void OnTouchUp(Vector2 touchPos) {
		 dragging = false;
	}
}
