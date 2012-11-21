using UnityEngine;
using System.Collections;

public class CompanionController : MonoBehaviour {
//	public int health = 100;
//	public int strength = 10;
//	public float attackRadius = 25.0f;	// distance from target before attack can be made
//	public int attackCooldown = 2;		// Seconds between attacks
	
	PlayerController pc;
	Boid boidComponent;
	
	int health;
	int strength;
	float attackRadius;
	float attackCooldown;
	
	EnemyController target;
	
	bool cooling = false;
	
	void Awake() {
		boidComponent = transform.GetComponent<Boid>();
		pc = transform.GetComponent<PlayerController>();
		
		health = Mathf.RoundToInt(pc.healthMax*pc.HealthPercentage);
		strength = pc.attackStrengths[0];
		attackRadius = pc.attackRadius;
		attackCooldown = pc.attackCooldown;
	}
	
	void OnEnable() {
		boidComponent.Speed = boidComponent.maxSpeed;
		
		boidComponent.GetBehaviour("ToEnemy").SetWeight(1);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
		boidComponent.GetBehaviour("ToTracker").SetWeight(0);
		
		// Update health
		health = Mathf.RoundToInt(pc.healthMax*pc.HealthPercentage);
		// Find a target
		FindTarget();
	}
	
	void OnDisable() {
		boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
		boidComponent.GetBehaviour("ToTracker").SetWeight(1);
	}
	
	void Update() {
		if (target == null) FindTarget();
		
		else {
			if (!cooling && (transform.position-target.transform.position).magnitude <= attackRadius) {
				target.TakeDamage(strength, transform);
				cooling = true;
				InvokeRepeating("Cooldown", attackCooldown, attackCooldown);
			}
		}
	}
	
	void Cooldown() {
		cooling = false;
		CancelInvoke("Cooldown");
	}
	
	void FindTarget() {
		// pick closest enemy
		float smallestDistance = 0;
		EnemyController closest = null;
		
		foreach (Transform t in Game.EnemyGroup) {
			float distance = (transform.position-t.position).magnitude;
			if (closest == null || distance < smallestDistance) {
				smallestDistance = distance;
				closest = t.GetComponent<EnemyController>();
			}
		}
		
		if (closest != null) {
			boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
			boidComponent.GetBehaviour("ToEnemy").SetWeight(1);
			target = closest;
			(boidComponent.GetBehaviour("ToEnemy") as ArrivalBehaviour).targetObject = target.transform;
		}
		else {
			boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
			
			(boidComponent.GetBehaviour("ToPlayer") as ArrivalBehaviour).targetObject = Game.Player.transform;
			boidComponent.GetBehaviour("ToPlayer").SetWeight(1);
		}
	}
	
	public void TakeDamage(int damage) {
		health -= damage;
		
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}