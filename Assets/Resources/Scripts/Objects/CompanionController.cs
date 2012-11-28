using UnityEngine;
using System.Collections;

public class CompanionController : MonoBehaviour {
	
	PlayerController pc;
	Boid boidComponent;
	
	int strength;
	float attackRadius;
	float attackCooldown;
	
	public float enemySearchRadius = 100.0f;
	
	EnemyController target;
	
	bool cooling = false;
	
	void Awake() {
		boidComponent = transform.GetComponent<Boid>();
		pc = transform.GetComponent<PlayerController>();
		
		strength = pc.attackStrengths[0];
		attackRadius = pc.attackRadius;
		attackCooldown = pc.attackCooldown;
	}
	
	void OnEnable() {
		boidComponent.Speed = boidComponent.maxSpeed;
		
		boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(1);
		boidComponent.GetBehaviour("ToTracker").SetWeight(0);
		boidComponent.GetBehaviour("ObstacleAvoidance").SetWeight(1);
		boidComponent.GetBehaviour("Separation").SetWeight(1);
		
		(boidComponent.GetBehaviour("ToPlayer") as ArrivalBehaviour).targetObject = Game.Player.transform;
	}
	
	void OnDisable() {
		boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
		boidComponent.GetBehaviour("ToTracker").SetWeight(1);
		boidComponent.GetBehaviour("ObstacleAvoidance").SetWeight(0);
		boidComponent.GetBehaviour("Separation").SetWeight(0);
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
		
		if (closest != null && smallestDistance < enemySearchRadius) {
			boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
			boidComponent.GetBehaviour("ToEnemy").SetWeight(1);
			target = closest;
			(boidComponent.GetBehaviour("ToEnemy") as ArrivalBehaviour).targetObject = target.transform;
		}
		else if ((closest == null || smallestDistance > enemySearchRadius) && boidComponent.GetBehaviour("ToEnemy").weight != 0) {
			boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
			boidComponent.GetBehaviour("ToPlayer").SetWeight(1);
		}
	}
	
	public void TakeDamage(int damage) {
		pc.TakeDamage(damage);
	}
}
