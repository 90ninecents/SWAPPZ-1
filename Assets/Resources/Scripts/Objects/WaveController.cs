using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	// Controls the release of waves (GameObjects containing spawners)
	
	public GameObject[] waves;		// set in inspector
	public GUIText waveCounter;
	int waveNumber = 0;
	public float checkFrequencyInSeconds = 0.5f;
	public int coinsOnWaveEnd = 5;
	public int coinsOnLevelEnd = 20;

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
			
			//if (waveNumber != 1) {
				SpawnRewardCoins(coinsOnWaveEnd);
			//}
			
			CancelInvoke("CheckWave");
			Invoke("LaunchWave", Game.TimeBetweenWaves);
		}
		else if (Game.EnemyGroup.GetChildCount() == 0 && waveNumber < waves.Length) {
			SpawnRewardCoins(coinsOnLevelEnd);
		}
	}
	
	void SpawnRewardCoins(int num) {
		// Pick random location in view to spawn coins
		
		Vector2 randVec;
		for (int i = 0; i < num; i++) {
			print (i);
			randVec = new Vector2(Random.Range(0.0f,1.0f)*Screen.width, Random.Range(0.0f,1.0f)*Screen.height);
			
			Ray ray = Camera.main.ScreenPointToRay(randVec);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 1000, 1 << 13)) {
				GameObject coin = Object.Instantiate(Resources.Load("Prefabs/Objects/General/Coin")) as GameObject;
				coin.transform.position = hit.point;
				
			}
		}
	}
}
