using UnityEngine;
using System.Collections;

public class TapDismissButton : Button {

	public override void Fire() {
		if (guiText != null) guiText.enabled = false;
		if (guiTexture != null) guiTexture.enabled = false;
		else gameObject.SetActiveRecursively(false);
	}
}
