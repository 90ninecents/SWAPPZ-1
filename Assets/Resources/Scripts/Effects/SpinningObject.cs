using UnityEngine;
using System.Collections;

public class SpinningObject : MonoBehaviour {
	
	public float speed = 1.0f;
	public int direction = 1;
	
	// Update is called once per frame
	void Update () {
		transform.RotateAroundLocal(new Vector3(0,0,1), direction*speed);
	}
}
