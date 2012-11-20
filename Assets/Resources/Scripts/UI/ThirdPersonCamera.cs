using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
	
	void Start() {
		if (target != null) offset = transform.position-target.position;
	}
	
	public void SetTarget(Transform t) {
		target = t;
		if (offset == Vector3.zero) offset = transform.position-target.position;
	}
	
	void Update() {
		if (target != null) transform.position = target.position+offset;
	}
}
