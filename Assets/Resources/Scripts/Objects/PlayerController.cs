using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	private Boid boidComponent;
	
	List<Powerup> powerups;
	
	// Stats
	public int healthMax = 100;
	int health;
	public int armor = 100;				// Affected by powerups
	
	public float healthRegenRate = 1.0f;
	
	float strengthModifier = 1.0f;		// Affected by powerups
	float speedModifier = 1.0f;			// Affected by powerups; modifies both movement speed and attack speed
	float armorModifier = 1.0f;			// Affected by powerups
	float regenModifier = 1.0f;			// Affected by powerups; modifies speed of regeneration, not amount
	float xpModifier = 1.0f;
	float sizeModifier = 1.0f;			// Affects model size, attack radius
	float comboModifier = 1.0f;
	
	bool invincible = false;			// Affected by powerups

	int comboMeter = 0;					// Current combo points
	int comboMax = 100;					// Combo points to unlock mega move
	int comboPoints = 10;				// Points gained after each combo
	
	int xp = 0;
	int xpTNL = 1000;
	
	// Attacks/Combos
	public float attackRadius = 20.0f;
	public int[] attackStrengths = {100,200};
	public float[] attackSpeeds = {1.5f, 0.75f, 1};
	public float attackCooldown = 0.25f;
	
	bool cooling = false;
	bool dashing = false;	// Handles forward motion on attack
	
	public string[] combos = {"1221", "2112"};
	public int[] comboStrengths = {300,300};
	
	string currentCombo = "";
	public float comboCooldown = 0.5f;	// If the time since last attack exceeds this number, combo resets
	
	public float ComboPercentage { get { return (float)comboMeter/comboMax; } }
	public float HealthPercentage { get {return (float)health/healthMax; } }
	public float XPPercentage { get { return (float)xp/xpTNL; } }
	
	public Animation anim;
	public string playerName;
	
	AudioSource audioPlayer;
	Dictionary<string, AudioClip> audioClips;
	
	
	public AudioSource AudioPlayer { get { return audioPlayer; } }
	
	void Awake() {
		audioPlayer = Camera.main.gameObject.AddComponent<AudioSource>();
		audioClips = new Dictionary<string, AudioClip>();
		
		audioClips.Add("swordSwing", Resources.Load("Sounds/sword_swing") as AudioClip);
		audioClips.Add("swordHit1", Resources.Load("Sounds/sword_strike_1") as AudioClip);
		audioClips.Add("swordHit2", Resources.Load("Sounds/sword_strike_2") as AudioClip);
		
		health = healthMax;
		anim = transform.GetComponentInChildren<Animation>();
		
		playerName = transform.name.Substring(0, transform.name.Length-7);
	}
	
	void OnEnable() {		
		boidComponent = transform.GetComponent<Boid>();
		
		boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
		boidComponent.GetBehaviour("ToTracker").SetWeight(1);
		boidComponent.GetBehaviour("ObstacleAvoidance").SetWeight(0);
		
		transform.GetComponent<ArrivalBehaviour>().targetObject = Game.TouchTracker;
		
		boidComponent.Speed = boidComponent.maxSpeed;
		
		powerups = new List<Powerup>();
	}
	
	void Update() {
		if (dashing) {
			Game.TouchTracker.position += transform.forward*5f;
			transform.position += transform.forward*5f;
		}
		else {
			if (!anim.IsPlaying("attack1_"+playerName) && !anim.IsPlaying("attack2_"+playerName) && !anim.IsPlaying("attack3_"+playerName)) boidComponent.Speed = (Game.Joystick.GetDrive().magnitude*boidComponent.maxSpeed)*speedModifier;
			if (Game.Joystick.GetDrive() != Vector3.zero && !anim.IsPlaying("run_"+playerName)) {
				anim.CrossFadeQueued("run_"+playerName, 0.1f, QueueMode.PlayNow);
			}
			else if (Game.Joystick.GetDrive() == Vector3.zero && (anim.IsPlaying ("run_"+playerName) || !anim.isPlaying)) {
				anim.CrossFadeQueued("idle_"+playerName,0.15f,QueueMode.CompleteOthers);
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (enabled) {
			Coin c = other.gameObject.GetComponent<Coin>();
			if (c != null) {
				CollectCoin(c);
			}
			else {
				Powerup pu = other.gameObject.GetComponent<Powerup>();
				if (pu != null) {
					CollectPowerup(pu);
				}
			}
		}
	}
	
	public void ExecuteAttack(int attackNumber = 1) {
		if (!cooling) {
			anim.CrossFadeQueued("attack"+attackNumber+"_"+playerName,0,QueueMode.PlayNow).speed = attackSpeeds[attackNumber-1];
			anim.CrossFadeQueued("idle_"+playerName,0.1f,QueueMode.CompleteOthers);
			
			CancelInvoke("BreakCombo");
			
			currentCombo += ""+attackNumber;
			
			bool match = false;
			for (int i = 0; i < combos.Length; i++) {
				if (combos[i].StartsWith(currentCombo)) {
					if (combos[i] == currentCombo) {
						Debug.Log("combo #"+i+" performed");
						currentCombo = "";
						if (comboMeter < comboMax) comboMeter += Mathf.RoundToInt(comboPoints*comboModifier);
						
						anim.CrossFadeQueued("attack3_"+playerName,0,QueueMode.PlayNow).speed = attackSpeeds[2];
						anim.CrossFadeQueued("idle_"+playerName,0.1f,QueueMode.CompleteOthers);
					}
					match = true;
				}
			}
			if (!match) {
				currentCombo = currentCombo.Remove(0,1);
			}
			
			// Check for object to be hit by attack 
			Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y+25, transform.position.z), transform.forward);
			RaycastHit hit;
			
			//if (rigidbody.SweepTest(transform.forward, out hit, attackRadius*sizeModifier)) {
			if (Physics.Raycast(ray, out hit, attackRadius*sizeModifier)) {
				EnemyController enemy = hit.collider.transform.GetComponent<EnemyController>();
				BreakableObject obj = hit.collider.transform.GetComponent<BreakableObject>();
				
				// If enemy hit:
				if (enemy != null) {
					enemy.TakeDamage(Mathf.RoundToInt(attackStrengths[attackNumber-1]*strengthModifier), transform);
					// Get XP on hit
					if (xp < xpTNL) ReceiveXP(enemy.xpGain);
				}
				// If breakable object hit:
				else if (obj != null) {
					obj.TakeDamage(Mathf.RoundToInt(attackStrengths[attackNumber-1]*strengthModifier));
				}
				
				if (enemy!=null || obj!=null) {
					if (enemy != null) Game.DisplayDamage(Mathf.RoundToInt(attackStrengths[attackNumber-1]*strengthModifier), enemy.transform);
					else Game.DisplayDamage(Mathf.RoundToInt(attackStrengths[attackNumber-1]*strengthModifier), obj.transform);
					
					audioPlayer.clip = audioClips["swordHit"+Random.Range(1,3)];
					audioPlayer.Play();
				}
				else {
					audioPlayer.clip = audioClips["swordSwing"];
					audioPlayer.Play();
				}
			}
			else {
				audioPlayer.clip = audioClips["swordSwing"];
				audioPlayer.Play();
			}
			
			if (currentCombo != "") Invoke("BreakCombo", comboCooldown);
			
			cooling = true;
			Invoke("Cooldown", attackCooldown*attackSpeeds[attackNumber]*speedModifier);
			
			if (attackNumber == 2) {
				if (boidComponent.maxSpeed > 0) {
					dashing = true;
					Invoke ("EndDash", ((attackCooldown*attackSpeeds[attackNumber])/2)*speedModifier);
				}
			}
		}
	}
	
	public void BreakCombo() {
		currentCombo = "";
	}
	
	void Cooldown() {
		cooling = false;
	}
	void EndDash() {
		dashing = false;
	}
	
	public void CollectPowerup(Powerup p) {
		Destroy(p.gameObject);
		
		// Assuming a powerup that affects enemies/restores life has no other benefits
		if (p.destroyEnemies) {
			Game.DestroyEnemies(p.effectRadius);
		}
		
		else if (p.slowEnemies) {
			Game.StartSlowMo(p.durationInSeconds);
		}
		
		else if (p.stunEnemies) {
			Game.StunEnemies(p.durationInSeconds, p.effectRadius);
		}
		
		else if (p.healthRestorePercent > 0) {
			health += healthMax*(p.healthRestorePercent/100);
			if (health > healthMax) health = healthMax;
		}
		
		// Powerups with duration
		else {
			powerups.Add(p);
			if (powerups.Count == 1) InvokeRepeating("PowerupTick", 1, 1);
			
			armorModifier 	 *= p.armorModifier;
			speedModifier 	 *= p.speedModifier;
			strengthModifier *= p.damageModifier;
			regenModifier 	 *= p.regenModifier;
			xpModifier 		 *= p.xpModifier;
			comboModifier 	 *= p.comboModifier;
			
			sizeModifier 	 *= p.sizeModifier;
			transform.localScale *= p.sizeModifier;
			
			if (IsInvoking("HealthTick")) {
				CancelInvoke("HealthTick");
				InvokeRepeating("HealthTick", healthRegenRate/regenModifier, healthRegenRate/regenModifier);
			}
			
			boidComponent.turningSpeed *= p.speedModifier;
			
			// don't use invincible = p.invincibility; will cause powerups to cancel invincibility granted by other powerups
			if (p.invincibility) invincible = true;
		}
	}
	
	void CollectCoin(Coin c) {
		c.Kill();
		Game.Coins++;
	}
	
	public void TakeDamage(int damage) {
		if (!invincible) {
			health -= Mathf.RoundToInt(damage-(armor*armorModifier));
			if (anim.IsPlaying("idle_"+playerName)) anim.CrossFadeQueued("hit_"+playerName, 0.05f, QueueMode.PlayNow);
			
			if (health <= 0) {
				// trigger game over
				Game.EndGame();
				health = 0;
			}
			else if (!IsInvoking("HealthTick")) {
				InvokeRepeating("HealthTick", healthRegenRate/regenModifier, healthRegenRate/regenModifier);
			}
		}
	}
	
	void HealthTick() {
		if (health < healthMax) health++;
		if (health >= healthMax) {
			CancelInvoke("HealthTick");
			health = healthMax;
		}
	}
	
	void PowerupTick() {
		List<Powerup> dead = new List<Powerup>();
		
		foreach (Powerup p in powerups) {			
			p.CountDown();
			if (p.Expired) {
				dead.Add(p);
				
				armorModifier 	 /= p.armorModifier;
				speedModifier 	 /= p.speedModifier;
				strengthModifier /= p.damageModifier;
				regenModifier	 /= p.regenModifier;
				xpModifier 		 /= p.xpModifier;
				comboModifier 	 /= p.comboModifier;
				
				sizeModifier 	 /= p.sizeModifier;
				transform.localScale /= p.sizeModifier;
				
				if (IsInvoking("HealthTick")) {
					CancelInvoke("HealthTick");
					InvokeRepeating("HealthTick", healthRegenRate/regenModifier, healthRegenRate/regenModifier);
				}
				
				boidComponent.turningSpeed /= p.speedModifier;
				
				if (p.invincibility) invincible = false;
			}
		}
		
		foreach (Powerup p in dead) {
			powerups.Remove(p);
		}
		
		if (powerups.Count == 0) CancelInvoke("PowerupTick");
	}
	
	public void ReceiveXP(int amount) {
		xp += Mathf.RoundToInt(amount*xpModifier);
	}
}
