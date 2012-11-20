using UnityEngine;
using System.Collections;

public class FleeBehaviour : SteeringBehaviour {
	public Vector3 targetPoint;
	public Transform targetObject;
	
	public void SetFleeTargetPoint(Vector3 point) {
		targetPoint = point;
	}
	
	public void SetFleeTargetObject(Transform obj) {
		targetObject = obj;
	}
	
	override public Vector3 CalculateSteering(Vector3 pos) {
		Vector3 steering = new Vector3();
		
		// Calculate steering
		if (targetPoint != null) {
			steering = new Vector3(pos.x - targetPoint.x, 0, pos.z - targetPoint.z).normalized;
			steering.Scale(new Vector3(weight, weight, weight));
		}
		if (targetObject != null) {
			steering = new Vector3(pos.x - targetObject.position.x, 0, pos.z - targetObject.position.z).normalized;
			steering.Scale(new Vector3(weight, weight, weight));
		}
		
		return steering;
	}
}
