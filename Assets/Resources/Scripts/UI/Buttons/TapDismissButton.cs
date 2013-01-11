using UnityEngine;
using System.Collections;

public class TapDismissButton : Button {

	public override void Fire() {
		if (transform.parent.guiTexture != null) transform.parent.gameObject.SetActiveRecursively(false);
		
		else if (guiText != null) guiText.enabled = false;
		else if (guiTexture != null) guiTexture.enabled = false;
		
		
		else gameObject.SetActiveRecursively(false);
	}
}
