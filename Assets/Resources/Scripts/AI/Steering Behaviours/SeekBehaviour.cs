using UnityEngine;
using System.Collections;

public class SeekBehaviour : SteeringBehaviour {
	public Vector3 targetPoint;
	public Transform targetObject;
	
	public void SetSeekTargetPoint(Vector3 point) {
		targetPoint = point;
	}
	
	public void SetSeekTargetObject(Transform obj) {
		targetObject = obj;
	}
	
	override public Vector3 CalculateSteering(Vector3 pos) {
		Vector3 steering = new Vector3();
		
		// Calculate steering
		if (targetObject != null) {
			targetPoint = targetObject.position;
		}
		
		steering = new Vector3(targetPoint.x - pos.x, 0, targetPoint.z - pos.z).normalized;
		
		lastSteering = steering*weight;
		return lastSteering;
	}
}
