using UnityEngine;
using System.Collections;

public class ScrollingPane : MonoBehaviour {
	
	public float scrollSpeed = 1.0f;
	public bool vertical = false;
	
	float upperScrollLimit = 0.0f;
	
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
		if (vertical) scrollDelta = info.delta.y;
		else scrollDelta = info.delta.x;
		
		MoveChildren(scrollDelta*scrollSpeed);
		
		scrollDelta /= Mathf.Abs(scrollDelta);
	}
	
	void MoveChildren(float d) {
		foreach(Transform t in transform) {
			if (vertical) {
				t.position = new Vector3(0,d,0);
			}
			else {
				t.position += new Vector3(d,0,0);
			}
		}
		
		if (first.position.x > upperScrollLimit) {
			MoveChildren(upperScrollLimit-first.position.x-5);
		}
		else if (last.position.x < upperScrollLimit) {
			MoveChildren(upperScrollLimit-last.position.x+5);
		}
	}
	
	void OnDragEnd(Vector2 touchPos) {
		Transform closest = null;
		float distance = 0;
		
		float direction = 0;
		
		foreach (Transform t in transform) {
			float d = (transform.position-t.position).magnitude;
			
			direction = (transform.position.x-t.position.x);
			direction /= Mathf.Abs(direction);
			
			if (closest == null || d < distance) {
				if  (direction == scrollDelta || d < 100) {
					closest = t;
					distance = d;
				}
			}
		}
		
		if (closest.position.y < transform.position.y || closest.position.x < transform.position.x) MoveChildren(distance);
		else MoveChildren(-distance);
	}
}
