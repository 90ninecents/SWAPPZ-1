using UnityEngine;
using System.Collections;

public class AttackButton : Button {
	public int attackNumber = 1;
	public PlayerController pc;
	
	void Start() {
		if (pc == null) pc = Game.Player;
	}
	
	public override void Fire() {
		pc.ExecuteAttack(attackNumber);
	}
}
