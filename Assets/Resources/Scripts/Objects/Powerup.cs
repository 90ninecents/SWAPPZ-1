using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {	
	public float armorModifier = 1.0f;
	public float damageModifier = 1.0f;
	public float speedModifier = 1.0f;
	public int durationInSeconds = 10;
	public float effectRadius = 0.0f;
	
	public int healthRestorePercent = 0;
	
	public bool invincibility = false;
	public bool destroyEnemies = false;
	
	int life = 0;
	bool expired = false;
	
	public bool Expired { get { return expired; } }
	
	public void CountDown() {
		life++;
		expired = (life >= durationInSeconds);
	}
}
