using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	// Controls the release of waves (GameObjects containing spawners)
	
	public GameObject[] waves;		// set in inspector
	public GUIText waveCounter;
	public GUITexture movePopup;
	public int waveNumber = 0;
	public float checkFrequencyInSeconds = 0.5f;
	public int coinsOnWaveEnd = 5;
	public int coinsOnLevelEnd = 20;
	float currentTime = 5.0f;		// Use this to increment/decrement based on time.
	float timeToHint = 5.0f;		// When the wave is over if after this time they are not at the location, show an arrow
	
	bool paused = false;
	
	GUITexture arrow;

	void Start () {
		foreach (GameObject go in waves) {
			go.SetActiveRecursively(false);
		}
		
		if (movePopup != null) movePopup.enabled = false;
		
		InvokeRepeating("CheckWave", 0, checkFrequencyInSeconds);
		
		Camera.main.GetComponent<ThirdPersonCamera>().SetNewBounds(waves[0].transform);
		
		Invoke("LaunchWave", Game.TimeBetweenWaves);
		
		if (GameObject.Find ("Icon") != null) arrow = GameObject.Find ("Icon").guiTexture;
	}
	
	void LaunchWave() {
		if (!paused) {
			
			CancelInvoke("LaunchWave");
			
			waves[waveNumber].SetActiveRecursively(true);
			
			// snap camera to wave center
			//if (waveNumber != 0) 
				//Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(waves[waveNumber].transform);
			
			if (waveCounter != null) {
				waveCounter.text = "Wave "+(waveNumber+1);
				waveCounter.GetComponent<SlidingTexture>().StartSlide();
			}
			
			InvokeRepeating("CheckWave", checkFrequencyInSeconds, checkFrequencyInSeconds);
		}
		
		else InvokeRepeating("LaunchWave", 1, 1);
	}
	
	void CheckWave () {
		if (!paused) {
			if (Game.EnemyGroup.GetChildCount() == 0 && waves[waveNumber].transform.childCount == 0 && waveNumber < waves.Length-1) {
				waves[waveNumber].SetActiveRecursively(false);
				waveNumber++;
	            Camera.main.GetComponent<ThirdPersonCamera>().SetNewBounds(waves[waveNumber].transform);
				
				//if (waveNumber != 1) {
					//CoinController.SpawnCoins(coinsOnWaveEnd);
					//Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(Game.Player.transform);
				//}
				
				CancelInvoke("CheckWave");
			}
			else if (Game.EnemyGroup.GetChildCount() == 0 && waves[waveNumber].transform.childCount == 0 && waveNumber == waves.Length-1) {
				if (movePopup != null) {
					movePopup.enabled = true;
					InvokeRepeating("CheckPopupEnd", checkFrequencyInSeconds, checkFrequencyInSeconds);
				}
				else {
					//CoinController.SpawnCoins(coinsOnLevelEnd);
					InvokeRepeating("CheckLevelEnd", checkFrequencyInSeconds, checkFrequencyInSeconds);
				}
				
				CancelInvoke("CheckWave");
			}
		}
	}
	
	void CheckPopupEnd() {
		if (movePopup == null || (movePopup != null && movePopup.enabled == false)) {
			//CoinController.SpawnCoins(coinsOnLevelEnd);
			Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(Game.Player.transform);
			
			CancelInvoke("CheckPopupEnd");
			InvokeRepeating("CheckLevelEnd", checkFrequencyInSeconds, checkFrequencyInSeconds);
		}
	}
	
	void CheckLevelEnd() {
		if (CoinController.CoinCount == 0) {
			CancelInvoke("CheckLevelEnd");
			Game.EndGame(true);
		}
	}
	
	void Update() {
		if (!paused && !IsInvoking() && waveNumber > 0) {
			if (Mathf.Abs (Camera.main.transform.position.x - waves[waveNumber].transform.position.x) < 10.0f) {
				LaunchWave();
				if (arrow != null) arrow.enabled = false;
			}
			else {
				currentTime -= Time.deltaTime;
				
				if (currentTime <= 0)
				{
					if (arrow != null) arrow.enabled = true;
					
					currentTime = timeToHint;
				}
			}
		}
	}
	
	public void TogglePause() {
		paused = !paused;
		
		if (paused) {
			gameObject.BroadcastMessage("Pause", SendMessageOptions.DontRequireReceiver);
		}
		else {
			gameObject.BroadcastMessage("Unpause", SendMessageOptions.DontRequireReceiver);
		}
	}
}
