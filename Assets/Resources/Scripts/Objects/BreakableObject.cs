using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour {
	public GameObject destructionEffect;
	GameObject particles;
	
	void Start() {
		particles = Instantiate(destructionEffect) as GameObject;
		particles.transform.parent = transform;
		particles.transform.localPosition = new Vector3(0,0,0);
	}
	
	public void TakeDamage(int damage) {
		transform.GetComponent<MeshRenderer>().enabled = false;
		particles.GetComponent<ParticleSystem>().Emit(damage);
	}
}
