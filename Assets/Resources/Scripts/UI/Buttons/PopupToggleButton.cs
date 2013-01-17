using UnityEngine;
using System.Collections;

public class PopupToggleButton : Button {
	public GameObject popup;
	
	public override void Fire() {
		popup.SetActiveRecursively(!popup.active);
	}
}
