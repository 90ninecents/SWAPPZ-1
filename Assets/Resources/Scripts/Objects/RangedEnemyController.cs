using UnityEngine;
using System.Collections;

public class RangedEnemyController : EnemyController {
	public float attackReach = 200.0f;	// Distance projectiles travel
	public GameObject projectile;		// The projectile object this enemy spawns
	public float projectileSpeed = 50.0f;
	
	FleeBehaviour fleeComponent;
	
	protected override void Attack() {
		AudioManager.PlayAudio("Swoosh"+Random.Range(1,5), AudioManager.UnusedChannel, 0.5f);
		
		anim.Stop("run_"+enemyName);
		anim.Stop("idle_"+enemyName);
		anim.CrossFadeQueued("toAttack_"+enemyName, 0.1f, QueueMode.PlayNow);
		anim.CrossFadeQueued("attack_"+enemyName, 0.1f, QueueMode.CompleteOthers);
		
		cooling = true;
		Invoke("Cooldown", attackCooldown/speedModifier);
		
		Invoke("AttackDelay", 0.5f);
	}
	
	protected override void AttackDelay() {
		GameObject go = Instantiate(projectile) as GameObject;
		go.transform.position = transform.position+(transform.forward*transform.localScale.z)+new Vector3(0,35,0);
		go.transform.Translate(transform.forward*25, Space.World);
		go.transform.Translate(transform.right*-5, Space.World);
		go.GetComponent<Projectile>().Launch(projectileSpeed*speedModifier, transform.forward, strength, attackReach);
	}
}
