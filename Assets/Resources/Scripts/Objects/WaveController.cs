using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	// Controls the release of waves (GameObjects containing spawners)
	
	public GameObject[] waves;		// set in inspector
	public GUIText waveCounter;
	public GUITexture movePopup;
	int waveNumber = 0;
	public float checkFrequencyInSeconds = 0.5f;
	public int coinsOnWaveEnd = 5;
	public int coinsOnLevelEnd = 20;
	
	GUITexture arrow;

	void Start () {
		foreach (GameObject go in waves) {
			go.SetActiveRecursively(false);
		}
		
		if (movePopup != null) movePopup.enabled = false;
		
		InvokeRepeating("CheckWave", 0, checkFrequencyInSeconds);
		Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(waves[0].transform);
		
		Invoke("LaunchWave", Game.TimeBetweenWaves);
		
		arrow = GameObject.Find ("ProgressArrow").guiTexture;
	}
	
	void LaunchWave() {
		waves[waveNumber-1].SetActiveRecursively(true);
		
		// snap camera to wave center
		if (waveNumber != 1) Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(waves[waveNumber-1].transform);
		
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
			
			if (waveNumber != 1) {
				SpawnRewardCoins(coinsOnWaveEnd);
			}
			
			CancelInvoke("CheckWave");
		}
		else if (Game.EnemyGroup.GetChildCount() == 0 && waveNumber == waves.Length) {
			movePopup.enabled = true;
			CancelInvoke("CheckWave");
			InvokeRepeating("CheckPopupEnd", checkFrequencyInSeconds, checkFrequencyInSeconds);
		}
	}
	
	void CheckPopupEnd() {
		if (movePopup.enabled == false) {
			SpawnRewardCoins(coinsOnLevelEnd);
			CancelInvoke("CheckPopupEnd");
			InvokeRepeating("CheckLevelEnd", checkFrequencyInSeconds, checkFrequencyInSeconds);
		}
	}
	
	void CheckLevelEnd() {
		if (GameObject.FindSceneObjectsOfType(typeof(Coin)).Length == 0) {
			CancelInvoke("CheckLevelEnd");
			Game.EndGame(true);
		}
	}
	
	void SpawnRewardCoins(int num) {
		// Pick random location in view to spawn coins
		
		Vector2 randVec;
		Vector3 pos = new Vector3();
		
		for (int i = 0; i < num; i++) {
			bool go = false;
			
			while (!go) {
				randVec = new Vector2(Random.Range(0.0f,1.0f)*Screen.width, Random.Range(0.0f,1.0f)*Screen.height);
				
				Ray ray = Camera.main.ScreenPointToRay(randVec);
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit, 1000, 1 << 13)) {
					 pos = new Vector3(hit.point.x, hit.point.y+5, hit.point.z);
					if (Physics.OverlapSphere(pos, 10).Length == 1) {
						go = true;
					}
				}
			}
			GameObject coin = Object.Instantiate(Resources.Load("Prefabs/Objects/General/Coin")) as GameObject;
			coin.transform.position = pos;
			coin.GetComponent<Coin>().SetTimes(7.5f, 2.5f);
		}
		
		
		Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(Game.Player.transform);
	}
	
	void Update() {
		if (!IsInvoking("CheckWave") && waveNumber > 1) {
			if (Mathf.Abs (Camera.main.transform.position.x - waves[waveNumber-1].transform.position.x) < 10.0f) {
				LaunchWave();
				arrow.enabled = false;
			}
			else {
				arrow.enabled = true;
			}
		}
	}
}
