using UnityEngine;
using System.Collections;

public class FadingPopup : MonoBehaviour {
	public float lifeInSeconds = 5;
	public float travelDistance = 25;
	public float travelSpeed = 5;
	float distanceTravelled = 0;
	
	public float fadeSpeed = 1;
	
	void OnEnable() {
		distanceTravelled = 0;
		Invoke("LifeEnd", lifeInSeconds);
	}
	
	void OnDisable() {
		CancelInvoke("LifeEnd");
		Color temp = transform.renderer.material.color;
		temp.a = 1;
		transform.renderer.material.color = temp;
	}
	
	// Update is called once per frame
	void Update () {
		if (distanceTravelled < travelDistance) {
			transform.Translate(new Vector3(0,travelSpeed,0));
			distanceTravelled += travelSpeed;
		}
		else {
			Color temp = transform.renderer.material.color;
			temp.a -= 0.01f*fadeSpeed;
			transform.renderer.material.color = temp;
		}
	}
	
	void LifeEnd() {
		transform.renderer.enabled = false;
		enabled = false;
	}
}
