using UnityEngine;
using System.Collections;

public class AttackButton : Button {
	public int attackNumber = 1;
	
	public override void Fire() {
		Game.Player.ExecuteAttack(attackNumber);
	}
}
