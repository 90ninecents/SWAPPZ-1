using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	public virtual void Fire() {
		// override to define actions on touch
	}
	
	public virtual void PreFire() {
		// override to customise actions on touch
	}
}
