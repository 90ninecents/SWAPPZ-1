using UnityEngine;
using System.Collections;

public class ArrivalBehaviour : SteeringBehaviour {
	public Vector3 targetPoint;
	public Transform targetObject;
	public float stoppingRadius = 50.0f;			// Distance from target before slowing to a stop
	
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
		
		Vector3 diff = targetPoint-transform.position;
		diff.y = 0;
		
		if (diff.magnitude > stoppingRadius) {
			steering = new Vector3(targetPoint.x - pos.x, 0, targetPoint.z - pos.z).normalized;
		}
		
		return steering*weight;
	}
}
