using UnityEngine;
using System.Collections;

public class RangedEnemyController : EnemyController {
	public float attackReach = 200.0f;	// Distance projectiles travel
	public GameObject projectile;		// The projectile object this enemy spawns
	public float projectileSpeed = 50.0f;
	
	FleeBehaviour fleeComponent;
	
	void Start() {
		arrivalComponent = transform.GetComponent<ArrivalBehaviour>();
		//fleeComponent = transform.GetComponent<FleeBehaviour>();
		
		arrivalComponent.stoppingRadius = attackRadius-5;
	}
	
	protected override void Attack() {
		//if (arrivalComponent.targetObject == Game.Player.transform) Game.Player.TakeDamage(strength);
		//else arrivalComponent.targetObject.GetComponent<CompanionController>().TakeDamage(strength);
		GameObject go = Instantiate(projectile) as GameObject;
		go.transform.position = transform.position+(transform.forward*5);
		go.GetComponent<Projectile>().Launch(projectileSpeed, transform.forward, attackReach);
		
		cooling = true;
		Invoke("Cooldown", attackCooldown);
	}
}
