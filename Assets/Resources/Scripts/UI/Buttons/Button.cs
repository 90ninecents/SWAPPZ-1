using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	bool down = false;
	
	Texture upTexture;
	Texture downTexture;
	
	void Awake() {
		if (guiTexture != null) upTexture = guiTexture.texture;
		else if (renderer != null) upTexture = renderer.material.mainTexture;
		
		if (upTexture != null) downTexture = Resources.Load("Textures/UI/Buttons/"+upTexture.name.Substring(0, upTexture.name.Length-2)+"Down") as Texture;
	}
	
	public virtual void Fire() {
		// override to define actions on touch
	}
	
	public virtual void PreFire() {
		// override to customise actions on touch
	}
	
	public void ToggleState() {
		down = !down;
		
		if (down && downTexture != null) {
			if (guiTexture != null) guiTexture.texture = downTexture;
			else if (renderer != null) renderer.material.mainTexture = downTexture;
		}
		else if (!down && downTexture != null) {
			if (guiTexture != null) guiTexture.texture = upTexture;
			else if (renderer != null) renderer.material.mainTexture = upTexture;
		}
	}
}
