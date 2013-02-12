using UnityEngine;
using System.Collections;

public class Trackablescripts : MonoBehaviour {
	void Awake() {
		GameObject.FindGameObjectWithTag("ninja").GetComponent<Animation>().Play();
	}
}
