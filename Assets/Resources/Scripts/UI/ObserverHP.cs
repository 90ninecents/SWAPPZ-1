using UnityEngine;
using System.Collections;

public class ObserverHP : MonoBehaviour {
	
	float originalWidth = 0;
	
	void Start() {
		originalWidth = transform.guiTexture.pixelInset.width;
	}
	
	void Update () {
		Rect r = transform.guiTexture.pixelInset;
		r.width = Game.Player.HealthPercentage*originalWidth;
		transform.guiTexture.pixelInset = r;
	}
}
