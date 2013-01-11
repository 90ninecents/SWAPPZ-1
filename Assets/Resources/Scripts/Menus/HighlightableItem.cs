using UnityEngine;
using System.Collections;

public class HighlightableItem : MonoBehaviour {
	Material origMaterial;
//	public Material highlightedMaterial;
	bool selected = false;
	Material highlightedMaterial;
	
	void Start () {
		selected = false;
		origMaterial = transform.renderer.material;
		highlightedMaterial = Resources.Load("Textures/ItemSelect/Materials/"+origMaterial.name.Substring(0,2)+origMaterial.name.Substring(4, origMaterial.name.Length-15)) as Material;
	}
	
	
	public void ToggleSelected () {
		selected = !selected;
		
		if (selected) {
			transform.renderer.material = highlightedMaterial;
		}
		else {
			transform.renderer.material = origMaterial;
		}
	}
}
