using UnityEngine;
using System.Collections;

[RequireComponent (typeof(TrailRenderer))]
public class SwipeTracker : MonoBehaviour {
	float origZ;

	void OnEnable () {
		Gesture.onDraggingE += OnDrag;
		
		origZ = transform.position.z;
	}
	
	void OnDisable () {
		Gesture.onDraggingE -= OnDrag;
	}
	
	void OnDrag(DragInfo di) {
		if (di.delta.magnitude > 25) {
			Vector3 pos = new Vector3(di.pos.x - Screen.width/2, di.pos.y - Screen.height/2, origZ);
			pos += Camera.main.transform.position;
			
			transform.position = new Vector3(pos.x, pos.y, origZ);
		}
	}
}
