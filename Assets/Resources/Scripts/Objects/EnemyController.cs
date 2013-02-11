using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public int health = 100;
	public int strength = 10;
	public float attackRadius = 25.0f;	// distance from target before attack can be made
	public int attackCooldown = 2;		// Seconds between attacks
	
	public static float speedModifier = 1.0f;
	
	Boid boidComponent;
	protected ArrivalBehaviour arrivalComponent;
	protected SeparationBehaviour separationComponent;
	
	public int xpGain = 100;
	public int score = 100;
	protected bool cooling = false;
	protected bool stunned = false;
	protected bool passive = false;
	
	protected Animation anim;
	protected string enemyName;
	
	void Start() {		
		enemyName = transform.name.Substring(0, transform.name.Length-7);
		
		arrivalComponent = transform.GetComponent<ArrivalBehaviour>();
		
		anim = transform.GetComponentInChildren<Animation>();
		
		boidComponent = transform.GetComponent<Boid>();
		
		foreach (AnimationState state in anim) {
			if (state.name == "run_"+enemyName) state.speed = 0.75f;
		}
	}
	
	void Update() {		
		if (!stunned && !passive && health > 0) {
			if (cooling && arrivalComponent.Steering == Vector3.zero) {
				if (anim.IsPlaying("run_"+enemyName)) anim.CrossFadeQueued("endRun_"+enemyName, 0f, QueueMode.PlayNow);
				else anim.CrossFadeQueued("idle_"+enemyName, 0.05f, QueueMode.CompleteOthers);
			}
			else {
				anim.Play("run_"+enemyName);
			}
			
			boidComponent.Speed = boidComponent.maxSpeed*speedModifier;
			
			if (arrivalComponent.targetObject == null) {
				arrivalComponent.targetObject = Game.Player.transform;
			}
			
			if (!cooling && (transform.position-arrivalComponent.targetObject.position).magnitude <= attackRadius) {
				Attack ();
			}
			
			// rotate to face player at all times
			else if (cooling) {
				Vector3 aim = arrivalComponent.targetObject.position-transform.position;
				aim.y = 0;
				
				transform.rigidbody.rotation = Quaternion.LookRotation(aim);
			}
		}
		else {
			// play stunned animations
			//boidComponent.Speed = 0;
			arrivalComponent.SetWeight(0);
			if (!anim.IsPlaying("idle_"+enemyName)) anim.CrossFadeQueued("idle_"+enemyName, 0.05f, QueueMode.PlayNow);
		}
	}
	
	protected virtual void Attack() {
		if (transform.name.StartsWith("Foot Clan Double Sword")) AudioManager.PlayAudio("Sword"+Random.Range(1,6), AudioManager.UnusedChannel, 0.5f);
		else if (transform.name.StartsWith("Foot Clan Single Sword")) AudioManager.PlayAudio("Punch"+Random.Range(1,5), AudioManager.UnusedChannel, 0.5f);
		//else AudioManager.PlayAudio("Swoosh"+Random.Range(1,5), AudioManager.UnusedChannel, 0.5f);
		
		anim.Stop("run_"+enemyName);
		anim.Stop("idle_"+enemyName);
		anim.CrossFadeQueued("toAttack_"+enemyName, 0.1f, QueueMode.PlayNow);
		anim.CrossFadeQueued("attack_"+enemyName, 0.1f, QueueMode.CompleteOthers);
		//anim.CrossFadeQueued("idle_"+enemyName, 0.1f, QueueMode.CompleteOthers);
		
		Invoke ("AttackDelay", 0.5f);
		
		cooling = true;
		Invoke("Cooldown", attackCooldown/speedModifier);
	}
	
	protected virtual void AttackDelay() {
		if (arrivalComponent.targetObject == Game.Player.transform) Game.Player.TakeDamage(strength);
		//else arrivalComponent.targetObject.GetComponent<CompanionController>().TakeDamage(strength);
	}
	
	protected void Cooldown() {
		cooling = false;
	}	
	
	public void Stun(float duration) {
		stunned = true;
		
		Invoke("EndStun", duration);
	}
	
	public void SetPassive(bool flag) {
		passive = flag;
	}
	
	void EndStun() {
		stunned = false;
		arrivalComponent.SetWeight(1);
	}
	
	public void TakeDamage(int damage, Transform attacker) {
		if (health > 0) {
//			if (attacker != arrivalComponent.targetObject) {
//				arrivalComponent.targetObject = attacker;
//				Debug.Log ("switch");
//			}
			
			health -= damage;
			anim.CrossFadeQueued("hit_"+enemyName, 0/*.05f*/,QueueMode.PlayNow);
			GameObject particle = Instantiate(Resources.Load("fx/Prefabs/Hit Stars")) as GameObject;
			particle.transform.position = transform.position+new Vector3(0,55,0);
			Destroy(particle, 0.5f);
			
			if (health <= 0) {
				Game.EnemiesKilled++;
				Game.Score += score;
				
				if (Game.SpawnCoin()) {
					GameObject coin = Instantiate(Resources.Load("Prefabs/Objects/General/Coin")) as GameObject;
					coin.transform.position = transform.position+new Vector3(0,15,0);
				}
				
				CancelInvoke("AttackDelay");
				//arrivalComponent.SetWeight(0);
				//anim.Stop();
				//anim.CrossFadeQueued("death_"+enemyName, 0.05f, QueueMode.PlayNow);
				Destroy(gameObject);
				Destroy (this);
			}
		}
	}
}
