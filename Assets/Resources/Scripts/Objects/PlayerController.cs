using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	// COMPONENTS/INFO------------------------------------------------------------------
	private Boid boidComponent;			// The turtle's boid script
	public string playerName;			// Player's name (e.g. Leonardo)
	
	public Transform weapon1;			// reference to turtle's weapon #1 for trail rendering
	ParticleSystem weaponTrail1;		// the trail attached to weapon 1
	public Transform weapon2;			// reference to turtle's weapon #2 for trail rendering
	ParticleSystem weaponTrail2;		// the trail attached to weapon 2
	
	
	// STATS----------------------------------------------------------------------------
	public int healthMax = 100;			// Maximum health
	int health;							// Current health
	public int armor = 100;				// Affected by powerups
	
	
	// POWERUPS/MODIFIERS---------------------------------------------------------------
	List<Powerup> powerups;				// List of active powerups
	
	public float healthRegenRate = 1.0f;// Seconds between health regen ticks
	public int healthRegenAmount = 50;	// Amount of health regenerated per tick
	
	float strengthModifier = 1.0f;		// Affected by powerups
	float speedModifier = 1.0f;			// Affected by powerups; modifies both movement speed and attack speed
	float armorModifier = 1.0f;			// Affected by powerups
	float regenModifier = 1.0f;			// Affected by powerups; modifies speed of regeneration, not amount
	float sizeModifier = 1.0f;			// Affects model size, attack radius
	
	float comboModifier = 1.0f;	//REMOVE
	float xpModifier = 1.0f;	//REMOVE
	
	int maxSizeIncrease = 2;			// The maximum size increase that can be gained through powerups
	
	
	// XP/COMBOS------------------------------------------------------------------------
	int comboMeter = 0;		//REMOVE	// Current combo points
	int comboMax = 100;		//REMOVE	// Combo points to unlock mega move
	int comboPoints = 10;	//REMOVE	// Points gained after each combo
	
	int xp = 0;				//REMOVE
	int xpTNL = 1000;		//REMOVE
	
	
	// ATTACKS--------------------------------------------------------------------------
	public float attackRadius = 20.0f;				// Maximum distance from enemy before needing to jump to its location
	public int[] attackStrengths = {100,200};		// Attack strength
	public float[] attackSpeeds = {1.5f, 0.75f, 1}; // Attack animation speed
	public float attackCooldown = 0.25f;			// Time between consecutive attacks
	
	
	// FLAGS----------------------------------------------------------------------------
	bool cooling = false;				// Flag to indicate whether the turtle is cooling down between attacks
	bool jumping = false;				// Flag to handle jump attack on distant targets
	bool invincible = false;			// Affected by powerups
	bool dragging = false;				// Tracks dragging state
	bool swiping = false;				// Tracks swiping state
	bool moving = false;				// Tracks movement state
	
	
	//_________________CHANGE_________________
	public string[] combos = {"1221", "2112"};
	public int[] comboStrengths = {300,300};
	
	string currentCombo = "";
	public float comboCooldown = 0.5f;	// If the time since last attack exceeds this number, combo resets
	
	public float ComboPercentage { get { return (float)comboMeter/comboMax; } }
	//________________________________________
	
	
	// GETTERS/SETTERS------------------------------------------------------------------
	public float HealthPercentage { get {return (float)health/healthMax; } }
	public int Health { get { return health; } 
						set { health = value; if (health > healthMax) health = healthMax; } }
	
	public float XPPercentage { get { return (float)xp/xpTNL; } }	//REMOVE
	
	public int FrameCount { get { return frameCount; }		
							set { frameCount = value; } }
	
	public ArrivalBehaviour ArrivalTouch { get { return (boidComponent.GetBehaviour("ToTracker")) as ArrivalBehaviour; } }
	public ArrivalBehaviour ArrivalEnemy { get { return (boidComponent.GetBehaviour("ToEnemy")) as ArrivalBehaviour; } }
	
	
	// ANIMATIONS-----------------------------------------------------------------------
	public Animation anim;				// The turtle's animation component	
	int frameCount = 0;					// Frame counter for enabling/disabling Mikey's nunchuks
	

	// SWIPING INFO---------------------------------------------------------------------
	Transform enemyHit;					// The enemy that was swiped over
	
	Vector2 dragStart;					// The start point of the swipe motion
	Vector2 dragEnd;					// The end point of the swipe motino
	
	int capsuleSize = 25;				// The radius of the capsule that represents the area swiped over
	
	public float jumpSpeed = 5;			// The speed at which the player jumps/dashes to a distant enemy
	
	Vector2 lastTouchPos;
	
	float swipeCooldown = 0.005f;
	
	int hitCount = 0;
	
	Vector2 lastSwipeDir;
	Vector2 lastSwipeDir2;
	
	
	
	void Awake() {
		lastSwipeDir = new Vector2(0,0);
		lastSwipeDir2 = new Vector2(0,0);
		
		// Set values
		boidComponent = gameObject.GetComponent<Boid>();
		
		health = healthMax;
		anim = transform.GetComponentInChildren<Animation>();
		
		playerName = transform.name.Substring(0, transform.name.Length-7);
		
		if (weapon1 != null) {
			weaponTrail1 = weapon1.GetComponentInChildren<ParticleSystem>();
			weaponTrail1.Stop();
		}
		if (weapon2 != null) {
			weaponTrail2 = weapon2.GetComponentInChildren<ParticleSystem>();
			weaponTrail2.Stop();
		}
		
		if (playerName == "Michelangelo") {
			weapon1.gameObject.active = false;
		}
	}
	
	void OnEnable() {
		// Set steering behaviours
		boidComponent = transform.GetComponent<Boid>();
		
		boidComponent.GetBehaviour("ToEnemy").SetWeight(0);
		boidComponent.GetBehaviour("ToPlayer").SetWeight(0);
		boidComponent.GetBehaviour("ToTracker").SetWeight(1);
		
		boidComponent.Speed = boidComponent.maxSpeed;
		
		powerups = new List<Powerup>();
		
		// Gestures
		Gesture.onTouchUpE += OnTouchUp;
		Gesture.onDraggingE += OnDrag;
		//Gesture.onDraggingEndE += OnDragEnd;
		Gesture.onChargingE += OnPressAndHold;
		//Gesture.onChargeEndE += OnPressAndHoldEnd;
	}
	
	void OnDisable() {
		// Gestures
		Gesture.onTouchUpE -= OnTouchUp;		
		Gesture.onDraggingE -= OnDrag;
		//Gesture.onDraggingEndE -= OnDragEnd;
		Gesture.onChargingE -= OnPressAndHold;
		//Gesture.onChargeEndE -= OnPressAndHoldEnd;
	}
	
	void OnPressAndHold(ChargedInfo ci) {
		if (!swiping && !dragging && !jumping) {
			moving = true;
			
			Ray ray = Camera.main.ScreenPointToRay(ci.pos);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit, 1000, 1 << 13)) {
				ArrivalTouch.targetPoint = hit.point;
			}
		}
	}
	
