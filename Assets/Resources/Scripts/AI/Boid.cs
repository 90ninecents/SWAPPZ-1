using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boid : MonoBehaviour {
	List<SteeringBehaviour> behaviours;
	public float maxSpeed = 1.0f;
	float currentSpeed = 1.0f;
	public float turningSpeed = 10.0f;
	public float jumpHeight = 10.0f;
	float distanceToJump = 0.0f;
	bool falling = false;
	bool jumping = false;
	
	int jumpCount = 0;
	
	public float Speed { 
		get { return currentSpeed; } 
		set { currentSpeed = value; }
	}
	
	void Awake() {
		behaviours = new List<SteeringBehaviour>();
		
		foreach (SteeringBehaviour s in transform.GetComponents<SteeringBehaviour>()) {
			behaviours.Add(s);
		}
	}
	
	public void AddBehaviour(SteeringBehaviour b) {
		behaviours.Add(b);
	}
	
	public void RemoveBehaviour(SteeringBehaviour b) {
		behaviours.Remove(b);
	}
	
	public SteeringBehaviour GetBehaviour(string identifier) {
		foreach (SteeringBehaviour b in behaviours) {
			if (b.identifier == identifier) {
				return b;
			}
		}
		return null;
	}
	
	void OnCollisionEnter(Collision collisionInfo) {
		// On collision, check if top of other collider is above boid's feet and also lower than max jumping height
		distanceToJump = collisionInfo.collider.bounds.max.y-transform.collider.bounds.min.y;
		
		//Debug.Log(collisionInfo.collider.bounds.max.y+" "+transform.collider.bounds.min.y);
		
		if (collisionInfo.collider.gameObject.GetComponent<Boid>() == null  && distanceToJump > 1f && distanceToJump <= jumpHeight) {
			//jumping = true;
		}
	}
	
	// Called before each physics timestep
	void FixedUpdate () {
		falling = (transform.rigidbody.velocity.y < -9.81f);
		
		if (!falling) {
			Vector3 combinedSteering = new Vector3();
			
			foreach (SteeringBehaviour b in behaviours) {			
				combinedSteering += b.CalculateSteering(transform.position);
			}
			
			if (combinedSteering != Vector3.zero) {
				combinedSteering.y = 0;
				transform.rigidbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(combinedSteering), Time.deltaTime*turningSpeed);
				transform.rigidbody.velocity = transform.forward*currentSpeed;
			}
			else {
				transform.rigidbody.velocity = Vector3.zero;
			}
			
			if (jumping) {
				
				transform.rigidbody.velocity += new Vector3(0,transform.rigidbody.mass*5,0);
				jumpCount++;
				if (jumpCount >= jumpHeight/5) {
					jumping = false;
					jumpCount = 0;
				}
			}
		}
		
		// Apply gravity
		if (transform.rigidbody.useGravity) transform.rigidbody.velocity += Physics.gravity;
		
		//if (transform.rigidbody.velocity.y < Physics.gravity.y) transform.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
		//else transform.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
}