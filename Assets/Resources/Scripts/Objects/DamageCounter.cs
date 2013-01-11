using UnityEngine;
using System.Collections;

public class DamageCounter : MonoBehaviour {
	TextMesh textMesh;
	FadingPopup popup;
	Vector3 startPosition;
	
	void Awake() {
		textMesh = transform.GetComponent<TextMesh>();
		popup = transform.GetComponent<FadingPopup>();
		startPosition = transform.position;
	}

	public void Show(string message) {
		//transform.localPosition = startPosition;
		transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
		
		transform.renderer.enabled = true;
		
		textMesh.text = message;
		
		popup.enabled = false;
		popup.enabled = true;
	}
}
