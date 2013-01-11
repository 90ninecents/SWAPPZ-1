using UnityEngine;
using System.Collections;

public class GlitchReticle : MonoBehaviour {
	// Place on a GUITexture AR Reticle for a twitching "glitchy" look
	GUITexture textureBase;
	GUITexture textureOverlay;

	// Use this for initialization
	void Start () {
		textureBase = transform.guiTexture;
		print (textureBase.name);
		textureOverlay = Instantiate(textureBase) as GUITexture;
		Destroy(textureOverlay.GetComponent<GlitchReticle>());
		
		textureOverlay.transform.parent = transform;
		
		Color temp = textureOverlay.color;
		temp.a = textureBase.color.a/4;
		
		textureOverlay.color = temp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
