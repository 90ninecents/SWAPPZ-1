using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public float radius = 250.0f;
	
	
	void Awake() {
		// calculate own radius
//		if (transform.collider != null) {
//			if (transform.collider.bounds.extents.z > transform.collider.bounds.extents.x) {
//				radius = 4*(transform.collider.bounds.extents.z);
//			}
//			else {
//				radius = 4*(transform.collider.bounds.extents.x);
//			}
//			if (transform.name == "StreetLamp") print (radius);
//		}
//		else {
//			if (transform.localScale.z > transform.localScale.x) {
//				radius = 10+(transform.localScale.z*transform.parent.localScale.z);
//			}
//			else {
//				radius = 10+(transform.localScale.x*transform.parent.localScale.x);
//			}
//		}
	}
	
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
