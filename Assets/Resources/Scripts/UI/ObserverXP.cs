using UnityEngine;
using System.Collections;

public class ObserverXP : MonoBehaviour {
	
	float originalWidth = 0;
	
	void Start() {
		originalWidth = transform.guiTexture.pixelInset.width;
	}
	
	void Update () {
		Rect r = transform.guiTexture.pixelInset;
		
		r.width = Game.Player.XPPercentage*originalWidth;
		if (r.width == 0) r.width = 5;
		
		transform.guiTexture.pixelInset = r;
	}
}
