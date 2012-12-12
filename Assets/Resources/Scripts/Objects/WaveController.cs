using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	// Controls the release of waves (GameObjects containing spawners)
	
	public GameObject[] waves;		// set in inspector
	public GUIText waveCounter;
	int waveNumber = 0;
	public float checkFrequencyInSeconds = 0.5f;

	void Start () {
		foreach (GameObject go in waves) {
			go.SetActiveRecursively(false);
		}
		
		InvokeRepeating("CheckWave", 0, checkFrequencyInSeconds);
	}
	
	void LaunchWave() {
		waves[waveNumber-1].SetActiveRecursively(true);
		if (waveCounter != null) {
			waveCounter.text = "Wave "+waveNumber;
			waveCounter.GetComponent<SlidingTexture>().StartSlide();
		}
		
		InvokeRepeating("CheckWave", checkFrequencyInSeconds, checkFrequencyInSeconds);
	}
	
	void CheckWave () {
		if (Game.EnemyGroup.GetChildCount() == 0 && waveNumber < waves.Length) {
			waves[waveNumber].SetActiveRecursively(false);
			waveNumber++;
			CancelInvoke("CheckWave");
			Invoke("LaunchWave", Game.TimeBetweenWaves);
		}
	}
}
