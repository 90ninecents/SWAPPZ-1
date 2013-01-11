using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour {
	public int startingTime = 3;
	public string countdownEndMessage = "GO!";
	bool running = false;
	int time;
	
	public bool Running { get { return running; } }
	
	// Use this for initialization
	void Start () {
		transform.guiText.enabled = false;
		Fire ();
	}
	
	public void Fire() {
		running = true;
		time = startingTime;
		transform.guiText.enabled = true;
		
		transform.guiText.text = "";
		InvokeRepeating("Tick", 1, 1);
	}
	
	void Tick () {
		if (transform.guiText.text == "") {
			transform.guiText.text = ""+time;
		}
		else {
			time--;
			if (time > 0) transform.guiText.text = ""+time;
			else if (time == 0) transform.guiText.text = countdownEndMessage;
			else {
				running = false;
				CancelInvoke("Tick");
				transform.guiText.enabled = false;
			}
		}
	}
}
