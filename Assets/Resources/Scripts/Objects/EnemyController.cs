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
	
	bool stunned = false;
	
	void Start() {
		arrivalComponent = transform.GetComponent<ArrivalBehaviour>();
		//arrivalComponent.stoppingRadius = attackRadius-5;
	}
	
	void Update() {
		if (!stunned) {
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
		if (arrivalComponent.targetObject == Game.Player.transform) Game.Player.TakeDamage(strength);
		else arrivalComponent.targetObject.GetComponent<CompanionController>().TakeDamage(strength);
		
		cooling = true;
		Invoke("Cooldown", attackCooldown/speedModifier);
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
			Debug.Log ("switch");
		}
		
		health -= damage;
		
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
