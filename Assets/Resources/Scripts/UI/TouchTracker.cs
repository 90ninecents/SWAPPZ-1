using UnityEngine;
using System.Collections;

public class TouchTracker : MonoBehaviour {
	
	public float maxOffset = 10.0f;
	public Joystick inputDevice;
	public GameObject playerObject;
	float originalY;
	
	// Use this for initialization
	void Start () {		
		originalY = transform.position.y;
		if (inputDevice == null) inputDevice = Game.Joystick;
		if (playerObject == null) playerObject = Game.Player.gameObject;
	}
	
	void FixedUpdate() {
		transform.position -= inputDevice.GetDrive()*maxOffset;
		
		Vector3 diff = (transform.position-playerObject.transform.position);
		
		if (diff.magnitude > maxOffset) {
			transform.position = playerObject.transform.position + diff.normalized*maxOffset;
			transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
		}
		
		transform.rigidbody.velocity = Vector3.zero;
	}
}
