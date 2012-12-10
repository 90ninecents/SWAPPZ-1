using UnityEngine;
using System.Collections;

public class CompanionController : MonoBehaviour {
	
	PlayerController pc;
	Boid boidComponent;
	ArrivalBehaviour arrivalComponentE;
	ArrivalBehaviour arrivalComponentP;
	
	int strength;
	float attackRadius;
	float attackCooldown;
	
	public float enemySearchRadius = 100.0f;
	
	EnemyController target;
	
	bool cooling = false;
	
	void Awake() {
		boidComponent = transform.GetComponent<Boid>();
		pc = transform.GetComponent<PlayerController>();
		
		arrivalComponentE = boidComponent.GetBehaviour("ToEnemy") as ArrivalBehaviour;
		arrivalComponentP = boidComponent.GetBehaviour("ToPlayer") as ArrivalBehaviour;
		
		strength = pc.attackStrengths[0]/4;
		attackRadius = pc.attackRadius*2;
		attackCooldown = pc.attackCooldown*10;
	}
	
	void OnEnable() {
		pc.anim.Play("idle");
			
		boidComponent.Speed = boidComponent.maxSpeed;
		
		boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(1);
		boidComponent.GetBehaviour("ToTracker").SetWeight(0);
		boidComponent.GetBehaviour("ObstacleAvoidance").SetWeight(1);
		boidComponent.GetBehaviour("Separation").SetWeight(1);
		
		(boidComponent.GetBehaviour("ToPlayer") as ArrivalBehaviour).targetObject = Game.Player.transform;
		
		boidComponent.maxSpeed -= 15;
		boidComponent.Speed = boidComponent.maxSpeed;
	}
	
	void OnDisable() {
		boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
		boidComponent.GetBehaviour("ToTracker").SetWeight(1);
		boidComponent.GetBehaviour("ObstacleAvoidance").SetWeight(0);
		boidComponent.GetBehaviour("Separation").SetWeight(0);
		
		boidComponent.maxSpeed += 15;
		boidComponent.Speed = boidComponent.maxSpeed;
	}

	void Update() {
		if (target == null) {
			if (Game.EnemyGroup.childCount > 0) FindTarget();
			if (arrivalComponentP.Steering != Vector3.zero && !pc.anim.IsPlaying("run")) {
				pc.anim.CrossFadeQueued("run", 0.1f, QueueMode.PlayNow);
			}
			else if (arrivalComponentP.Steering == Vector3.zero) {
				pc.anim.CrossFadeQueued("idle", 0.1f, QueueMode.CompleteOthers);
			}
		}
		
		else {
			if (arrivalComponentE.Steering != Vector3.zero && !pc.anim.IsPlaying("run")) {
				pc.anim.CrossFadeQueued("run", 0.1f, QueueMode.PlayNow);
			}
			else if (!cooling && (transform.position-target.transform.position).magnitude <= attackRadius) {
				int attackNumber = Random.Range(1,3);
				pc.anim.CrossFadeQueued("attack"+attackNumber+"",0,QueueMode.PlayNow);
				pc.anim.CrossFadeQueued("idle",0.1f,QueueMode.CompleteOthers);
				
				target.TakeDamage(strength, transform);
				cooling = true;
				InvokeRepeating("Cooldown", attackCooldown, attackCooldown);
			}
			else if (cooling) {
				pc.anim.CrossFadeQueued("idle",0.15f,QueueMode.CompleteOthers);
				Vector3 aim = arrivalComponentE.targetObject.position-transform.position;
				aim.y = 0;
				
				transform.rigidbody.rotation = Quaternion.LookRotation(aim);
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
