using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	// Controls the release of waves (GameObjects containing spawners)
	
	public GameObject[] waves;		// set in inspector
	int waveNumber = 0;

	void Start () {
		foreach (GameObject go in waves) {
			go.SetActiveRecursively(false);
		}
		
		InvokeRepeating("CheckWave", 0, 2.0f);
	}
	
	void LaunchWave(int number) {
		waves[number].SetActiveRecursively(true);
	}
	
	void CheckWave () {
		if (Game.EnemyGroup.GetChildCount() == 0) {
			LaunchWave(waveNumber);
			waveNumber++;
		}
	}
}
