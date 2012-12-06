using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {
	
	public float maxOffsetRadius = 0.1f;
	Vector2 restPosition;
	Vector2 restScreenPosition;
	
	void Start() {
		//restPosition = transform.position;
		restScreenPosition = new Vector2(transform.guiTexture.pixelInset.center.x/Screen.width, transform.guiTexture.pixelInset.center.y/Screen.height);
		restPosition = transform.position;
	}

	void OnEnable() {
		Gesture.onDraggingE += OnDrag;
		Gesture.onDraggingEndE += OnDragEnd;
	}
	
	void OnDisable() {
		Gesture.onDraggingE -= OnDrag;
		Gesture.onDraggingEndE -= OnDragEnd;
	}
	
	void OnDrag(DragInfo dragInfo) {		
		Vector2 position = new Vector3(dragInfo.pos.x/Screen.width, dragInfo.pos.y/Screen.height);
		
		if ((restScreenPosition-position).magnitude <= maxOffsetRadius) {
			transform.position = (position-restScreenPosition);
		}
		else if (new Vector2(transform.position.x, transform.position.y) != restPosition) {
			transform.position = restPosition + (position-restScreenPosition).normalized*maxOffsetRadius;
		}
	}
	
	public Vector3 GetDrive() {
		return new Vector3((restPosition.x - transform.position.x)/maxOffsetRadius, 0, (restPosition.y - transform.position.y)/maxOffsetRadius);
	}
	
	void OnDragEnd (Vector2 pos) {
		transform.position = restPosition;
	}
}
