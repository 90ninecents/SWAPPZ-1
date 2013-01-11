using UnityEngine;
using System.Collections;

public class ClearSave : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		PlayerPrefs.DeleteAll();
	}
}
