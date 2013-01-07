using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	public GameObject effect;
	public bool playEffectOnAwake = false;
	
	public float armorModifier = 1.0f;
	public float damageModifier = 1.0f;
	public float speedModifier = 1.0f;
	public float regenModifier = 1.0f;
	public float xpModifier = 1.0f;
	public float comboModifier = 1.0f;
	
	public float sizeModifier = 1.0f;
	
	public int durationInSeconds = 10;
	public float effectRadius = 0.0f;
	
	public int healthRestorePercent = 0;
	
	public bool invincibility = false;
	public bool destroyEnemies = false;
	public bool stunEnemies = false;
	public bool slowEnemies = false;
	
	int life = 0;
	bool expired = false;
	
	public bool Expired { get { return expired; } }
	
	void Start() {
		if (effect != null) {
			effect = Instantiate(effect) as GameObject;
			effect.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
			//effect.transform.parent = transform;
			effect.SetActiveRecursively(playEffectOnAwake);
		}
	}
	
	public void CountDown() {
		life++;
		expired = (life >= durationInSeconds);
	}
}
