using UnityEngine;
using System.Collections;

public class SteeringBehaviour : MonoBehaviour {
	public string identifier;
	public float weight = 1.0f;
	
	public void SetWeight(float w) {
		weight = w;
	}
	
	virtual public Vector3 CalculateSteering(Vector3 pos) {
		// Override this in subclasses
		return new Vector3();
	}
}
