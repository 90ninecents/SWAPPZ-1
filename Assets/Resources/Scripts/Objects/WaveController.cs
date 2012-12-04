using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	// Controls the release of waves (GameObjects containing spawners)
	
	public GameObject[] waves;		// set in inspector
	public GUITexture waveCounter;
	int waveNumber = 0;

	void Start () {
		foreach (GameObject go in waves) {
			go.SetActiveRecursively(false);
		}
		
		InvokeRepeating("CheckWave", 0, 3.0f);
	}
	
	void LaunchWave(int number) {
		waves[number].SetActiveRecursively(true);
		if (waveCounter != null) {
			waveCounter.texture = Resources.Load ("Textures/UI/Wave_Count_0"+(number+1)) as Texture;
			waveCounter.GetComponent<SlidingTexture>().StartSlide();
		}
	}
	
	void CheckWave () {
		if (Game.EnemyGroup.GetChildCount() == 0 && waveNumber < waves.Length) {
			waves[waveNumber].SetActiveRecursively(false);
			LaunchWave(waveNumber);
			waveNumber++;
		}
	}
}
