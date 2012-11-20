using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public float radius = 10.0f;
	
	
	// Update is called once per frame
	void Update () {
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		
		foreach (Collider c in colliders) {
			AvoidanceBehaviour ab = c.GetComponent<AvoidanceBehaviour>();
			if (ab != null) {
				ab.AddObstacle(transform);
			}
		}
	}
}
