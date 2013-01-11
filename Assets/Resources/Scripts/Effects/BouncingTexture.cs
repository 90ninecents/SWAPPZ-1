using UnityEngine;
using System.Collections;

public class BouncingTexture : MonoBehaviour {
	public Vector3 bounceDirection = new Vector3(0,1,0);// The direction the texture will move in
	public float height = 0.5f;							// The maximum offset from the original Y position
	public float speed = 0.1f;
	
	float velocity;
	float acceleration;
	
	Vector3 origPos;
//	Vector3 finalPos;
	float distance;
	
	// Use this for initialization
	void Start () {
		origPos = transform.position;
//		finalPos = origPos + (bounceDirection*height);
		
		velocity = height*speed;
		
		float steps = height/velocity;
		
		acceleration = velocity/steps;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity*bounceDirection;
		velocity -= acceleration;
		if (transform.position == origPos) velocity = height*speed;
	}
}
