using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvoidanceBehaviour : SteeringBehaviour {
	List<Transform> obstacles = new List<Transform>();
	
	public void AddObstacle(Transform t) {
		obstacles.Add(t);
	}
	
	override public Vector3 CalculateSteering(Vector3 pos) {
		Vector3 steering = new Vector3();
		
		// Calculate steering
		if (obstacles.Count > 0) {
			foreach (Transform t in obstacles) {
				Vector3 dir = new Vector3(pos.x - t.position.x, 0, pos.z - t.position.z).normalized;
				steering += dir; 
			}
			
			obstacles.Clear();
		}
		
		return steering*weight;
	}
}
