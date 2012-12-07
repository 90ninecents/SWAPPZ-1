using UnityEngine;
using System.Collections;

public class RangedEnemyController : EnemyController {
	public float attackReach = 200.0f;	// Distance projectiles travel
	public GameObject projectile;		// The projectile object this enemy spawns
	public float projectileSpeed = 50.0f;
	
	FleeBehaviour fleeComponent;
	
	protected override void Attack() {
		anim.Stop("run");
		anim.Stop("idle");
		anim.CrossFadeQueued("toAttack", 0.1f, QueueMode.PlayNow);
		anim.CrossFadeQueued("attack", 0.1f, QueueMode.CompleteOthers);
		
		GameObject go = Instantiate(projectile) as GameObject;
		go.transform.position = transform.position+(transform.forward*transform.localScale.z)+new Vector3(0,10,0);
		go.GetComponent<Projectile>().Launch(projectileSpeed*speedModifier, transform.forward, strength, attackReach);
		
		cooling = true;
		Invoke("Cooldown", attackCooldown/speedModifier);
	}
}
