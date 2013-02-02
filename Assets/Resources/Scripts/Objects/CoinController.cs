using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinController {
	static List<Coin> coinList = new List<Coin>();
	
	public static int CoinCount { get { return coinList.Count; } }
	
	public static void SpawnCoins(int num) {
		Vector2 randVec;
		Vector3 pos = new Vector3();
		
		for (int i = 0; i < num; i++) {
			bool go = false;
			
			while (!go) {
				randVec = new Vector2(Random.Range(0.0f,1.0f)*Screen.width, Random.Range(0.0f,1.0f)*Screen.height);
				
				Ray ray = Camera.main.ScreenPointToRay(randVec);
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit, 1000, 1 << 13)) {
					 pos = new Vector3(hit.point.x, hit.point.y+5, hit.point.z);
					if (Physics.OverlapSphere(pos, 10).Length == 1) {
						go = true;
					}
				}
			}
			GameObject coin = Object.Instantiate(Resources.Load("Prefabs/Objects/General/Coin")) as GameObject;
			coin.transform.position = pos+new Vector3(0,10,0);
			coin.GetComponent<Coin>().SetTimes(7.5f, 2.5f);
			
			coinList.Add(coin.GetComponent<Coin>());
		}
		
		
		Camera.main.GetComponent<ThirdPersonCamera>().SetTarget(Game.Player.transform);
	}
	
	public static void CheckCoinTap(Vector2 touchPos) {
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 1000)) {
			Coin c = hit.transform.GetComponent<Coin>();
			if (c != null) {
				coinList.Remove(c);
				c.Kill();
				
				Game.Coins++;
			}
		}
	}
	
}
