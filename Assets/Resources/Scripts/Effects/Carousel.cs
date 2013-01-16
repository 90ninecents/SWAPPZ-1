using UnityEngine;
using System.Collections;

public class Carousel : MonoBehaviour {
	
	int numberOfSlots = 4;
	bool vertical = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAroundLocal(new Vector3(1,0,0), 0.1f);
	}
	
}
