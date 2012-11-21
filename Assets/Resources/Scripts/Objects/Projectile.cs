using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	float distanceToTravel = 0.0f;
	float speed = 0.0f;
	Vector3 startPosition = Vector3.zero;
	
	public void Launch(float velocity, Vector3 direction, float distance) {
		speed = velocity;
		distanceToTravel = distance;
		transform.rotation = Quaternion.LookRotation(direction);
		transform.rigidbody.velocity = velocity*transform.forward;
		
		startPosition = transform.position;
	}
	
	void Update() {
		if ((startPosition-transform.position).magnitude > distanceToTravel) {
			Destroy(gameObject);
		}
	}
}
