using UnityEngine;
using System.Collections;

public class Carousel : MonoBehaviour {
	
	public Transform lockedPopup;
	
	int numberOfSlots = 4;
	public bool vertical = true;
	public Transform[] items;
	public bool fullScreen = false;
	
	bool dragging = false;
	bool touchWithin = false;
	
	Transform selectedItem;
	public Transform SelectedItem { get { return selectedItem; } }
	
	Vector3 inheritedRotation;

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
		
		inheritedRotation = transform.rotation.eulerAngles;
	}
	
	void OnEnable() {
		Gesture.onTouchDownE += OnTouch;
		Gesture.onTouchUpE += OnTouchUp;
		Gesture.onDraggingE += OnDrag;
		Gesture.onDraggingEndE += OnDragEnd;
	}
	
	void OnDisable() {
		Gesture.onTouchDownE -= OnTouch;
		Gesture.onTouchUpE -= OnTouchUp;
		Gesture.onDraggingE -= OnDrag;
		Gesture.onDraggingEndE -= OnDragEnd;
	}
	
	
	void OnTouch(Vector2 touchPos) {
		if (fullScreen) {
			touchPos.x = transform.position.x+Screen.width/2;
		}
		
		
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		
		foreach (RaycastHit hit in hits) {
			if (hit.collider ==  transform.collider) {
				touchWithin = true;
				break;
			}
		}
	}
	
	void OnTouchUp(Vector2 pos) {
		if (touchWithin && !dragging) {
			touchWithin = false;
			
			if (lockedPopup != null && SelectedItem.name.Substring(SelectedItem.name.Length-6, 6) == "Locked") {
				lockedPopup.gameObject.SetActiveRecursively(true);
			}
		}
	}
	
	void OnDrag(DragInfo di) {
		if (touchWithin) {
			dragging = true;
			transform.RotateAroundLocal(new Vector3(0,1,0), -di.delta.x/100);
		}
		else {
			OnTouch(di.pos);
		}
	}
	
	void OnDragEnd(Vector2 touchPos) {
		if (dragging) {
			// snap to nearest portrait
			float absRotation = transform.rotation.eulerAngles.y-inheritedRotation.y;
			
			if (absRotation < 0) absRotation += 360;
			
			// 316-45 = 0
			if (absRotation >= 315 || absRotation <= 45) absRotation = 0;
			// 46-135 = 90
			else if (absRotation >= 45 && absRotation <= 135) absRotation = 90;
			// 136-225 = 180
			else if (absRotation >= 135 && absRotation <= 225) absRotation = 180;
			// 226-315 = 270
			else if (absRotation >= 225 && absRotation <= 315) absRotation = 270;
			
			//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,absRotation+inheritedRotation.y,transform.rotation.eulerAngles.z);
			RotateTo(absRotation);
			
			SetSelectedItem();
			dragging = false;
		}
	}
	
	public void Reset() {
		dragging = true;
		OnDragEnd(new Vector2(0,0));
	}
	
	void SetSelectedItem() {
		// find currently centered item
		// defined as the item closest to the camera on the z-axis
		
		Transform result = null;
		
		float closestDistance = 0;
		
		foreach (Transform t in items) {
			if (Mathf.Abs(t.position.z-Camera.main.transform.position.z) < closestDistance|| closestDistance == 0) {
				result = t;
				closestDistance = Mathf.Abs (t.position.z-Camera.main.transform.position.z);
			}
		}
		
		selectedItem = result;
	}
	
	public void RotateTo(float angle) {
		transform.Rotate(Vector3.up, angle-transform.localEulerAngles.y, Space.Self);
		Invoke("SetSelectedItem", 0.05f);
		//transform.Rotate(transform.up, angle, Space.World);
		//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,angle,transform.rotation.eulerAngles.z);
	}
}
