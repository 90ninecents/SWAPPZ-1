using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public float lifeInSeconds = 7;
	public float warningTimeInSeconds = 2;
	public float warnRateInSeconds = 1;
	public float spinSpeed = 1;
	
	bool flag = true;
	
	void Awake() {
		Invoke("Kill", lifeInSeconds);
		InvokeRepeating ("Warning", lifeInSeconds-warningTimeInSeconds, warnRateInSeconds);
	}
	
	public void SetTimes(float life, float warning) {
		CancelInvoke("Kill");
		CancelInvoke("Warning");
		
		lifeInSeconds = life;
		warningTimeInSeconds = warning;
		
		Awake();
	}
	
	void Update() {
		transform.Rotate (Vector3.forward, spinSpeed);
	}
	
	void Warning() {
		if (flag) transform.localScale /= 2;
		else transform.localScale *= 2;
		
		flag = !flag;
	}
	
	public void Kill() {
		CancelInvoke("Warning");
		CancelInvoke("Kill");
		
		Destroy (gameObject);
	}
}
