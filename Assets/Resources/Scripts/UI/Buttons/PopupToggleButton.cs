using UnityEngine;
using System.Collections;

public class PopupToggleButton : Button {
	public GameObject popup;
	
	public override void Fire() {
		if (popup != null) popup.SetActiveRecursively(!popup.active);
	}
}