//	void OnPressAndHoldEnd(ChargedInfo ci) {
//		print ("charge end: "+ci.pos);
//	}
	
	void OnDrag(DragInfo di) {
		//CancelInvoke("StopTest");
		
		if (!moving) {
			if (!dragging) dragStart = di.pos;
			dragging = true;
			
			if (!swiping && di.delta.magnitude > 15) {
				swiping = true;
			}
			
			
			lastSwipeDir2 = lastSwipeDir;
			lastSwipeDir = di.pos-lastTouchPos;
			lastTouchPos = di.pos;
		}
		
//		Ray ray = Camera.main.ScreenPointToRay(di.pos);
//		//RaycastHit hit;
//		RaycastHit[] hits;
//		hits = Physics.RaycastAll(ray, 1000);
//		
//		//if (Physics.Raycast(ray, out hit, 1000)) {
//		if (hits.Length > 0) {
//			bool enemy = false;
//			foreach (RaycastHit hit in hits) {
//				if (hit.transform.GetComponent<EnemyController>() != null) {
//					enemy = true;
//					enemyHit = hit.transform;
//					break;
//				}
//			}
//			
//			if (!swiping && di.delta.magnitude > 25) {
//				swiping = true;
//				
//				if (!enemy) {
//					ArrivalTouch.targetPoint = transform.position;
//					ExecuteAttack();
//				}
//				else {
//					//enemyHit = hit.transform;
//					Vector3 prevRot = transform.rotation.eulerAngles;
//					transform.LookAt(enemyHit.position);
//					
//					if (Mathf.Abs((transform.position-enemyHit.position).magnitude) > attackRadius) Invoke ("JumpToTarget", 0.1f);
//					else ExecuteAttack();
//				}
//			}
//		}
	}
	
	void JumpToTarget() {
		if (enemyHit != null) {
			Vector3 prevRot = transform.rotation.eulerAngles;
			transform.LookAt(enemyHit);
			transform.rotation = Quaternion.Euler(prevRot.x, transform.rotation.eulerAngles.y, prevRot.z);
			
			ArrivalTouch.targetPoint = enemyHit.position - ((enemyHit.position - transform.position)/10);
			speedModifier *= jumpSpeed;
			jumping = true;
		}
	}
	
	void OnDragEnd(Vector2 touchPos) {
		if (swiping) {
			dragging = false;
			dragEnd = touchPos;
			
			// Find where the dragging motion intersects playing field
			RaycastHit hit1;
			Physics.Raycast(Camera.main.ScreenPointToRay(dragStart), out hit1, 5000, ((1 << 13) | (1 << 15)));
			
			RaycastHit hit2;
			Physics.Raycast(Camera.main.ScreenPointToRay(dragEnd), out hit2, 5000, ((1 << 13) | (1 << 15)));
			
			// Create a capsule from the above hit points and move it towards the camera to see if any enemies were swiped
			RaycastHit[] hits =	Physics.CapsuleCastAll(hit1.point, hit2.point, capsuleSize, Camera.main.transform.forward*-1);
			
			if (hits.Length > 0) {
				Transform prevEnemy = enemyHit;
				enemyHit = null;
				
				foreach (RaycastHit hit in hits) {
					if (hit.transform.GetComponent<EnemyController>() != null) {
						enemyHit = hit.transform;
						
						if (enemyHit == prevEnemy) break;
					}
				}
				
				if (enemyHit != null) {
					hitCount++;
					if (hitCount > 3) hitCount = 1;
					
					// if the swiped enemy is outside the turtle's reach, jump to the enemy
					if ((enemyHit.position-transform.position).magnitude > attackRadius*1.5) JumpToTarget();
					
					// otherwise, face the enemy and attack
					else {
						ArrivalTouch.targetPoint = transform.position;
						
						Vector3 prevRot = transform.rotation.eulerAngles;
						transform.LookAt(enemyHit);
						transform.rotation = Quaternion.Euler(prevRot.x, transform.rotation.eulerAngles.y, prevRot.z);
						
						ExecuteAttack();
					}
				}
				else enemyHit = prevEnemy;
			}
		}
		
		swiping = false;
		moving = false;
		
				lastSwipeDir = new Vector2(0,0);
		lastSwipeDir2 = new Vector2(0,0);
	}
	
	void OnTouchUp(Vector2 touchPos) {
		if (!swiping && !dragging && !jumping) {
			bool exit = false;
			foreach (GUITexture tex in Game.UIList) {
				if (tex.enabled && tex.GetScreenRect().Contains(touchPos)) {
					exit = true;
					break;
				}
			}
			
			if (!exit) {
				Ray ray = Camera.main.ScreenPointToRay(touchPos);
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit, 1000)) {
					ArrivalTouch.targetPoint = hit.point;
				}
			}
		}
		
		else if (swiping && lastSwipeDir == Vector2.zero) {
			OnDragEnd(lastTouchPos);
		}
		
		if (!dragging) moving = false;
		hitCount = 0;
		swiping = false;
		dragging = false;
	}
	
	
	void StopTest() {
//		if (new Vector2(Input.mousePosition.x, Input.mousePosition.y) == lastTouchPos || (Input.touchCount > 0 && Input.touches[0].position == lastTouchPos)) {
//			if (Input.touchCount > 0) {
//				OnDragEnd(Input.touches[0].position);
//				dragStart = Input.touches[0].position;
//			}
//			else {
//				OnDragEnd(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
//				dragStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
//			}
//			
//			dragging = true;
//		}
	}
	
	
	void Update() {
		if (health > 0) {
			
			// If swipe has stopped, trigger swipe end
			if ((swiping) && (Input.touchCount > 0 || Input.GetMouseButton(0))) {
				//if (new Vector2(Input.mousePosition.x, Input.mousePosition.y) == lastTouchPos || (Input.touchCount > 0 && Input.touches[0].position == lastTouchPos)) {
				//	Invoke("StopTest", swipeCooldown);
					//if (Input.touchCount > 0) OnDragEnd(Input.touches[0].position);
					//else OnDragEnd(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
				//}
				
				float angle = Vector3.Angle(lastSwipeDir, lastSwipeDir2);
				if (angle > 180) angle -= 360;
				angle = Mathf.Abs (angle);
				
				if (angle > 90 || lastSwipeDir == Vector2.zero) {
					if (Input.touchCount > 0) {
						OnDragEnd(Input.touches[0].position);
						dragStart = Input.touches[0].position;
					}
					else {
						OnDragEnd(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
						dragStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					}
					
					dragging = true;
				}
				
			}
			
			
			if (jumping && ((enemyHit.position-transform.position).magnitude <= attackRadius*1.65)) {
				speedModifier /= jumpSpeed;
				jumping = false;
				print(speedModifier);
				ExecuteAttack();
			}
			else {
				if (!anim.IsPlaying("attack1_"+playerName) && !anim.IsPlaying("attack2_"+playerName) && !anim.IsPlaying("attack3_"+playerName)) boidComponent.Speed = (boidComponent.maxSpeed)*speedModifier;
				else boidComponent.Speed = 0;
				
				if (ArrivalTouch.CalculateSteering(transform.position) != Vector3.zero && !anim.IsPlaying("run_"+playerName)) {
					anim.CrossFadeQueued("run_"+playerName, 0.1f, QueueMode.PlayNow);
				}
				else if (ArrivalTouch.CalculateSteering(transform.position) == Vector3.zero && (anim.IsPlaying ("run_"+playerName) || !anim.isPlaying)) {
					anim.CrossFadeQueued("idle_"+playerName,0.15f,QueueMode.CompleteOthers);
				}
				
				// Enable/Disable Mikey's nunchucks
				if (playerName == "Michelangelo") {
					if (anim.IsPlaying("attack1_"+playerName)) {
						frameCount++;
						
						if (frameCount > 160*Time.timeScale) {
							weapon1.gameObject.active = true;
							weapon2.gameObject.active = false;
						}
						else {
							weapon2.gameObject.active = true;
							weapon1.gameObject.active = false;
						}
					}
					else {
						weapon1.gameObject.active = true;
						weapon2.gameObject.active = false;
						frameCount = 0;
					}
				}
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
	
	public void ExecuteAttack() {
		int attackNumber = hitCount;
		if (hitCount == 0) attackNumber = 1;
		
		//if (!cooling) {
			anim.CrossFadeQueued("attack"+attackNumber+"_"+playerName,0,QueueMode.PlayNow).speed = attackSpeeds[attackNumber-1];			
			anim.CrossFadeQueued("idle_"+playerName,0.1f,QueueMode.CompleteOthers);
			
			CancelInvoke("BreakCombo");
			
			currentCombo += ""+attackNumber;
			
			bool match = false;
			for (int i = 0; i < combos.Length; i++) {
				if (combos[i].StartsWith(currentCombo)) {
					if (combos[i] == currentCombo) {
						currentCombo = "";
						if (comboMeter < comboMax) comboMeter += Mathf.RoundToInt(comboPoints*comboModifier);
						
						anim.CrossFadeQueued("attack3_"+playerName,0,QueueMode.PlayNow).speed = attackSpeeds[2];
						anim.CrossFadeQueued("idle_"+playerName,0.1f,QueueMode.CompleteOthers);
						
						ToggleTrail1();
						Invoke("ToggleTrail1", attackSpeeds[2]/3);
						
						ToggleTrail2();
						Invoke("ToggleTrail2", attackSpeeds[2]/3);
					}
					match = true;
				}
			}
			
			if (currentCombo != "") {
				//Invoke("ToggleTrail"+attackNumber, 0);
				//Invoke("ToggleTrail"+attackNumber, attackSpeeds[attackNumber-1]/3);
			}
			
			if (!match) {
				currentCombo = currentCombo.Remove(0,1);
			}
			

			
			// Check for object to be hit by attack			
			Collider[] collisions = Physics.OverlapSphere(transform.position, attackRadius*sizeModifier);
			
			Vector3 toEnemy;
			EnemyController enemy;
			bool hit = false;
			
			foreach (Collider c in collisions) {
				enemy = c.transform.GetComponent<EnemyController>();
				if (enemy != null) {
					toEnemy = transform.position-enemy.transform.position;
					
					if (Mathf.Abs(Vector3.Angle(transform.forward, toEnemy)) >= 130) {
						enemy.TakeDamage(Mathf.RoundToInt(attackStrengths[attackNumber-1]*strengthModifier), transform);
						// Get XP on hit
						if (xp < xpTNL) ReceiveXP(enemy.xpGain);
						
						Game.DisplayDamage(Mathf.RoundToInt(attackStrengths[attackNumber-1]*strengthModifier), enemy.transform);
						hit = true;
					}
				}
			}
			if (hit) AudioManager.PlayAudio("Sword"+Random.Range(1,6), AudioManager.UnusedChannel);
			else AudioManager.PlayAudio("Swoosh"+Random.Range(1,5), AudioManager.UnusedChannel);
	
			// Combo/jumping/cooldowns
			if (currentCombo != "") Invoke("BreakCombo", comboCooldown);
			
			cooling = true;
			Invoke("Cooldown", attackCooldown*attackSpeeds[attackNumber-1]*speedModifier);
			
//			if (attackNumber == 2) {
//				if (boidComponent.maxSpeed > 0) {
//					jumping = true;
//					Invoke ("EndDash", ((attackCooldown*attackSpeeds[attackNumber])/2)*speedModifier);
//				}
//			}

		//}
	}
	
	public void BreakCombo() {
		currentCombo = "";
	}
	
	void Cooldown() {
		cooling = false;
	}
	void EndDash() {
		jumping = false;
	}
	
	public void CollectPowerup(Powerup p) {
		Destroy(p.gameObject);
		
		if (p.effect != null) {
			if (p.playEffectOnAwake) Destroy(p.effect);
			else {
				p.effect.SetActiveRecursively(true);
				Destroy(p.effect, 3.0f);
			}
		}
		
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
			
			if (sizeModifier > maxSizeIncrease) sizeModifier = maxSizeIncrease;
			else transform.localScale *= p.sizeModifier;
			
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
		if (!invincible && health > 0) {
			health -= Mathf.RoundToInt(damage-(armor*armorModifier));
			
			GameObject particle = Instantiate(Resources.Load("fx/Prefabs/Hit 0"+Random.Range(1,3)+" Particle System")) as GameObject;
			particle.transform.position = transform.position+new Vector3(0,40,0);
			Destroy(particle, 1.0f);
			
			//if (!anim.IsPlaying("run_"+playerName)) 
			if (anim.IsPlaying("idle_"+playerName)) anim.CrossFadeQueued("hit_"+playerName, 0.05f, QueueMode.PlayNow);
			
			
			if (health <= 0) {
				// trigger game over
				CancelInvoke("HealthTick");
				health = 0;				
				
				anim.CrossFadeQueued("death_"+playerName, 0.05f, QueueMode.PlayNow);
				Game.PacifyEnemies();
				Invoke("EndGame", 2.5f);
			}
			else if (!IsInvoking("HealthTick")) {
				InvokeRepeating("HealthTick", healthRegenRate/regenModifier, healthRegenRate/regenModifier);
			}
		}
	}
	
	void EndGame() {
		Game.EndGame(false);
	}
	
	void HealthTick() {
		if (health < healthMax) health += healthRegenAmount;
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
	
	void ToggleTrail1() {
		if (weaponTrail1 != null) {
			if (weaponTrail1.isPlaying) {
				weaponTrail1.Stop ();
				weaponTrail1.Clear();
			}
			else {
				weaponTrail1.Clear();
				weaponTrail1.Play();
			}
		}
	}
	void ToggleTrail2() {
		if (weaponTrail2 != null) {
			if (weaponTrail2.isPlaying) {
				weaponTrail2.Stop ();
				weaponTrail2.Clear();
			}
			else {
				weaponTrail2.Clear();
				weaponTrail2.Play();
			}
		}
	}
}
