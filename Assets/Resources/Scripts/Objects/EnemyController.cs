using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public int health = 100;
	public int strength = 10;
	public float attackRadius = 25.0f;	// distance from target before attack can be made
	public int attackCooldown = 2;		// Seconds between attacks
	
	public static float speedModifier = 1.0f;
	
	protected ArrivalBehaviour arrivalComponent;
	
	public int xpGain = 100;
	protected bool cooling = false;
	protected bool stunned = false;
	
	Animation anim;
	
	void Start() {
		arrivalComponent = transform.GetComponent<ArrivalBehaviour>();
		
		anim = transform.GetComponentInChildren<Animation>();
		
		foreach (AnimationState state in anim) {
			if (state.name == "run") state.speed = 0.75f;
		}
		
		
	}
	
	void Update() {
		if (!stunned) {
			if (arrivalComponent.Steering.magnitude > 0) anim.Play("run");
			else if (cooling) {
				if (anim.IsPlaying("run")) anim.CrossFadeQueued("idle", 0f, QueueMode.PlayNow);
				else anim.CrossFadeQueued("idle", 0.05f, QueueMode.CompleteOthers);
			}
			
			transform.GetComponent<Boid>().Speed = transform.GetComponent<Boid>().maxSpeed*speedModifier;
			
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
		}
	}
	
	protected virtual void Attack() {
		anim.Stop("run");
		anim.Stop("idle");
		anim.CrossFadeQueued("toAttack", 0.1f, QueueMode.PlayNow);
		anim.CrossFadeQueued("attack", 0.1f, QueueMode.CompleteOthers);
		//anim.CrossFadeQueued("idle", 0.05f, QueueMode.CompleteOthers);
		
		Invoke ("AttackDelay", 0.5f);
//		if (arrivalComponent.targetObject == Game.Player.transform) Game.Player.TakeDamage(strength);
//		else arrivalComponent.targetObject.GetComponent<CompanionController>().TakeDamage(strength);
		
		cooling = true;
		Invoke("Cooldown", attackCooldown/speedModifier);
	}
	
	protected void AttackDelay() {
		if (arrivalComponent.targetObject == Game.Player.transform) Game.Player.TakeDamage(strength);
		else arrivalComponent.targetObject.GetComponent<CompanionController>().TakeDamage(strength);
	}
	
	protected void Cooldown() {
		cooling = false;
	}	
	
	public void Stun(float duration) {
		stunned = true;
		
		Invoke("EndStun", duration);
	}
	
	void EndStun() {
		stunned = false;
	}
	
	public void TakeDamage(int damage, Transform attacker) {		
		if (attacker != arrivalComponent.targetObject) {
			arrivalComponent.targetObject = attacker;
			//Debug.Log ("switch");
		}
		
		health -= damage;
		
		if (health <= 0) {
			Game.EnemiesKilled++;
			
			if (Game.SpawnCoin()) {
				GameObject coin = Instantiate(Resources.Load("Prefabs/Objects/General/Coin")) as GameObject;
				coin.transform.position = transform.position+new Vector3(0,15,0);
			}
				
			Destroy(gameObject);
		}
	}
}
