using UnityEngine;
using System.Collections;

public class Carousel : MonoBehaviour {
	
	int numberOfSlots = 4;
	public bool vertical = true;
	public Transform[] items;
	
	bool dragging = false;
	
	Transform selectedItem;
	public Transform SelectedItem { get { return selectedItem; } }

	// Use this for initialization
	void Awake () {
		items = new Transform[transform.parent.childCount-1];
		
		int i = 0;
		
		foreach (Transform t in transform.parent) {
			if (t != transform) {
				items[i] = t;			
				i++;
			}
		}
		
		selectedItem = items[0];
	}
	
	void OnEnable() {
		Gesture.onTouchDownE += OnTouch;
		Gesture.onDraggingE += OnDrag;
		Gesture.onDraggingEndE += OnDragEnd;
	}
	
	void OnDisable() {
		Gesture.onTouchDownE -= OnTouch;
		Gesture.onDraggingE -= OnDrag;
		Gesture.onDraggingEndE += OnDragEnd;
	}
	
	
	void OnTouch(Vector2 touchPos) {
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		
		foreach (RaycastHit hit in hits) {
			if (hit.collider ==  transform.collider) {
				dragging = true;
				break;
			}
		}
	}
	
	void OnDrag(DragInfo di) {
		if (dragging) {
			if (vertical) transform.RotateAroundLocal(new Vector3(1,0,0), -di.delta.y/100);
			else transform.RotateAroundLocal(new Vector3(0,1,0), -di.delta.x/100);
		}
	}
	
	void OnDragEnd(Vector2 touchPos) {
		if (dragging) {
			// snap to nearest portrait
			float absRotation;
			if (vertical) absRotation = transform.rotation.eulerAngles.x;
			else absRotation = transform.rotation.eulerAngles.y;
			
			if (absRotation < 0) absRotation += 360;
			
			// 316-45 = 0
			if (absRotation >= 315 || absRotation <= 45) absRotation = 0;
			// 46-135 = 90
			else if (absRotation >= 45 && absRotation <= 135) absRotation = 90;
			// 136-225 = 180
			else if (absRotation >= 135 && absRotation <= 225) absRotation = 180;
			// 226-315 = 270
			else if (absRotation >= 225 && absRotation <= 315) absRotation = 270;
			
			if (vertical) transform.rotation = Quaternion.Euler(absRotation,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
			else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,absRotation,transform.rotation.eulerAngles.z);
		}
		
		SetSelectedItem();
		dragging = false;
	}
	
	void SetSelectedItem() {
		// find currently centered item
		// defined as the item closest to the camera on the z-axis
		
		Transform result = null;
		
		float closestDistance = 0;
		
		foreach (Transform t in items) {
			if (t.position.z-Camera.main.transform.position.z < closestDistance|| closestDistance == 0) {
				result = t;
				closestDistance = t.position.z-Camera.main.transform.position.z;
			}
		}
		
		selectedItem = result;
	}
}
