using UnityEngine;
using System.Collections;

public class PlayerSwitchButton : Button {

	public override void Fire() {
		//foreach (string s in SavedData.UnlockedCharacters.Split(SavedData.Separator[0])) {
			//if (s != SavedData.CharacterLoadout) Game.ChangePlayer(s);
		//}
		
		Game.ChangePlayer("Michelangelo");
	}
}
