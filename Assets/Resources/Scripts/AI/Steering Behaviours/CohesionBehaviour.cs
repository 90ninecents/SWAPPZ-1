using UnityEngine;
using System.Collections;

public class CohesionBehaviour : SteeringBehaviour {
	Flock flock;
	public float distanceLimit = 125.0f;
	
	void Start() {
		flock = transform.parent.GetComponent<Flock>();
	}
	
	override public Vector3 CalculateSteering(Vector3 pos) {
		Vector3 steering = new Vector3();
		
		Vector3 avg = flock.GetAveragePosition();
		if (Mathf.Sqrt(Mathf.Pow(transform.position.x-avg.x, 2)+Mathf.Pow (transform.position.z-avg.z, 2)) > distanceLimit) {
			steering += new Vector3(avg.x - transform.position.x, avg.y - transform.position.y, avg.z - transform.position.z).normalized;
		}
		
		return steering*weight;
	}
}
