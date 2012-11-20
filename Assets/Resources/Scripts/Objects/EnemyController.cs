using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public int health = 100;
	public int strength = 10;
	public float attackRadius = 25.0f;	// distance from target before attack can be made
	public int attackCooldown = 2;		// Seconds between attacks
	
	ArrivalBehaviour arrivalComponent;
	
	public int xpGain = 100;
	
	bool cooling = false;
	
	void Start() {
		arrivalComponent = transform.GetComponent<ArrivalBehaviour>();
	}
	
	void Update() {
		if (arrivalComponent.targetObject == null) {
			arrivalComponent.targetObject = Game.Player.transform;
		}
		
		if (!cooling && (transform.position-arrivalComponent.targetObject.position).magnitude <= attackRadius) {			
			if (arrivalComponent.targetObject == Game.Player.transform) Game.Player.TakeDamage(strength);
			else arrivalComponent.targetObject.GetComponent<CompanionController>().TakeDamage(strength);
			
			cooling = true;
			Invoke("Cooldown", attackCooldown);
		}
	}
	
	void Cooldown() {
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
			
			// if Player made the kill, award full xp
			//if (attacker == Game.Player.transform) Game.Player.ReceiveXP(xpGain);
			// else award half xp?
			//else Game.Player.ReceiveXP(Mathf.RoundToInt(xpGain/2));
		}
	}
}
