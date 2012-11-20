using UnityEngine;
using System.Collections;

public class TouchTracker : MonoBehaviour {
	
	public float maxOffset = 10.0f;
	float originalY;
	
	// Use this for initialization
	void Start () {		
		originalY = transform.position.y;
	}
	
	void FixedUpdate() {
		transform.position -= Game.Joystick.GetDrive()*maxOffset;
		
		Vector3 diff = (transform.position-Game.Player.transform.position);
		if (diff.magnitude > maxOffset) {
			transform.position = Game.Player.transform.position + diff.normalized*maxOffset;
			transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
		}
		
		transform.rigidbody.velocity = Vector3.zero;
	}
}
