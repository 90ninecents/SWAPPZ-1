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
	public Transform playerGroupObject;
	
	public float powerupSpawnInterval = 2.0f;		// Seconds between attempting to spawn a powerup
	
	PlayerController player;
	Joystick joystick;
	// -----
	
	static List<GameObject> availablePowerups;
	static PowerupSpawner[] powerupSpawners;
	//static List<GameObject> companions;
	static GUITexture[] uiList;
	
	//static bool fy = false;
	
	public static PlayerController Player { get { return instance.player; } }
	public static Joystick Joystick { get { return instance.joystick; } }
	public static Transform EnemyGroup { get { return instance.enemyGroupObject; } }
	public static Transform TouchTracker { get { return instance.touchTracker; } }
	public static Transform PlayerGroup { get { return instance.playerGroupObject; } }
	

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
				GameObject g = Resources.Load("Prefabs/Loadout Characters/"+s) as GameObject;
				
				GameObject go = Instantiate(g.GetComponent<InventoryItem>().itemPrefab) as GameObject;
				
				go.transform.position = new Vector3(0,10,0);
				go.transform.parent = playerGroupObject;
				SwitchPlayer(go);
			}
			// others = companions
			else {
				GameObject g = Resources.Load("Prefabs/Loadout Characters/"+s) as GameObject;
				
				GameObject companion = Instantiate(g.GetComponent<InventoryItem>().itemPrefab) as GameObject;
				
				companion.GetComponent<CompanionController>().enabled = true;
				companion.GetComponent<PlayerController>().enabled = false;
				companion.transform.position = new Vector3(0,10,0);
				companion.transform.parent = playerGroupObject;
				//companions.Add(companion);
			}
		}
		
		powerupSpawners = gameObject.GetComponentsInChildren<PowerupSpawner>();
		
		player = playerObject.GetComponent<PlayerController>();
		joystick = joystickObject.GetComponent<Joystick>();
		
		if (powerupSpawners.Length > 0) InvokeRepeating("SpawnPowerup", powerupSpawnInterval, powerupSpawnInterval);
	}
	
	void OnEnable() {
		Gesture.onTouchDownE += OnTouch;
		// Unpause
		Time.timeScale = 1.0f;
		//fy = false;
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
	
	public static void StunEnemies(float duration, float radius = 0.0f) {
		// Stuns all enemies in a circular area with given radius around the player
		// If radius is 0, all spawned enemies are stunned
		
		int count = EnemyGroup.childCount;
		
		if (radius == 0) {
			for (int i = 0; i < count; i++) {
				EnemyGroup.GetChild(i).GetComponent<EnemyController>().Stun(duration);
			}
		}
		
		else {			
			for (int i = 0; i < count; i++) {
				if ((EnemyGroup.GetChild(i).position-instance.playerObject.position).magnitude <= radius) {
					EnemyGroup.GetChild(i).GetComponent<EnemyController>().Stun(duration);
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
		
		// Update all companions (and enemies?) to point at new player
		foreach (Transform t in playerGroupObject) {
			(t.gameObject.GetComponent<Boid>().GetBehaviour("ToPlayer") as ArrivalBehaviour).targetObject = go.transform;
		}
		
		// Set up new player character
		playerObject = go.transform;
		Boid b = playerObject.GetComponent<Boid>();
		b.GetBehaviour("ToTracker").SetWeight(1);
		(b.GetBehaviour("ToTracker") as ArrivalBehaviour).targetObject = touchTracker;
		
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
		
		// Pick random powerup spawner
		int index = Random.Range(0, powerupSpawners.Length);
		
		if (powerupSpawners[index].available) {
			powerupSpawners[index].Spawn();
		}
	}
}
