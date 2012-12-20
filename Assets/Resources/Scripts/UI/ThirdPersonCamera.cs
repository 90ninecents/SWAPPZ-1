using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
	
	public Vector3 movementAxes = new Vector3(1,1,1);
	
	public void SetTarget(Transform t) {
		target = t;
		if (offset == Vector3.zero) offset = transform.position-target.position;
	}
	
	void Update() {
		Vector3 pos = target.position+offset;
		if (movementAxes.x == 0) pos.x = transform.position.x;
		if (movementAxes.y == 0) pos.y = transform.position.y;
		if (movementAxes.z == 0) pos.z = transform.position.z;
		
		if (target != null) transform.position = pos;
	}
}
