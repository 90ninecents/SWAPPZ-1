using UnityEngine;
using System.Collections;

public class RunningCounter : MonoBehaviour {
	// A class that works with a GUIText component to increment a counter until it reaches a specific number
	
	public int numberOfPlaces = 4;
	public float runSpeed = 1.0f;
	
	float baseSpeed = 1.0f;
	
	int startNumber = 0;
	int number = 0;
	int endNumber = 0;
	GUIText display;
	
	bool running = false;

	// Use this for initialization
	void Awake () {
		display = transform.GetComponent<GUIText>();
	}
	
	public void Run(int targetNumber) {
		endNumber = targetNumber;
		number = startNumber;
		
		InvokeRepeating("Tick", baseSpeed/runSpeed, baseSpeed/runSpeed);
	}
	
	void Tick() {
		if (number < endNumber) {
			number++;
			display.text = FormatCounter();
		}
		else CancelInvoke("Tick");
	}
	
	string FormatCounter() {
		string result = ""+number;
		
		int diff = numberOfPlaces-result.Length;
		for (int i = 0; i < diff; i++) {
			result = "0"+result;
		}
		
		return result;
	}
}
