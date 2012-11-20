using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject entityToSpawn;
	public int numberToSpawn = 5;
	public float spawnInterval = 5;			// Seconds between spawnings
	
	int numberSpawned = 0;

	// Use this for initialization
	void Start () {		
		InvokeRepeating("SpawnEntity", 0, spawnInterval);
	}
	
	void SpawnEntity() {
		GameObject entity = Instantiate(entityToSpawn) as GameObject;
		entity.transform.parent = Game.EnemyGroup;
		entity.transform.position = transform.position;
		entity.rigidbody.AddForce(new Vector3(0,Physics.gravity.y*10*entity.rigidbody.mass,0));
		
		Transform target = Game.Player.transform;
		entity.GetComponent<ArrivalBehaviour>().targetObject = target;
		entity.transform.LookAt(new Vector3(target.position.x, entity.transform.position.y, target.position.z));
		
		
		numberSpawned++;
		
		if (numberSpawned >= numberToSpawn) {
			CancelInvoke("SpawnEntity");
			Destroy(gameObject);
		}
	}
}
