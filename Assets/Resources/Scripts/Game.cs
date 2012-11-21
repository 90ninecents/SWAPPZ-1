using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	// This class holds commonly-referenced objects for better encapsulation
	public static Game instance;
	
	// DO NOT make these fields static - static fields can't be set in the inspector
	Transform playerObject;
	public Transform joystickObject;
	public Transform enemyGroupObject;
	public Transform touchTracker;
	
	public float powerupSpawnInterval = 2.0f;		// Seconds between attempting to spawn a powerup
	public float powerupSpawnDistance = 100.0f;
	
	PlayerController player;
	Joystick joystick;
	// -----
	
	static List<GameObject> availablePowerups;
	//static List<GameObject> companions;
	static GUITexture[] uiList;
	
	public static PlayerController Player { get { return instance.player; } }
	public static Joystick Joystick { get { return instance.joystick; } }
	public static Transform EnemyGroup { get { return instance.enemyGroupObject; } }
	public static Transform TouchTracker { get { return instance.touchTracker; } }
	

	// Use this for initialization
	void Start () {
		if (instance == null) instance = this;
		
		// Load chosen companions
		//companions = new List<GameObject>();
		uiList = GameObject.FindObjectsOfType(typeof(GUITexture)) as GUITexture[];
		
		string[] characters = SavedData.CharacterLoadout.Split(SavedData.Separator[0]);
		
		foreach (string s in characters) {
			// First character = player
			if (s == characters[0]) {
				GameObject go = Instantiate(Resources.Load("Prefabs/Player Characters/"+s)) as GameObject;
				go.transform.position = new Vector3(0,10,0);
				SwitchPlayer(go);
			}
			// others = companions
			else {
				GameObject companion = Instantiate(Resources.Load("Prefabs/Player Characters/"+s)) as GameObject;
				companion.GetComponent<CompanionController>().enabled = true;
				companion.GetComponent<PlayerController>().enabled = false;
				companion.transform.position = new Vector3(0,10,0);
				//companions.Add(companion);
			}
		}
		
		
		// Load chosen items
		availablePowerups = new List<GameObject>();
		
		string[] items = SavedData.ItemLoadout.Split(SavedData.Separator[0]);
		
		foreach (string s in items) {
			if (s.StartsWith("Powerup")) {
				availablePowerups.Add(Resources.Load("Prefabs/In-Game Items/"+s) as GameObject);
			}
		}
		
		player = playerObject.GetComponent<PlayerController>();
		joystick = joystickObject.GetComponent<Joystick>();
		
		if (availablePowerups.Count > 0) InvokeRepeating("SpawnPowerup", powerupSpawnInterval, powerupSpawnInterval);
	}
	
	void OnEnable() {
		Gesture.onTouchDownE += OnTouch;
		// Unpause
		Time.timeScale = 1.0f;
	}
	
	void OnDisable() {
		Gesture.onTouchDownE -= OnTouch;
	}
	
	void OnTouch(Vector2 touchPos) {
		
		bool exit = false;
		foreach (GUITexture tex in uiList) {
			if (tex.GetScreenRect().Contains(touchPos)) {
				exit = true;
				break;
			}
		}
		
		if (!exit) {
			Ray ray = Camera.main.ScreenPointToRay(touchPos);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 1000)) {
				if (hit.transform.GetComponent<PlayerController>() != null) {
					SwitchPlayer(hit.transform.gameObject);
				}
			}
		}
		
	}
	
	public static void DestroyEnemies(float radius = 0.0f) {
		// Destroys all enemies in a circular area with given radius around the player
		// If radius is 0, all spawned enemies are destroyed
		
		int count = EnemyGroup.childCount;
		
		if (radius == 0) {
			for (int i = 0; i < count; i++) {
				Destroy(EnemyGroup.GetChild(i).gameObject);
			}
		}
		
		else {			
			for (int i = 0; i < count; i++) {
				if ((EnemyGroup.GetChild(i).position-instance.playerObject.position).magnitude <= radius) {
					Destroy(EnemyGroup.GetChild(i).gameObject);
				}
			}
		}
	}
	
	void SwitchPlayer(GameObject go) {
		
		if (playerObject != null) {
			// Reset previous player character (if applicable)
			player.enabled = false;
			playerObject.GetComponent<CompanionController>().enabled = true;
		}
		
		// Set up new player character
		playerObject = go.transform;
		playerObject.GetComponent<ArrivalBehaviour>().SetWeight(1);
		playerObject.GetComponent<ArrivalBehaviour>().targetObject = touchTracker;
		
		// Disable companion AI
		playerObject.GetComponent<CompanionController>().enabled = false;
		
		// Enable player control
		player = playerObject.GetComponent<PlayerController>();
		player.enabled = true;
		
		
		// Update camera and touch tracker
		Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(playerObject);
		
		touchTracker.position = new Vector3(playerObject.position.x, touchTracker.position.y, playerObject.position.z);
	}
	
	void SpawnPowerup() {
		// Choose a random powerup from the list of chosen powerups and spawn it in the player's view
		
		// 25% chance of spawning each interval
		if (Random.Range(0.0f, 1.0f) < 0.25) {			
			int index = Random.Range(0, availablePowerups.Count);
			GameObject powerup = Instantiate(availablePowerups[index]) as GameObject;
			powerup.transform.position = new Vector3(instance.playerObject.position.x+Random.Range(-powerupSpawnDistance, powerupSpawnDistance), instance.playerObject.position.y+powerup.renderer.bounds.extents.y, instance.playerObject.position.z+Random.Range(-powerupSpawnDistance, powerupSpawnDistance));
		}
	}
}