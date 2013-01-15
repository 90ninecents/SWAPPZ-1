using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {
	// Update is called once per frame
	void Start() {
		if (transform.GetComponent<TextMesh>() != null) transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
		else transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z*-1);
	}
	
	void Update () {
		float prevZ = transform.rotation.eulerAngles.z;
		
		transform.LookAt(Camera.main.transform);
		transform.RotateAround(new Vector3(0,0,0), 180);
		
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,prevZ);
	}
}
