using UnityEngine;
using System.Collections;

public class SteeringBehaviour : MonoBehaviour {
	public string identifier;
	public float weight = 1.0f;
	protected Vector3 lastSteering = new Vector3();
	
	public Vector3 Steering { get { return lastSteering; } }
	
	public void SetWeight(float w) {
		weight = w;
	}
	
	virtual public Vector3 CalculateSteering(Vector3 pos) {
		// Override this in subclasses
		return new Vector3();
	}
}
