using UnityEngine;
using System.Collections;

public class PlayerSwitchButton : Button {
	bool debug = true;

	public override void Fire() {
		//foreach (string s in SavedData.UnlockedCharacters.Split(SavedData.Separator[0])) {
			//if (s != SavedData.CharacterLoadout) Game.ChangePlayer(s);
		//}
		
		if (debug) Game.ChangePlayer("Michelangelo");
		else Game.ChangePlayer("Leonardo");
		debug = !debug;
	}
}
