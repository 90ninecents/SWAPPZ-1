using UnityEngine;
using System.Collections;

public class MuteButton : Button {
	
	public override void Fire() {
		audio.mute = !audio.mute;
	}
}
