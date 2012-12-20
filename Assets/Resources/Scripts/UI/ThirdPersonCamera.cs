using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
	Vector3 defaultPos;
	Vector3 lastPos;
	
	public Vector3 movementAxes = new Vector3(1,1,1);
	
	public void SetTarget(Transform t) {
		target = t;
		if (offset == Vector3.zero) offset = transform.position-target.position;
		defaultPos.x = target.position.x;
		defaultPos.y = offset.y;
		defaultPos.z = offset.z;
		lastPos = defaultPos;
	}
	
	void Update() {
		Vector3 pos = target.position+offset;
		
		if (movementAxes.x == 0) pos.x = transform.position.x;
		if (movementAxes.y == 0) pos.y = transform.position.y;
		if (movementAxes.z == 0) pos.z = transform.position.z;
		
		transform.position = pos;
		
		//pos = Vector3.zero;
		if (target != Game.Player.transform) {
			pos.z = defaultPos.z + (offset.z - Mathf.Abs(Game.Player.transform.position.x-target.position.x));
			
			if (Mathf.Abs(pos.z - lastPos.z) > 0.01f && Mathf.Abs(Game.Player.transform.position.x-target.position.x) > 100) {
				transform.Translate(new Vector3(0,0, (pos.z - lastPos.z)), Space.Self);
				lastPos = pos;
			}
			
			else if (Mathf.Abs(Game.Player.transform.position.x-target.position.x) <= 100) {
				transform.position = defaultPos;
				lastPos = defaultPos;
			}
			
		}
	}
}