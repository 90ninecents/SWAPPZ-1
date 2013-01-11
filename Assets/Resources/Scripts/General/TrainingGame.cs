using UnityEngine;
using System.Collections;

public class TrainingGame : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Game.PlayerGroup.localScale = new Vector3(0.33f,0.33f,0.33f);
		Game.Player.transform.GetComponent<Boid>().maxSpeed = 0;
	}
}
