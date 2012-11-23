using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	float radius = 10.0f;
	
	
	void Awake() {
		// calculate own radius
		if (transform.localScale.z > transform.localScale.x) {
			radius = 10+(transform.localScale.z*transform.parent.localScale.z);
		}
		else {
			radius = 10+(transform.localScale.x*transform.parent.localScale.x);
		}
	}
	
	// Update is called once per frame
//	void Update () {
//		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
//		
//		foreach (Collider c in colliders) {
//			AvoidanceBehaviour ab = c.GetComponent<AvoidanceBehaviour>();
//			if (ab != null) {
//				ab.AddObstacle(transform);
//			}
//		}
//	}
}
