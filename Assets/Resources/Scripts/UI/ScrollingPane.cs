using UnityEngine;
using System.Collections;

public class ScrollingPane : MonoBehaviour {
	
	public float scrollSpeed = 1.0f;
	public bool vertical = false;
	
	float upperScrollLimit = 0.0f;
	float lowerScrollLimit = 0.0f;
	
	Transform first;
	Transform last;
	
	float scrollDelta = 0.0f;
	
	void Start() {
		first = null;
		last = null;
		
		if (vertical) {
			foreach (Transform t in transform) {
				if (first == null && last == null) {
					first = t;
					last = t;
				}
				else {
					if (t.position.y < first.position.y) {
						first = t;
					}
					else if (t.position.y > last.position.y) {
						last = t;
					}
				}
			}
			
			lowerScrollLimit = transform.position.y-last.position.y;
			upperScrollLimit = transform.position.y+first.position.y;
		}
		
		else {
			foreach (Transform t in transform) {
				if (first == null && last == null) {
					first = t;
					last = t;
				}
				else {
					if (t.position.x < first.position.x) {
						first = t;
					}
					else if (t.position.x > last.position.x) {
						last = t;
					}
				}
			}
			
			lowerScrollLimit = transform.position.x-last.position.x-1;
			upperScrollLimit = transform.position.x+first.position.x+1;
		}
		
		
	}

	void OnEnable () {
		Gesture.onDraggingE += OnDrag;
		Gesture.onDraggingEndE += OnDragEnd;
	}
	
	void OnDisable () {
		Gesture.onDraggingE -= OnDrag;
		Gesture.onDraggingEndE -= OnDragEnd;
	}
	
	void OnDrag (DragInfo info) {
		if (vertical) MoveChildren(info.delta.y*scrollSpeed);
		else MoveChildren(info.delta.x*scrollSpeed);
	}
	
	void MoveChildren(float d) {
		if (first.position.x <= upperScrollLimit && last.position.x >= lowerScrollLimit) {
			foreach(Transform t in transform) {
				if (vertical) {
					t.position = new Vector3(0,d,0);
				}
				else {
					t.position += new Vector3(d,0,0);
				}
			}
		}
	}
	
	void OnDragEnd(Vector2 touchPos) {
		
		Transform closest = null;
		float distance = -1;
		
		foreach (Transform t in transform) {
			float d = (transform.position-t.position).magnitude;
			if (closest == null || d < distance) {
				closest = t;
				distance = d;
			}
		}
		
		if (vertical) {
			if (closest.position.y < transform.position.y) MoveChildren(distance);
			else MoveChildren(-distance);
		}
		else {
			if (closest.position.x < transform.position.x) MoveChildren(distance);
			else MoveChildren(-distance);
		}
	}
}
