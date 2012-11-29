using UnityEngine;
using System.Collections;

public class Trackablescripts : MonoBehaviour {
	public 	static GameObject ninja;
	public TrackableBehaviour mtrackableBehaviour;
	
	void Awake() {
		ninja = GameObject.FindGameObjectWithTag("ninja") as GameObject;
		mtrackableBehaviour = (TrackableBehaviour)UnityEngine.Object.FindObjectOfType(typeof(TrackableBehaviour));
	}
	
	void Update () {	
		if((mtrackableBehaviour.mStatus == TrackableBehaviour.Status.TRACKED) ||  (mtrackableBehaviour.mStatus == TrackableBehaviour.Status.DETECTED)) {
			GameObject.FindGameObjectWithTag("ninja").GetComponent<Animation>().Play();
			//Debug.Log("marker detected");
		}
		else {
			Debug.Log("marker not detected");
		}
	}
}
