using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flock : MonoBehaviour {
	
	void Start() {
	}
	
	public Vector3 GetAveragePosition () {
		Vector3 averagePosition = new Vector3();
		
		foreach (Transform t in transform) {
			averagePosition += t.position;
		}
		
		int count = transform.childCount;
		averagePosition.Scale(new Vector3(1.0f/count, 1.0f/count, 1.0f/count));
		
		return averagePosition; 
	}
	
	public Vector3 GetAverageVelocity () {
		Vector3 averageVelocity = new Vector3();
		
		foreach (Transform t in transform) {
			averageVelocity += t.forward * (t.GetComponent<Boid>().maxSpeed+0.1f);
		}
		
		int count = transform.childCount;
		averageVelocity.Scale(new Vector3(1.0f/count, 1.0f/count, 1.0f/count));
		
		return averageVelocity; 
	}
}
