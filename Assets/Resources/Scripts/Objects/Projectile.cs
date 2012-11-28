using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	float distanceToTravel = 0.0f;
	Vector3 startPosition = Vector3.zero;
	int damage = 0;
	
	public void Launch(float velocity, Vector3 direction, int strength, float distance) {
		distanceToTravel = distance;
		transform.rotation = Quaternion.LookRotation(direction);
		transform.rigidbody.velocity = velocity*transform.forward;
		damage = strength;
		
		startPosition = transform.position;
	}
	
	void OnCollisionEnter(Collision collision) {
		// On collision, damage applicable players/companions and destroy self
		PlayerController pc = collision.transform.GetComponent<PlayerController>();
		CompanionController cc = collision.transform.GetComponent<CompanionController>();
		
		if (pc != null && pc.enabled) {
			pc.TakeDamage(damage);
		}
		else if (cc != null && cc.enabled) {
			cc.TakeDamage(damage);
		}
		
		Destroy(gameObject);
	}
	
	void Update() {
		// if distance limit reached, destroy self
		if ((startPosition-transform.position).magnitude > distanceToTravel) {
			Destroy(gameObject);
		}
	}
}
