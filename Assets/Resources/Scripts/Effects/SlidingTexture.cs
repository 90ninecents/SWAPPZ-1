using UnityEngine;
using System.Collections;

public class SlidingTexture : MonoBehaviour {
	public Vector3 startPosition;
	public Vector3 endPosition;
	public Vector3 slideDirection = new Vector3(1,0,0);
	public float centerPauseTime = 0.0f;
	public float speed = 1f;
	
	bool running = false;
	bool hasPaused = false;
	
	float distanceToTravel = 0;
	float distanceTravelled = 0;
	
	// Use this for initialization
	void Start () {
		speed /= 100;
		transform.position = startPosition;
		
		distanceToTravel = (startPosition-endPosition).magnitude;
	}
	
	public void StartSlide() {
		transform.position = startPosition;
		running = true;
		
		distanceTravelled = 0;
		hasPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			transform.position += new Vector3(speed*slideDirection.x, speed*slideDirection.y, speed*slideDirection.z);
			distanceTravelled += speed;
			
			if (!hasPaused && distanceTravelled >= distanceToTravel/2) {
				running = false;
				Invoke("EndPause", centerPauseTime);
			}
			
			else if (distanceTravelled >= distanceToTravel) running = false;
		}
	}
	
	void EndPause() {
		running = true;
		hasPaused = true;
	}
}
