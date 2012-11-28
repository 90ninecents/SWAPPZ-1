using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour {
	public GameObject destructionEffect;
	GameObject particles;
	bool played = false;
	
	void Start() {
		particles = Instantiate(destructionEffect) as GameObject;
		particles.transform.position = transform.position;
	}
	
	public void TakeDamage(int damage) {
		particles.GetComponent<ParticleSystem>().Emit(10);
		Destroy(gameObject.collider);
		transform.renderer.enabled = false;
		played = true;
	}
	
	void Update() {
		if (played && !particles.GetComponent<ParticleSystem>().isPlaying) {
			Destroy(gameObject);
		}
	}
}
