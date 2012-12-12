using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public float radius = 100.0f;
	
	
	void Awake() {
		// calculate own radius	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 groundPoint = new Vector3(transform.collider.bounds.center.x, transform.collider.bounds.center.y-transform.collider.bounds.extents.y, transform.collider.bounds.center.z);
		Collider[] colliders = Physics.OverlapSphere(groundPoint, radius);
		
		foreach (Collider c in colliders) {
			AvoidanceBehaviour ab = c.GetComponent<AvoidanceBehaviour>();
			if (ab != null) {
				ab.AddObstacle(transform);
			}
		}
	}
}
