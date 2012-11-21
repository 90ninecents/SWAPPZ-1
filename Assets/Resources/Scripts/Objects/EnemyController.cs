using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public int health = 100;
	public int strength = 10;
	public float attackRadius = 25.0f;	// distance from target before attack can be made
	public int attackCooldown = 2;		// Seconds between attacks
	
	protected ArrivalBehaviour arrivalComponent;
	
	public int xpGain = 100;
	
	protected bool cooling = false;
	
	void Start() {
		arrivalComponent = transform.GetComponent<ArrivalBehaviour>();
		arrivalComponent.stoppingRadius = attackRadius-5;
	}
	
	void Update() {
		if (arrivalComponent.targetObject == null) {
			arrivalComponent.targetObject = Game.Player.transform;
		}
		
		if (!cooling && (transform.position-arrivalComponent.targetObject.position).magnitude <= attackRadius) {
			Attack ();
		}
		
		// rotate to face player at all times
		else if (cooling) {
			Vector3 aim = arrivalComponent.targetObject.position-transform.position;
			aim.y = transform.position.y;
			
			transform.rotation = Quaternion.LookRotation(aim);
		}
	}
	
	protected virtual void Attack() {
		if (arrivalComponent.targetObject == Game.Player.transform) Game.Player.TakeDamage(strength);
		else arrivalComponent.targetObject.GetComponent<CompanionController>().TakeDamage(strength);
		
		cooling = true;
		Invoke("Cooldown", attackCooldown);
	}
	
	protected void Cooldown() {
		cooling = false;
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
