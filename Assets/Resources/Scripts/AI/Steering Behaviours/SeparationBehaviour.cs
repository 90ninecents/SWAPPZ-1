using UnityEngine;
using System.Collections;

public class SeparationBehaviour : SteeringBehaviour {
	public Transform flock;
	public float distanceLimit = 50.0f;
	
	void Start() {
		if (flock == null) flock = transform.parent;
	}
	
	override public Vector3 CalculateSteering(Vector3 pos) {
		Vector3 steering = new Vector3();
		
		if (flock != null && flock.childCount > 0) {
			foreach (Transform neighbour in flock) {
				if (neighbour != transform) {
					float distance = Mathf.Abs(Vector3.Distance(transform.position, neighbour.position));
					
					if (distance < distanceLimit) {
						steering += (transform.position - neighbour.position).normalized;
					}
				}
			}
		}
		
		lastSteering = steering*weight;
		return lastSteering;
	}
}
